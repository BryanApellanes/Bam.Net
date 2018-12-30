using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching.Tests.TestData
{

    [Serializable]
    public class TestMonkey : RepoData
    {
        public string Name { get; set; }
    }
}
