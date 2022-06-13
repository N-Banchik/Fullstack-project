using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ErrorHandling
{
    public class UnauthorizedExtention:Exception
    {
        public UnauthorizedExtention(string message)
        : base(message)
        {
        }
        public UnauthorizedExtention(string message, Exception inner)
       : base(message, inner)
        {
        }
    }
}
