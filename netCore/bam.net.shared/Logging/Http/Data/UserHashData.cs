using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging.Http.Data
{
    [Serializable]
    public class UserHashData : RepoData
    {
        /// <summary>
        /// Gets or sets the user hash identifier which is the SHA512 hash of the user's username 
        /// converted to ulong.
        /// </summary>
        public ulong UserNameHash { get; set; }
        public string UserName { get; set; }
    }
}
