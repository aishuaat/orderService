using Microsoft.AspNetCore.Mvc;
using OrderService.Controllers;
using OrderService.DTOs;
using OrderService.Services;

namespace OrderService.Tests;

public class OrdersControllerTests
{
    [Fact]
    public async Task GetOrders_ReturnsOrders()
    {
        var controller = new OrdersController(new FakeOrderService(
            orders: new List<OrderDto>
            {
                new(1, "Ivan Petrov", "Laptop", 2, 1200.99m)
            }));

        var result = await controller.GetOrders();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var orders = Assert.IsType<List<OrderDto>>(okResult.Value);
        var order = Assert.Single(orders);
        Assert.Equal(1, order.Id);
    }

    [Fact]
    public async Task GetOrder_ReturnsOrder_WhenOrderExists()
    {
        var controller = new OrdersController(new FakeOrderService(
            order: new OrderDto(2, "Anna Smith", "Phone", 1, 700m)));

        var result = await controller.GetOrder(2);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var order = Assert.IsType<OrderDto>(okResult.Value);
        Assert.Equal(2, order.Id);
    }

    [Fact]
    public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        var controller = new OrdersController(new FakeOrderService());

        var result = await controller.GetOrder(100);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateOrder_ReturnsCreatedOrder()
    {
        var controller = new OrdersController(new FakeOrderService());
        var request = new CreateOrderDto
        {
            CustomerName = "Ivan Petrov",
            ProductName = "Laptop",
            Quantity = 1,
            Price = 1500.50m
        };

        var result = await controller.CreateOrder(request);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdOrder = Assert.IsType<OrderDto>(createdResult.Value);
        Assert.Equal(1, createdOrder.Id);
        Assert.Equal(nameof(OrdersController.GetOrder), createdResult.ActionName);
    }

    [Fact]
    public async Task UpdateOrder_ReturnsNoContent_WhenOrderExists()
    {
        var controller = new OrdersController(new FakeOrderService(updateResult: true));
        var request = new UpdateOrderDto
        {
            CustomerName = "Ivan Petrov",
            ProductName = "Laptop",
            Quantity = 2,
            Price = 1200.99m
        };

        var result = await controller.UpdateOrder(1, request);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteOrder_ReturnsNoContent_WhenOrderExists()
    {
        var controller = new OrdersController(new FakeOrderService(deleteResult: true));

        var result = await controller.DeleteOrder(1);

        Assert.IsType<NoContentResult>(result);
    }

    private class FakeOrderService : IOrderService
    {
        private readonly List<OrderDto> _orders;
        private readonly OrderDto? _order;
        private readonly bool _updateResult;
        private readonly bool _deleteResult;

        public FakeOrderService(
            List<OrderDto>? orders = null,
            OrderDto? order = null,
            bool updateResult = false,
            bool deleteResult = false)
        {
            _orders = orders ?? new List<OrderDto>();
            _order = order;
            _updateResult = updateResult;
            _deleteResult = deleteResult;
        }

        public Task<List<OrderDto>> GetOrdersAsync()
        {
            return Task.FromResult(_orders);
        }

        public Task<OrderDto?> GetOrderAsync(int id)
        {
            return Task.FromResult(_order);
        }

        public Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new OrderDto(1, dto.CustomerName, dto.ProductName, dto.Quantity, dto.Price);
            return Task.FromResult(order);
        }

        public Task<bool> UpdateOrderAsync(int id, UpdateOrderDto dto)
        {
            return Task.FromResult(_updateResult);
        }

        public Task<bool> DeleteOrderAsync(int id)
        {
            return Task.FromResult(_deleteResult);
        }
    }
}
