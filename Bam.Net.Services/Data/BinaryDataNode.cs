using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Data
{
    public class BinaryDataNode: DataNode
    {
        public DataNode LeftChild { get; set; }
        public DataNode RightChild { get; set; }
        public override IEnumerable<DataNode> Children
        {
            get
            {
                yield return LeftChild;
                yield return RightChild;
            }
        }

        public override void Add(IComparable value)
        {
            if (Value == null)
            {
                Value = value;
            }
            else
            {
                int compareValue = value.CompareTo(Value);
                if (compareValue == -1)
                {
                    if (LeftChild == null)
                    {
                        LeftChild = new BinaryDataNode { Value = value };
                    }
                    else
                    {
                        LeftChild.Add(value);
                    }
                }
                else if (compareValue == 1)
                {
                    if (RightChild == null)
                    {
                        RightChild = new BinaryDataNode { Value = value };
                    }
                    else
                    {
                        RightChild.Add(value);
                    }
                }
            }
        }
    }
}
