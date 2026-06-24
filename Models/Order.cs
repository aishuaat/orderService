namespace OrderService.Models;

/// <summary>
/// Represents an order persisted by EF Core.
/// This entity is internal to the application and is not used as the public API contract.
/// </summary>
public class Order
{
    /// <summary>
    /// Gets or sets the database-generated order identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer who placed the order.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the ordered product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of ordered product units.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the price per product unit.
    /// </summary>
    public decimal Price { get; set; }
}
