using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    public class User : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; } = true;

       

        public ICollection<Order> Orders { get; set; }
        public ICollection<CommunityPost> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }

        
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<SavedRecipe> SavedRecipes { get; set; }
    }
}
