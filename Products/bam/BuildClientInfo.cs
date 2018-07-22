using Bam.Net.Services.Clients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    [Serializable]
    public class BuildClientInfo: BamClientInfo
    {
        public BuildClientInfo()
        {
            CoreHostName = "core.bamapps.net";
            CorePort = 80;
            BambotHost = "bambot.bamapps.net";
            BuildKeyFile = "\\\\core\\share\\buildkey.txt";
        }

        public string BambotHost { get; set; }

        /// <summary>
        /// Gets or sets the build key file.  This file contains
        /// secret text accessible only to bam.exe and bambot.
        /// This ensures that only authorized internal callers
        /// can interact with bambot.bamapps.net.
        /// </summary>
        /// <value>
        /// The build key file.
        /// </value>
        public string BuildKeyFile { get; set; }

        [JsonIgnore]
        public string BuildKey
        {
            get
            {
                return BuildKeyFile.SafeReadFile();
            }
        }
    }
}
