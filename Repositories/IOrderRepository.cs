using OrderService.Models;

namespace OrderService.Repositories;

/// <summary>
/// Defines persistence operations for orders and isolates higher layers from EF Core.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Returns all stored orders.
    /// </summary>
    Task<List<Order>> GetAllAsync();

    /// <summary>
    /// Finds an order by its identifier.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <returns>The matching order, or <see langword="null"/> when it does not exist.</returns>
    Task<Order?> GetByIdAsync(int id);

    /// <summary>
    /// Adds an order and persists the change.
    /// </summary>
    /// <param name="order">The order to add.</param>
    Task AddAsync(Order order);

    /// <summary>
    /// Persists changes made to a tracked order.
    /// </summary>
    /// <param name="order">The tracked order that was changed.</param>
    Task UpdateAsync(Order order);

    /// <summary>
    /// Removes an order and persists the change.
    /// </summary>
    /// <param name="order">The order to remove.</param>
    Task DeleteAsync(Order order);
}
