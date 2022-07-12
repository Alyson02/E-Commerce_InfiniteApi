using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Infrastructure.Helper
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message, Exception exception) : base(message, exception) { }

    }
}
