using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs;

/// <summary>
/// Represents the editable data of an existing order.
/// The order to update is identified by the route parameter, not by the request body.
/// </summary>
public class UpdateOrderDto
{
    /// <summary>
    /// Gets or sets the updated customer name.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated product name.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated quantity. The value must be greater than zero.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the updated price per unit. The value must be greater than zero.
    /// </summary>
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}
