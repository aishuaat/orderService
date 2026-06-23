using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OrderService.Data;

#nullable disable

namespace OrderService.Migrations;

[DbContext(typeof(OrderDbContext))]
[Migration("20260623000000_ConfigureOrderFields")]
partial class ConfigureOrderFields
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");

            entity.Property<decimal>("Price")
                .HasPrecision(18, 2)
                .HasColumnType("numeric(18,2)");

            entity.Property<string>("ProductName")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("character varying(200)");

            entity.Property<int>("Quantity")
                .HasColumnType("integer");

            entity.HasKey("Id");

            entity.ToTable("Orders");
        });
    }
}
