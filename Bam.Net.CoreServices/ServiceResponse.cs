using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data
        {
            get
            {
                return (T)ObjectData;
            }
        }
    }

    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object ObjectData { get; set; }
    }
}
