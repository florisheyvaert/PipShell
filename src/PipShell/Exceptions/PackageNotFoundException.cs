using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipShell.Exceptions
{
    public class PackageNotFoundException : Exception
    {
        public PackageNotFoundException(string package) : base($"'{package}' is not found")
        {

        }
    }
}
