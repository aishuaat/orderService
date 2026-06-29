using System.ComponentModel.DataAnnotations;
using OrderService.DTOs;

namespace OrderService.Tests;

public class DtoValidationTests
{
    [Fact]
    public void CreateOrderDto_IsInvalid_WhenRequiredValuesAreMissing()
    {
        var dto = new CreateOrderDto
        {
            CustomerName = string.Empty,
            ProductName = string.Empty,
            Quantity = 0,
            Price = 0m
        };

        var results = Validate(dto);

        Assert.Contains(results, result => ContainsMember(result, nameof(CreateOrderDto.CustomerName)));
        Assert.Contains(results, result => ContainsMember(result, nameof(CreateOrderDto.ProductName)));
        Assert.Contains(results, result => ContainsMember(result, nameof(CreateOrderDto.Quantity)));
        Assert.Contains(results, result => ContainsMember(result, nameof(CreateOrderDto.Price)));
    }

    [Fact]
    public void UpdateOrderDto_IsValid_WhenValuesMeetContract()
    {
        var dto = new UpdateOrderDto
        {
            CustomerName = "Ivan Petrov",
            ProductName = "Laptop",
            Quantity = 1,
            Price = 1500.50m
        };

        var results = Validate(dto);

        Assert.Empty(results);
    }

    private static List<ValidationResult> Validate(object dto)
    {
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(dto, context, results, validateAllProperties: true);

        return results;
    }

    private static bool ContainsMember(ValidationResult result, string memberName)
    {
        return result.MemberNames.Contains(memberName);
    }
}
