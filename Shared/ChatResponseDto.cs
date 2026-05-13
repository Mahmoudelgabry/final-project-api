using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ChatResponseDto
    {
        public string Response_Text { get; set; }

        public string Intent { get; set; }

        public double Intent_Confidence { get; set; }
    }
}
