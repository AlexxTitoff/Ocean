using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Library
{
    public class OceanException : Exception
    {
        public OceanException()
        {
        }

        public OceanException(string message) : base(message)
        {
        }

        public OceanException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
