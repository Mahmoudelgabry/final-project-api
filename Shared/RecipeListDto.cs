using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class RecipeListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string DifficultyLevel { get; set; }
        public int PrepTime { get; set; }
        public bool IsSaved { get; set; }
        public int Servings { get; set; }
        public string CategoryName { get; set; }
    }
}
