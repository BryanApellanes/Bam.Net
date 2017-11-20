using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Binary
{
    [Serializable]
    public class BinaryResponse<T>: BinaryResponse
    {
        public new T Data { get; set; }
    }

    [Serializable]
    public class BinaryResponse
    {
        public object Data { get; set; }
    }
}
