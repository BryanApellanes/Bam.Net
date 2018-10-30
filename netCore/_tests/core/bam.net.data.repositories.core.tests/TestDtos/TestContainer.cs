using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories.Tests.TestDtos
{


    [Serializable]
    public class TestContainer
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public virtual Parent[] Parents { get; set; }
    }
}
