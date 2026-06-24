namespace OrderService.DTOs;

/// <summary>
/// Represents order data returned through the public API.
/// </summary>
/// <param name="Id">The database-generated order identifier.</param>
/// <param name="CustomerName">The name of the customer who placed the order.</param>
/// <param name="ProductName">The name of the ordered product.</param>
/// <param name="Quantity">The number of ordered product units.</param>
/// <param name="Price">The price per product unit.</param>
public record OrderDto(
    int Id,
    string CustomerName,
    string ProductName,
    int Quantity,
    decimal Price);
