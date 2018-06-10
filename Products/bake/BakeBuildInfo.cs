using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class BakeBuildInfo
    {
        public string LastBuild { get; set; }
        public string Commit { get; set; }
        public BakeBuildConfig BuildConfig { get; set; }
    }
}
