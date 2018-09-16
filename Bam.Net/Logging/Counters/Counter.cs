using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging.Counters
{
    public class Counter: Stats
    {
        public override object Value
        {
            get
            {
                return Count;
            }
            set
            {
                Count = (long)value;
            }
        }

        public long Count { get; set; }

        public Counter Increment()
        {
            ++Count;
            return this;
        }

        public Counter Decrement()
        {
            --Count;
            return this;
        }
    }
}
