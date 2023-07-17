using Microsoft.Extensions.DependencyInjection;
using PipShell.Python;
using System;
using System.Threading.Tasks;

namespace PipShell.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new PipOptions() { DisablePipVersionCheck = true, IgnoreRootUserAction = true };
            var commander = new PipCommander(options);
            var mapper = new PipMapper();
            var pip = new Pip(commander, mapper);

            await commander.Update();

            var packages = await pip.Get();
            //var package = await pip.Get("selenium");
            //var package123 = await pip.Get("selenium123");
            await pip.Install("SaopBerry");
            await pip.Uninstall("SaopBerry");
        }
    }
}
