using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property(i => i.QuantityDescription)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.HasOne(i => i.Recipe)
                   .WithMany(r => r.Ingredients)
                   .HasForeignKey(i => i.RecipeId)
                   .OnDelete(DeleteBehavior.Cascade);

          
        }
    }
}
