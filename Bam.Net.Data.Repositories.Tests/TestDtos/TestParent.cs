using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories.Tests.TestDtos
{
    [Serializable]
    public class TestParent
    {
        public ulong Id { get; set; }
        public TestChild[] Childs { get; set; }
    }
}
