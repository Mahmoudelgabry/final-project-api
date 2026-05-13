using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ChatSession : BaseEntity<int>
    {

        public int? UserId { get; set; }

        public User? User { get; set; }

        public string? Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastActive { get; set; }

        public ICollection<ChatMessage> Messages { get; set; }
            = new List<ChatMessage>();
    }
}
