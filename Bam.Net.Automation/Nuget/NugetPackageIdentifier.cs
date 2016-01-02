/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.Nuget
{
    public class NugetPackageIdentifier
    {
        public NugetPackageIdentifier(string id)
        {
            this.Id = id;
        }

        public NugetPackageIdentifier(string id, string version)
            : this(id)
        {
            this.Version = version;
        }

        public string Id { get; set; }
        public string Version { get; set; }
    }
}
