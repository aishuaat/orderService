using OrderService.DTOs;
using OrderService.Mappings;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services;

/// <inheritdoc />
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orders;

    public OrderService(IOrderRepository orders)
    {
        _orders = orders;
    }

    /// <inheritdoc />
    public async Task<List<OrderDto>> GetOrdersAsync()
    {
        var orders = await _orders.GetAllAsync();
        return orders.Select(order => order.ToDto()).ToList();
    }

    /// <inheritdoc />
    public async Task<OrderDto?> GetOrderAsync(int id)
    {
        var order = await _orders.GetByIdAsync(id);
        return order?.ToDto();
    }

    /// <inheritdoc />
    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            CustomerName = dto.CustomerName,
            ProductName = dto.ProductName,
            Quantity = dto.Quantity,
            Price = dto.Price
        };

        await _orders.AddAsync(order);

        return order.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateOrderAsync(int id, UpdateOrderDto dto)
    {
        var order = await _orders.GetByIdAsync(id);

        if (order is null)
        {
            return false;
        }

        order.CustomerName = dto.CustomerName;
        order.ProductName = dto.ProductName;
        order.Quantity = dto.Quantity;
        order.Price = dto.Price;

        await _orders.UpdateAsync(order);

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteOrderAsync(int id)
    {
        var order = await _orders.GetByIdAsync(id);

        if (order is null)
        {
            return false;
        }

        await _orders.DeleteAsync(order);

        return true;
    }
}
