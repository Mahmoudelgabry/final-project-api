using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public bool IsSaved { get; set; }
        public NutritionFactDto NutritionFact { get; set; }
    }
}
