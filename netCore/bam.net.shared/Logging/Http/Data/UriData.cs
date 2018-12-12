using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging.Http.Data
{
    [Serializable]
    public class UriData: RepoData
    {
        public string Scheme { get; set; }
        //public 
        public static UriData FromUri(Uri uri)
        {
            return new UriData
            {

            };
        }
    }
}
