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
        public NugetPackageIdentifier() { }

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

        public override int GetHashCode()
        {
            return $"{Id}{Version}".GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is NugetPackageIdentifier npi)
            {
                return npi.Id.Equals(Id) && npi.Version.Equals(Version);
            }
            return false;
        }
    }
}
