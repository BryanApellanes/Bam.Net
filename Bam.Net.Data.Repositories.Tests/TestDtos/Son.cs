using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories.Tests.TestDtos
{

    [Serializable]
    public class Son
    {
        public ulong Id { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; }

        public ulong ParentId { get; set; }
        public virtual Parent Parent { get; set; }
    }
}
