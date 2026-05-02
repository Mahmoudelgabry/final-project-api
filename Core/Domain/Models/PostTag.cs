using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PostTag : BaseEntity<int>
    {
        public int PostId { get; set; }
        public CommunityPost Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
