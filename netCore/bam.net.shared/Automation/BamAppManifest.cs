using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public class BamAppManifest
    {
        public string AppName { get; set; }
        public string[] DirectoryNames { get; set; }
        /// <summary>
        /// The names of all the files required for the 
        /// current Bam Application without path information
        /// </summary>
        public string[] FileNames { get; set; }

        /// <summary>
        /// The hash of the files in case of name collisions.  May
        /// be null or empty.
        /// </summary>
        public Dictionary<string, string> RequiredHashes { get; set; }
    }
}
