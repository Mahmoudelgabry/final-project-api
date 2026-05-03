using Microsoft.EntityFrameworkCore;
using Domain.Models;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options)
    : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<NutritionFact> NutritionFacts { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<CommunityPost> CommunityPosts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostVote> PostVotes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<SavedPost> SavedPosts { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<GhostCraftOrder> GhostCraftOrders { get; set; }
    public DbSet<Instruction> Instructions { get; set; }
    public DbSet<SavedRecipe> SavedRecipes { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    // 🔥 Payment System
    public DbSet<Payment> Payments { get; set; }
    public DbSet<SavedPaymentMethod> SavedPaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
    }

}
