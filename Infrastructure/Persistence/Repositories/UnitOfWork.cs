using Domain.Contracts;
using System.Collections.Concurrent;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private readonly ConcurrentDictionary<string, object> _properties;

        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IRecipeRepository> _recipeRepository;
        private readonly Lazy<IFavoriteRepository> _favoriteRepository;
        private readonly Lazy<ISavedRecipeRepository> _savedRecipeRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IPostRepository> _postRepository;

        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;

        // 🔥 NEW
        private readonly Lazy<IUserProfileRepository> _userProfileRepository;

        private readonly Lazy<ICommentRepository> _commentRepository;
        private readonly Lazy<IPostVoteRepository> _postVoteRepository;
        private readonly Lazy<ISavedPostRepository> _savedPostRepository;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _properties = new();

            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(storeContext));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(storeContext));
            _recipeRepository = new Lazy<IRecipeRepository>(() => new RecipeRepository(storeContext));
            _favoriteRepository = new Lazy<IFavoriteRepository>(() => new FavoriteRepository(storeContext));
            _savedRecipeRepository = new Lazy<ISavedRecipeRepository>(() => new SavedRecipeRepository(storeContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(storeContext));
            _postRepository = new Lazy<IPostRepository>(() => new PostRepository(storeContext));

            _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(storeContext));

            // 🔥 Order
            _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(storeContext));

            // 🔥 UserProfile
            _userProfileRepository = new Lazy<IUserProfileRepository>(() => new UserProfileRepository(storeContext));

            _commentRepository = new Lazy<ICommentRepository>(() => new CommentRepository(storeContext));
            _postVoteRepository = new Lazy<IPostVoteRepository>(() => new PostVoteRepository(storeContext));
            _savedPostRepository = new Lazy<ISavedPostRepository>(() => new SavedPostRepository(storeContext));
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            => (IGenericRepository<TEntity, TKey>)
               _properties.GetOrAdd(typeof(TEntity).FullName!,
                   _ => new GenericRepository<TEntity, TKey>(_storeContext));

        public IProductRepository ProductRepository => _productRepository.Value;
        public ICategoryRepository CategoryRepository => _categoryRepository.Value;
        public IRecipeRepository RecipeRepository => _recipeRepository.Value;
        public IFavoriteRepository FavoriteRepository => _favoriteRepository.Value;
        public ISavedRecipeRepository SavedRecipeRepository => _savedRecipeRepository.Value;
        public IUserRepository UserRepository => _userRepository.Value;
        public IPostRepository PostRepository => _postRepository.Value;

        public ICartRepository CartRepository => _cartRepository.Value;
        public IOrderRepository OrderRepository => _orderRepository.Value;

        // 🔥 NEW
        public IUserProfileRepository UserProfileRepository => _userProfileRepository.Value;

        public ICommentRepository CommentRepository => _commentRepository.Value;
        public IPostVoteRepository PostVoteRepository => _postVoteRepository.Value;
        public ISavedPostRepository SavedPostRepository => _savedPostRepository.Value;

        public async Task<int> SaveChangesAsync() => await _storeContext.SaveChangesAsync();
    }
}