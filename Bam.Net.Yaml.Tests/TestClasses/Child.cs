using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Yaml.Tests.TestClasses
{
    [Serializable]
    public class Child: FsRepoData
    {
        public string ChildDetails { get; set; }
        public long ParentId { get; set; }
        public Parent Parent { get; set; }
    }
}
