using OrderService.DTOs;
using OrderService.Models;

namespace OrderService.Mappings;

/// <summary>
/// Contains mappings between internal order entities and public API models.
/// </summary>
public static class OrderMappingExtensions
{
    /// <summary>
    /// Converts a persisted order entity to the DTO returned by the API.
    /// </summary>
    /// <param name="order">The order entity to convert.</param>
    /// <returns>The public representation of the order.</returns>
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
