using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Order>> GetAllAsync()
    {
        return _dbContext.Orders
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<Order?> GetByIdAsync(int id)
    {
        return _dbContext.Orders.FindAsync(id).AsTask();
    }

    public async Task AddAsync(Order order)
    {
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Order order)
    {
        return _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}
