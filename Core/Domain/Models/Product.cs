using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public NutritionFact NutritionFact { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public ICollection<Favorite> Favorites { get; set; }
    }
}