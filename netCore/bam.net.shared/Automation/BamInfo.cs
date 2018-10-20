using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public class BamInfo
    {
        public string Authors { get; set; }
        public string Owners { get; set; }
        public string LicenseUrl { get; set; }
        public string ProjectUrl { get; set; }
        public string ReleaseNotes { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int PatchVersion { get; set; }
        public string BuildNumber { get; set; }

        public string VersionString
        {
            get
            {
                string buildNumber = !string.IsNullOrEmpty(BuildNumber) ? "-{0}"._Format(BuildNumber) : "";
                string patch = string.Format("{0}{1}", PatchVersion.ToString(), buildNumber);
                return "{0}.{1}.{2}"._Format(MajorVersion, MinorVersion, patch);
            }
            set
            {
                string[] split = value.DelimitSplit(".", "-");
                Args.ThrowIf<ArgumentException>(split.Length < 3 || split.Length > 4, "Invalid version string specified ({0})", value);
                MajorVersion = int.Parse(split[0]);
                MinorVersion = int.Parse(split[1]);
                PatchVersion = int.Parse(split[2]);
                if(split.Length == 4)
                {
                    BuildNumber = split[3];
                }
            }
        }
    }
}
