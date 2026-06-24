using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Services;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Exposes HTTP endpoints for managing orders.
/// Business operations are delegated to <see cref="IOrderService"/>.
/// </summary>
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orders;

    public OrdersController(IOrderService orders)
    {
        _orders = orders;
    }

    /// <summary>
    /// Returns all orders.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrders()
    {
        return Ok(await _orders.GetOrdersAsync());
    }

    /// <summary>
    /// Returns an order by its identifier.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <returns>The order, or HTTP 404 when it does not exist.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        var order = await _orders.GetOrderAsync(id);

        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    /// <summary>
    /// Creates a new order from validated request data.
    /// </summary>
    /// <param name="dto">The data required to create the order.</param>
    /// <returns>HTTP 201 with the created order.</returns>
    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto dto)
    {
        var created = await _orders.CreateOrderAsync(dto);

        return CreatedAtAction(nameof(GetOrder), new { id = created.Id }, created);
    }

    /// <summary>
    /// Replaces the editable values of an existing order.
    /// </summary>
    /// <param name="id">The identifier of the order to update.</param>
    /// <param name="dto">The replacement order values.</param>
    /// <returns>HTTP 204 on success, or HTTP 404 when the order does not exist.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto dto)
    {
        var updated = await _orders.UpdateOrderAsync(id, dto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes an order by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the order to delete.</param>
    /// <returns>HTTP 204 on success, or HTTP 404 when the order does not exist.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var deleted = await _orders.DeleteOrderAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
