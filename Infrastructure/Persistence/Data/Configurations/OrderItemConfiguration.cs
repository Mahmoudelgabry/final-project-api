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

            // 🔥 Order relation
            builder.HasOne(o => o.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(o => o.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Product)
                   .WithMany()
                   .HasForeignKey(o => o.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ============================
            // 💣 Prevent Duplicate Products in Same Order
            // ============================
            builder.HasIndex(o => new { o.OrderId, o.ProductId })
                   .IsUnique();
        }
    }

}
