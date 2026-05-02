using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class GhostCraftResultDto
    {
        public int Id { get; set; }

        public string DishDescription { get; set; }

        public List<string> Allergies { get; set; }
        public List<string> DietaryPreferences { get; set; }

        public int SpicinessLevel { get; set; }
        public int SaltinessLevel { get; set; }

        public string PortionSize { get; set; }
        public string SpecialInstructions { get; set; }

        public string Status { get; set; }
    }
}
