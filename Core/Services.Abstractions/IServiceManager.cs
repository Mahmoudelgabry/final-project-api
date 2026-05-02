namespace Services.Abstractions
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public ICategoryService CategoryService { get; }
        public IRecipeService RecipeService { get; }
        public IFavoriteService FavoriteService { get; }
        public ISavedRecipeService SavedRecipeService { get; }
        public IUserService UserService { get; }
        public IAuthService AuthService { get; }
        public IPostService PostService { get; }

        public IPaymentService PaymentService { get; }

        public ICommentService CommentService { get; }
        public IPostVoteService PostVoteService { get; }
        public ISavedPostService SavedPostService { get; }

        // 🔥 User Profile
        public IUserProfileService UserProfileService { get; }

        public ICartService CartService { get; }

        // 🔥 Order
        public IOrderService OrderService { get; }

        // 🔥 Admin Order
        public IAdminOrderService AdminOrderService { get; }
        public IGhostCraftService GhostCraftService { get; }
    }
}