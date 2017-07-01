using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Data
{
    public abstract class DataNode
    {
        public long Key { get; set; }
        public DataNode Parent { get; set; }
        public long ParentKey { get; set; }
        public string Name { get; set; }
        public IComparable Value { get; set; }

        public abstract IEnumerable<DataNode> Children { get; }
        public abstract void Add(IComparable value);
    }
}
