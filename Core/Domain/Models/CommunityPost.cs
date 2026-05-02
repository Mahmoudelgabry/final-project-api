using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    public class CommunityPost : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostVote> Votes { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<SavedPost> SavedPosts { get; set; }
    }
}
