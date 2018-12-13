using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging.Http.Data
{
    [Serializable]
    public class UserData: RepoData
    {
        public string UserName { get; set; }
        public string RequestCuid { get; set; }
    }
}
