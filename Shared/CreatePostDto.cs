using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public List<int> TagIds { get; set; }
    }
}
