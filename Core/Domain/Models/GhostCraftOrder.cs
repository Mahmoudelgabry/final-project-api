using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GhostCraftOrder : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string DishDescription { get; set; }
        public string Allergies { get; set; }
        public string DietaryPreferences { get; set; }

        public int SpicinessLevel { get; set; }
        public int SaltinessLevel { get; set; }

        public string PortionSize { get; set; }
        public string SpecialInstructions { get; set; }

        public decimal Price { get; set; }
        
    }
}
