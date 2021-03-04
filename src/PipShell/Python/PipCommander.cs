using PipShell.Exceptions;
using PipShell.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PipShell.Python
{
    public interface IPipCommander
    {
        Task<string> Execute(string command, CancellationToken cancellationToken = default);
    }

    public class PipCommander : IPipCommander
    {
        public async Task<string> Execute(string command, CancellationToken cancellationToken = default)
        {
            try
            {
                var pInfo = new ProcessStartInfo
                {
                    FileName = "pip",
                    Arguments = command,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                var process = Process.Start(pInfo);
                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                await process.WaitForExitAsync(cancellationToken);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new PipCommandException(command, error);

                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
