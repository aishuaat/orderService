using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs;

public class CreateOrderDto
{
    [Required]
    [StringLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string ProductName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}
