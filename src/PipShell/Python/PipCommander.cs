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
        Task Update();
    }

    public class PipCommander : IPipCommander
    {
        private readonly PipOptions _options;

        public PipCommander(PipOptions options)
        {
            _options = options;
        }

        public async Task<string> Execute(string command, CancellationToken cancellationToken = default)
        {
            try
            {
                var pInfo = CreateProcess(command);

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

        public async Task Update()
        {
            try
            {
                var pInfo = CreateProcess("install --upgrade pip", addOptions: false);

                var process = Process.Start(pInfo);
                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();

                await process.WaitForExitAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ProcessStartInfo CreateProcess(string command, bool addOptions = true)
        {
            if (addOptions)
            {
                if (_options.DisablePipVersionCheck)
                    command += $" --disable-pip-version-check";
            }

            return new ProcessStartInfo
            {
                FileName = "pip",
                Arguments = command,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
        }
    }
}
