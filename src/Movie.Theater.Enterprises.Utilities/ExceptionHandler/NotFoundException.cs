using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Theater.Enterprises.Utilities.ExceptionHandler
{
    /// <summary>
    /// class to handle not found exceptions
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

}
