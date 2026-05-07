using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class GhostCraftOrderConfiguration : IEntityTypeConfiguration<GhostCraftOrder>
    {
        public void Configure(EntityTypeBuilder<GhostCraftOrder> builder)
        {
            builder.Property(g => g.DishDescription)
                   .IsRequired();

            builder.Property(g => g.Allergies)
                   .HasMaxLength(300);

            builder.Property(g => g.DietaryPreferences)
                   .HasMaxLength(300);

            builder.Property(g => g.PortionSize)
                   .HasMaxLength(100);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(g => g.User)
                   .WithMany()
                   .HasForeignKey(g => g.UserId);
        }
    }
}
