using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Theater.Enterprises.Utilities.ExceptionHandler
{
    /// <summary>
    /// class to handle service unavailable exceptions
    /// </summary>
    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException(string message) : base(message)
        {
        }
    }

}
