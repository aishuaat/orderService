using OrderService.DTOs;

namespace OrderService.Services;

/// <summary>
/// Defines order use cases exposed to the API layer.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Returns all orders as public API models.
    /// </summary>
    Task<List<OrderDto>> GetOrdersAsync();

    /// <summary>
    /// Returns one order by its identifier.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <returns>The order DTO, or <see langword="null"/> when the order does not exist.</returns>
    Task<OrderDto?> GetOrderAsync(int id);

    /// <summary>
    /// Creates and persists a new order.
    /// </summary>
    /// <param name="dto">Validated order creation data received from the API.</param>
    /// <returns>The created order including its generated identifier.</returns>
    Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="id">The identifier of the order to update.</param>
    /// <param name="dto">Validated replacement values for the order.</param>
    /// <returns><see langword="true"/> when the order was updated; otherwise, <see langword="false"/>.</returns>
    Task<bool> UpdateOrderAsync(int id, UpdateOrderDto dto);

    /// <summary>
    /// Deletes an existing order.
    /// </summary>
    /// <param name="id">The identifier of the order to delete.</param>
    /// <returns><see langword="true"/> when the order was deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteOrderAsync(int id);
}
