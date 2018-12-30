using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging.Http.Data
{
    [Serializable]
    public class HeaderData: RepoData
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
