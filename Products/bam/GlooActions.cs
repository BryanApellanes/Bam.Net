using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;

namespace bam
{
    [Serializable]
    public class GlooActions: CommandLineTestInterface
    {
        [ConsoleAction("gloo", "Download glue client")]
        public static void DownloadGlooClient()
        {
            Out("Download not implemented", ConsoleColor.DarkRed);
        }
    }
}
