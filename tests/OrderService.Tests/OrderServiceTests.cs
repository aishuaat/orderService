using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;
using OrderService.Services;

namespace OrderService.Tests;

public class OrderServiceTests
{
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

    private class FakeOrderRepository : IOrderRepository
    {
        public Order? SavedOrder { get; private set; }

        public Task<List<Order>> GetAllAsync()
        {
            return Task.FromResult(new List<Order>());
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            return Task.FromResult<Order?>(null);
        }

        public Task AddAsync(Order order)
        {
            order.Id = 1;
            SavedOrder = order;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Order order)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Order order)
        {
            return Task.CompletedTask;
        }
    }
}
