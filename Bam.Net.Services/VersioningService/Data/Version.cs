using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.VersioningService.Data
{
    public class Version: RepoData
    {
        /// <summary>
        /// A unique identifier, typically the result
        /// of a hashing function like SHA1 or SHA256
        /// </summary>
        public string Identifier { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }
        public string Build { get; set; }

        public string AssemblyPath { get; set; }
    }
}
