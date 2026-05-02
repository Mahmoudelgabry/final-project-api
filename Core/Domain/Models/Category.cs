using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public CategoryType Type { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
