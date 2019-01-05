using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Automation.Nuget;

namespace Bam.Net.Automation.SourceControl
{
    public partial class GitReleaseNotes
    {
        public GitReleaseNotes(string sinceVersion, string packageId = "")
        {
            PackageId = packageId;
            Bullet = " - ";
            Since = new PackageVersion(sinceVersion);
            Bullets = new StringBuilder();
        }

        public PackageVersion Since { get; set; }
    }
}
