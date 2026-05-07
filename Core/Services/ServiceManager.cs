using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IRecipeService> _recipeService;
        private readonly Lazy<IFavoriteService> _favoriteService;
        private readonly Lazy<ISavedRecipeService> _savedRecipeService;
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IPostService> _postService;
        private readonly Lazy<IUserProfileService> _userProfileService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<ICartService> _cartService;
        // 🔥 Order (User)
        private readonly Lazy<IOrderService> _orderService;

        // 🔥 Admin Order
        private readonly Lazy<IAdminOrderService> _adminOrderService;

        private readonly Lazy<ICommentService> _commentService;
        private readonly Lazy<IPostVoteService> _postVoteService;
        private readonly Lazy<ISavedPostService> _savedPostService;
        private readonly Lazy<IGhostCraftService> _ghostCraftService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(unitOfWork, mapper));
            _recipeService = new Lazy<IRecipeService>(() => new RecipeService(unitOfWork, mapper));
            _favoriteService = new Lazy<IFavoriteService>(() => new FavoriteService(unitOfWork, mapper));
            _savedRecipeService = new Lazy<ISavedRecipeService>(() => new SavedRecipeService(unitOfWork, mapper));
            _authService = new Lazy<IAuthService>(() => new AuthService(unitOfWork));
            _userService = new Lazy<IUserService>(() => new UserService(unitOfWork, mapper));

            _postService = new Lazy<IPostService>(() => new PostService(unitOfWork, mapper));

            // 🔥 User Profile
            _userProfileService = new Lazy<IUserProfileService>(() => new UserProfileService(unitOfWork, mapper));

            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(unitOfWork, mapper));

            // 🔥 Cart الأول
            _cartService = new Lazy<ICartService>(() => new CartService(unitOfWork, mapper));

            // 🔥 Order بياخد CartService
            _orderService = new Lazy<IOrderService>(() =>
                new OrderService(unitOfWork, mapper)
            );

            // 🔥 Admin Order
            _adminOrderService = new Lazy<IAdminOrderService>(() => new AdminOrderService(unitOfWork, mapper));

            _commentService = new Lazy<ICommentService>(() => new CommentService(unitOfWork, mapper));
            _postVoteService = new Lazy<IPostVoteService>(() => new PostVoteService(unitOfWork.PostVoteRepository, unitOfWork));
            _savedPostService = new Lazy<ISavedPostService>(() => new SavedPostService(unitOfWork, mapper));
            _ghostCraftService = new Lazy<IGhostCraftService>(() => new GhostCraftService(unitOfWork, mapper));
        }

        public IProductService ProductService => _productService.Value;
        public ICategoryService CategoryService => _categoryService.Value;
        public IRecipeService RecipeService => _recipeService.Value;
        public IFavoriteService FavoriteService => _favoriteService.Value;
        public ISavedRecipeService SavedRecipeService => _savedRecipeService.Value;
        public IUserService UserService => _userService.Value;
        public IAuthService AuthService => _authService.Value;
        public IPostService PostService => _postService.Value;

        // 🔥 User Profile
        public IUserProfileService UserProfileService => _userProfileService.Value;

        public IPaymentService PaymentService => _paymentService.Value;
        public ICartService CartService => _cartService.Value;

        public IOrderService OrderService => _orderService.Value;
        public IAdminOrderService AdminOrderService => _adminOrderService.Value;

        public ICommentService CommentService => _commentService.Value;
        public IPostVoteService PostVoteService => _postVoteService.Value;
        public ISavedPostService SavedPostService => _savedPostService.Value;
        public IGhostCraftService GhostCraftService => _ghostCraftService.Value;
    }
}