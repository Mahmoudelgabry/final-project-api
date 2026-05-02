using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SavedPost : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public CommunityPost Post { get; set; }

        public DateTime CreatedAt { get; set; } 
    }
}
