using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class FavoriteProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ImageUrl { get; set; }
        public bool IsSaved { get; set; }

        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
