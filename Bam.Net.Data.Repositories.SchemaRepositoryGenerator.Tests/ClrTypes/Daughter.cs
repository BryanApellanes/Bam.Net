using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes
{

    [Serializable]
    public class Daughter
    {
        public ulong Id { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; }
        public ulong ParentId { get; set; }
        public Parent Parent { get; set; }
    }
}
