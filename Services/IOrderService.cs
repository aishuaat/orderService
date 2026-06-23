using OrderService.DTOs;

namespace OrderService.Services;

public interface IOrderService
{
    Task<List<OrderDto>> GetOrdersAsync();
    Task<OrderDto?> GetOrderAsync(int id);
    Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
    Task<bool> UpdateOrderAsync(int id, UpdateOrderDto dto);
    Task<bool> DeleteOrderAsync(int id);
}
