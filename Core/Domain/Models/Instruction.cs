using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Instruction : BaseEntity<int>
    {
        public string Step { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
