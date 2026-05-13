using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ChatRequestDto
    {
        public string Message { get; set; }

        public List<ChatHistoryItemDto> History { get; set; } = [];

        public object? Filters { get; set; }

        public string? User_Id { get; set; }

        public string? Language { get; set; }
    }
}
