using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Ingredient : BaseEntity<int>
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }



        public string QuantityDescription { get; set; }
    }
}
