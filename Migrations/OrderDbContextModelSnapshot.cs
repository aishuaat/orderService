using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OrderService.Data;

#nullable disable

namespace OrderService.Migrations;

[DbContext(typeof(OrderDbContext))]
partial class OrderDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "10.0.9")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        modelBuilder.Entity("OrderService.Models.Order", entity =>
        {
            entity.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            entity.Property<string>("CustomerName")
                .IsRequired()
                .HasColumnType("text");

            entity.Property<decimal>("Price")
                .HasColumnType("numeric");

            entity.Property<string>("ProductName")
                .IsRequired()
                .HasColumnType("text");

            entity.Property<int>("Quantity")
                .HasColumnType("integer");

            entity.HasKey("Id");

            entity.ToTable("Orders");
        });
    }
}
