using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;
using OrderService.Services;

namespace OrderService.Tests;

public class OrderServiceTests
{
    [Fact]
    public async Task GetOrdersAsync_ReturnsMappedOrders()
    {
        var repository = new FakeOrderRepository(
            new Order
            {
                Id = 1,
                CustomerName = "Ivan Petrov",
                ProductName = "Laptop",
                Quantity = 2,
                Price = 1200.99m
            });
        var service = new Services.OrderService(repository);

        var result = await service.GetOrdersAsync();

        var order = Assert.Single(result);
        Assert.Equal(1, order.Id);
        Assert.Equal("Ivan Petrov", order.CustomerName);
        Assert.Equal("Laptop", order.ProductName);
        Assert.Equal(2, order.Quantity);
        Assert.Equal(1200.99m, order.Price);
    }

    [Fact]
    public async Task GetOrderAsync_ReturnsMappedOrder_WhenOrderExists()
    {
        var repository = new FakeOrderRepository(
            new Order
            {
                Id = 7,
                CustomerName = "Anna Smith",
                ProductName = "Phone",
                Quantity = 1,
                Price = 700m
            });
        var service = new Services.OrderService(repository);

        var result = await service.GetOrderAsync(7);

        Assert.NotNull(result);
        Assert.Equal(7, result.Id);
        Assert.Equal("Anna Smith", result.CustomerName);
    }

    [Fact]
    public async Task CreateOrderAsync_MapsDtoAndSavesOrder()
    {
        var repository = new FakeOrderRepository();
        var service = new Services.OrderService(repository);
        var request = new CreateOrderDto
        {
            CustomerName = "Ivan Petrov",
            ProductName = "Laptop",
            Quantity = 2,
            Price = 1200.99m
        };

        var result = await service.CreateOrderAsync(request);

        Assert.Equal(1, result.Id);
        Assert.Equal(request.CustomerName, result.CustomerName);
        Assert.Equal(request.ProductName, result.ProductName);
        Assert.Equal(request.Quantity, result.Quantity);
        Assert.Equal(request.Price, result.Price);
        Assert.NotNull(repository.SavedOrder);
    }

    [Fact]
    public async Task UpdateOrderAsync_UpdatesExistingOrder()
    {
        var existingOrder = new Order
        {
            Id = 3,
            CustomerName = "Old Name",
            ProductName = "Old Product",
            Quantity = 1,
            Price = 100m
        };
        var repository = new FakeOrderRepository(existingOrder);
        var service = new Services.OrderService(repository);
        var request = new UpdateOrderDto
        {
            CustomerName = "New Name",
            ProductName = "New Product",
            Quantity = 5,
            Price = 250m
        };

        var result = await service.UpdateOrderAsync(3, request);

        Assert.True(result);
        Assert.True(repository.UpdateWasCalled);
        Assert.Equal("New Name", existingOrder.CustomerName);
        Assert.Equal("New Product", existingOrder.ProductName);
        Assert.Equal(5, existingOrder.Quantity);
        Assert.Equal(250m, existingOrder.Price);
    }

    [Fact]
    public async Task UpdateOrderAsync_ReturnsFalse_WhenOrderDoesNotExist()
    {
        var service = new Services.OrderService(new FakeOrderRepository());
        var request = new UpdateOrderDto
        {
            CustomerName = "Ivan Petrov",
            ProductName = "Laptop",
            Quantity = 2,
            Price = 1200.99m
        };

        var result = await service.UpdateOrderAsync(100, request);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteOrderAsync_DeletesExistingOrder()
    {
        var existingOrder = new Order
        {
            Id = 4,
            CustomerName = "Ivan Petrov",
            ProductName = "Laptop",
            Quantity = 2,
            Price = 1200.99m
        };
        var repository = new FakeOrderRepository(existingOrder);
        var service = new Services.OrderService(repository);

        var result = await service.DeleteOrderAsync(4);

        Assert.True(result);
        Assert.True(repository.DeleteWasCalled);
        Assert.Same(existingOrder, repository.DeletedOrder);
    }

    private class FakeOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders;

        public FakeOrderRepository(params Order[] orders)
        {
            _orders = orders.ToList();
        }

        public Order? SavedOrder { get; private set; }
        public Order? DeletedOrder { get; private set; }
        public bool UpdateWasCalled { get; private set; }
        public bool DeleteWasCalled { get; private set; }

        public Task<List<Order>> GetAllAsync()
        {
            return Task.FromResult(_orders.ToList());
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            return Task.FromResult(_orders.FirstOrDefault(order => order.Id == id));
        }

        public Task AddAsync(Order order)
        {
            order.Id = 1;
            SavedOrder = order;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Order order)
        {
            UpdateWasCalled = true;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Order order)
        {
            DeleteWasCalled = true;
            DeletedOrder = order;
            return Task.CompletedTask;
        }
    }
}
