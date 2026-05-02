using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class NutritionFactConfiguration : IEntityTypeConfiguration<NutritionFact>
    {
        public void Configure(EntityTypeBuilder<NutritionFact> builder)
        {
            builder.HasOne(n => n.Product)
                   .WithOne(p => p.NutritionFact)
                   .HasForeignKey<NutritionFact>(n => n.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(n => n.Protein)
                   .HasColumnType("decimal(5,2)");

            builder.Property(n => n.Carbs)
                   .HasColumnType("decimal(5,2)");

            builder.Property(n => n.Fat)
                   .HasColumnType("decimal(5,2)");

            builder.Property(n => n.Fiber)
                   .HasColumnType("decimal(5,2)");
        }
    }
}
