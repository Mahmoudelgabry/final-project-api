using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>;

        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IRecipeRepository RecipeRepository { get; }
        IFavoriteRepository FavoriteRepository { get; }
        IPostRepository PostRepository { get; }
        IUserRepository UserRepository { get; }
        ISavedRecipeRepository SavedRecipeRepository { get; }

        // 🔥 بتاعك
        ICartRepository CartRepository { get; }
        IOrderRepository OrderRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }

        // 🔥 بتاع صحابك
        ICommentRepository CommentRepository { get; }
        IPostVoteRepository PostVoteRepository { get; }
        ISavedPostRepository SavedPostRepository { get; }
    }
}