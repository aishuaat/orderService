using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data;

/// <summary>
/// Provides EF Core access to the Order Service database.
/// It is consumed by the repository layer rather than directly by controllers.
/// </summary>
public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(order => order.CustomerName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(order => order.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(order => order.Price)
                .HasPrecision(18, 2);
        });
    }
}
