using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // ============================
            // 🔹 Basic Properties
            // ============================
            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Tax)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.ShippingCost)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(o => o.OrderStatus)
                .IsRequired();

            builder.Property(o => o.PaymentStatus)
                .IsRequired();

            builder.Property(o => o.PaymentMethod)
                .IsRequired();

            builder.Property(o => o.RequestId)
                .HasMaxLength(100);

            builder.Property(o => o.CreatedAt)
                .HasDefaultValueSql("NOW()");

            // ============================
            // 🔗 Relationships (🔥 FIX)
            // ============================
            builder.HasMany(o => o.OrderItems)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ============================
            // 💣 Idempotency
            // ============================
            builder.HasIndex(o => new { o.RequestId, o.UserId })
                .IsUnique()
                .HasFilter("[RequestId] IS NOT NULL");
        }
    }
}