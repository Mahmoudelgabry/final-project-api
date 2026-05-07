using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Quantity)
                   .IsRequired();

            builder.Property(o => o.PriceAtPurchase)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Order Relation
            builder.HasOne(o => o.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(o => o.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Product Relation
            builder.HasOne(o => o.Product)
                   .WithMany()
                   .HasForeignKey(o => o.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            // GhostCraft Relation
            builder.HasOne(o => o.GhostCraftOrder)
                   .WithMany()
                   .HasForeignKey(o => o.GhostCraftOrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate products in same order
            builder.HasIndex(o => new { o.OrderId, o.ProductId })
                   .IsUnique()
                   .HasFilter("\"ProductId\" IS NOT NULL");

            // Prevent duplicate ghostcrafts in same order
            builder.HasIndex(o => new { o.OrderId, o.GhostCraftOrderId })
                   .IsUnique()
                   .HasFilter("\"GhostCraftOrderId\" IS NOT NULL");
        }
    }
}