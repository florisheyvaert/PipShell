using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipShell.Exceptions
{
    public class PipCommandException : Exception
    {
        public PipCommandException(string command, string error) : base($"Error while executing pip command '{command}' with message '{error}'.")
        {

        }
    }
}
