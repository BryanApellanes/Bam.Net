using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories.Tests.TestDtos
{


    [Serializable]
    public class House
    {
        public ulong Id { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; }

        public virtual List<Parent> Parents { get; set; } // many to many
    }
}
