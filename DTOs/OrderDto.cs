namespace OrderService.DTOs;

public record OrderDto(
    int Id,
    string CustomerName,
    string ProductName,
    int Quantity,
    decimal Price);
