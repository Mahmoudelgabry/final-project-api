using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CreateRecipeDto
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string DifficultyLevel { get; set; }
        public int PrepTime { get; set; }
        public int Servings { get; set; }
        
        public List<CreateInstructionsDto> Instructions { get; set; }
        public int CategoryId { get; set; }

        public List<CreateIngredientDto> Ingredients { get; set; }
        
    }
}
