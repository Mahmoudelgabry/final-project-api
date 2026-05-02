using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
