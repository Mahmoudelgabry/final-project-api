using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<string> Tags { get; set; }
        public int? CurrentUserVote { get; set; }
        public int Votes { get; set; }
        public bool IsSaved { get; set; }
        public int CommentsCount { get; set; }
    }
}
