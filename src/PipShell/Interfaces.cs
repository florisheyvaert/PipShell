using PipShell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PipShell
{
    public interface IPip
    {
        Task<List<Package>> Get(CancellationToken cancellationToken = default);
        Task<Package> Get(string name, CancellationToken cancellationToken = default);
        Task<Package> Update(string name, CancellationToken cancellationToken = default);
        Task<Package> Uninstall(string name, CancellationToken cancellationToken = default);
        Task<Package> Install(string name, CancellationToken cancellationToken = default);
    }
}
