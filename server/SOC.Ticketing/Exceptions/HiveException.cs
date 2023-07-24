using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Ticketing.Exceptions
{
    public class HiveException : Exception
    {
        public HiveException(string? message) : base(message)
        {
        }
    }
}
