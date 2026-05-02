using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // ============================
            // 🔹 Basic Properties
            // ============================
            builder.Property(p => p.UserId)
            .IsRequired();

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.PaymentMethod)
                .IsRequired();

            builder.Property(p => p.TransactionId)
                .HasMaxLength(150);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("NOW()");

            // ============================
            // 💳 Duplicate Payment Protection
            // ============================
            builder.HasIndex(p => p.TransactionId)
                .IsUnique()
                .HasFilter("\"TransactionId\" IS NOT NULL");
        }
    }

}
