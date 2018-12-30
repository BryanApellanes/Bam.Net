using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Yaml.Tests.TestClasses
{
    [Serializable]
    public class Parent: FsRepoData
    {
        public string Details { get; set; }
        public virtual List<House> Houses { get; set; }
        public virtual List<Child> Children { get; set; }
    }
}
