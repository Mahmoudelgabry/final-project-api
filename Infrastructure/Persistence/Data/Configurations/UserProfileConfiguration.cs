using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            // 🔑 Key
            builder.HasKey(u => u.Id);

            // 🔥 Properties
            builder.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(u => u.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.ZipCode)
                .IsRequired()
                .HasMaxLength(20);

            // 🔗 One-to-One مع User
            builder.HasOne(u => u.User)
                .WithOne()
                .HasForeignKey<UserProfile>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 💣 مهم جدًا: منع تكرار Profile لنفس اليوزر
            builder.HasIndex(u => u.UserId)
                .IsUnique();
        }
    }
}