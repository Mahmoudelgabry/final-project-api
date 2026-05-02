using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configurations
{
    public class SavedRecipeConfiguration : IEntityTypeConfiguration<SavedRecipe>
    {
        public void Configure(EntityTypeBuilder<SavedRecipe> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(u=>u.SavedRecipes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Recipe)
                .WithMany(r => r.SavedRecipes)
                .HasForeignKey(s => s.RecipeId);


            builder.HasIndex(x => new { x.UserId, x.RecipeId })
                .IsUnique();
        }
    }
}
