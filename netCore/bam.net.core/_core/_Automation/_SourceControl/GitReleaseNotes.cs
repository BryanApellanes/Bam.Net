using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net.Automation.SourceControl
{
    public partial class GitReleaseNotes
    {
        public GitReleaseNotes(string sinceVersion, string packageId = "")
        {
            PackageId = packageId;
            Bullet = " - ";
            Since = sinceVersion;
            Bullets = new StringBuilder();
        }

        public string Since { get; set; }
    }
}
