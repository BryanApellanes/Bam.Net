using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Yaml.Tests.TestClasses
{
    [Serializable]
    public class House: FsRepoData
    {
        public string HouseDetails { get; set; }
        public virtual List<Parent> Parents { get; set; }
    }
}
