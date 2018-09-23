using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public class Comparer<T> : IComparer<T>
    {
        public Comparer(Func<T, T, int> implementation)
        {
            Implementation = implementation;
        }

        public Func<T, T, int> Implementation{ get; set; }

        public int Compare(T x, T y)
        {
            return Implementation(x, y);
        }
    }
}
