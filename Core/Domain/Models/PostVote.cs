using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PostVote : BaseEntity<int>
    {
        public int PostId { get; set; }
        public CommunityPost Post { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int VoteType { get; set; }
    }
}
