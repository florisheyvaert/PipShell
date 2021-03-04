using PipShell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PipShell.Python
{
    public interface IPipMapper
    {
        List<Package> MapPackagesFromFreeze(string input);
        Package MapPackageFromShow(string input);
    }

    public class PipMapper : IPipMapper
    {
        public List<Package> MapPackagesFromFreeze(string input)
        {
            var result = new List<Package>();
            foreach (var line in input.Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var split = line.Split("==");
                result.Add(new Package()
                {
                    Name = split[0],
                    Version = split[1]
                });
            }
            return result;
        }

        public Package MapPackageFromShow(string input)
        {
            var package = new Package();
            var nameRegex = new Regex("(?<=Name: ).*");
            var nameMatch = nameRegex.Match(input);
            if (nameMatch.Success)
                package.Name = nameMatch.Value.Replace("\r", string.Empty);
            var versionRegex = new Regex("(?<=Version: ).*");
            var versionMatch = versionRegex.Match(input);
            if (versionMatch.Success)
                package.Version = versionMatch.Value.Replace("\r", string.Empty);
            return package;
        }
    }
}
