using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Exceptions
{
    public class BaseException : Exception
    {
        public int StatusCode { get; set; }

        public BaseException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
