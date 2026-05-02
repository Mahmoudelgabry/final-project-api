using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Recipe : BaseEntity<int>
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public string DifficultyLevel { get; set; }
        public int PrepTime { get; set; }
        public int Servings { get; set; }

   

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();
        public ICollection<SavedRecipe> SavedRecipes { get; set; }  
    }
}
