using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ChatMessage : BaseEntity<int>
    {
        

        public int SessionId { get; set; }

        public ChatSession Session { get; set; }

        public string Role { get; set; }

        public string Content { get; set; }

        public string? Intent { get; set; }

        public double? Confidence { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
