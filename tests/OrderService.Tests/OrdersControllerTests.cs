using Microsoft.AspNetCore.Mvc;
using OrderService.Controllers;
using OrderService.DTOs;
using OrderService.Services;

namespace OrderService.Tests;

public class OrdersControllerTests
{
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

    private class FakeOrderService : IOrderService
    {
        public Task<List<OrderDto>> GetOrdersAsync()
        {
            return Task.FromResult(new List<OrderDto>());
        }

        public Task<OrderDto?> GetOrderAsync(int id)
        {
            return Task.FromResult<OrderDto?>(null);
        }

        public Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new OrderDto(1, dto.CustomerName, dto.ProductName, dto.Quantity, dto.Price);
            return Task.FromResult(order);
        }

        public Task<bool> UpdateOrderAsync(int id, UpdateOrderDto dto)
        {
            return Task.FromResult(false);
        }

        public Task<bool> DeleteOrderAsync(int id)
        {
            return Task.FromResult(false);
        }
    }
}
