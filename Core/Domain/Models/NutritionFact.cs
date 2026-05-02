using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class NutritionFact : BaseEntity<int>
    {
        public int ProductId { get; set; }

        public int Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fat { get; set; }
        public decimal Fiber { get; set; }

        public Product Product { get; set; }
    }
}
