

namespace Shared
{
    public class SavedRecipeDto
    {
        public int RecipeId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int PrepTime { get; set; }

        public string DifficultyLevel { get; set; }
        public bool IsSaved { get; set; }
        public string CategoryName { get; set; }
    }
}
