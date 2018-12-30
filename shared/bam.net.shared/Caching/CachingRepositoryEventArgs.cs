using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching
{
    public class CachingRepositoryEventArgs: EventArgs
    {
        public string PropertyName { get; set; }
        public object ParameterValue { get; set; }
    }
}
