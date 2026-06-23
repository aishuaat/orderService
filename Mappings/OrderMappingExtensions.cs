using OrderService.DTOs;
using OrderService.Models;

namespace OrderService.Mappings;

public static class OrderMappingExtensions
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto(
            order.Id,
            order.CustomerName,
            order.ProductName,
            order.Quantity,
            order.Price);
    }
}
