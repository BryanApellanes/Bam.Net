using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Binary
{
    [Serializable]
    public class BinaryRequest<T>: BinaryRequest
    {
        public T Message { get; set; }
    }

    [Serializable]
    public class BinaryRequest
    {
        public object Message { get; set; }
    }
}
