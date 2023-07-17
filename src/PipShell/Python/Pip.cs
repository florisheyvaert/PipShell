using PipShell.Exceptions;
using PipShell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PipShell.Python
{
    public class Pip : IPip
    {
        private readonly IPipCommander _pipCommander;
        private readonly IPipMapper _pipMapper;

        public Pip(IPipCommander pipCommander, IPipMapper pipMapper)
        {
            _pipCommander = pipCommander;
            _pipMapper = pipMapper;
        }

        public async Task<List<Package>> Get(CancellationToken cancellationToken = default)
        {
            var output = await _pipCommander.Execute("freeze", cancellationToken);
            var mapped = _pipMapper.MapPackagesFromFreeze(output);
            return mapped;
        }

        public async Task<Package> Get(string name, CancellationToken cancellationToken = default)
        {
            try
            {
                var output = await _pipCommander.Execute($"show {name}", cancellationToken);
                var mapped = _pipMapper.MapPackageFromShow(output);
                return mapped;
            }
            catch (PipCommandException)
            {
                throw new PackageNotFoundException(name);
            }
        }

        public async Task<Package> Update(string name, CancellationToken cancellationToken = default)
        {
            var package = await Get(name, cancellationToken);
            await _pipCommander.Execute($"install {package.Name} --upgrade", cancellationToken);
            return await Get(name, cancellationToken);
        }

        public async Task<Package> Install(string name, CancellationToken cancellationToken = default)
        {
            await _pipCommander.Execute($"install \"{name}\"", cancellationToken);
            return await Get(name, cancellationToken);
        }

        public async Task<Package> Uninstall(string name, CancellationToken cancellationToken = default)
        {
            var package = await Get(name, cancellationToken);
            await _pipCommander.Execute($"uninstall -y {package.Name}=={package.Version}", cancellationToken);
            return package;
        }
    }
}
