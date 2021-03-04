using Microsoft.Extensions.DependencyInjection;
using PipShell.Python;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipShell
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPipShell(this IServiceCollection services)
        {
            services.AddTransient<IPip, Pip>();
            services.AddTransient<IPipMapper, PipMapper>();
            services.AddTransient<IPipCommander, PipCommander>();
            return services;
        }
    }
}
