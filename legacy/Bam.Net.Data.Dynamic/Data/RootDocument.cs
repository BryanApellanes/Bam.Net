using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic.Data
{
    /// <summary>
    /// Represents the original document used
    /// to generate dynamic types.  Not all
    /// DynamicTypeDescriptors will have a 
    /// RootDocument.
    /// </summary>
    [Serializable]
    public class RootDocument: RepoData
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public string ContentHash { get; set; }
    }
}
