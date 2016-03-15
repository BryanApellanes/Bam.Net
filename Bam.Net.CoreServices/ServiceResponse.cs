using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public T DataAs<T>() where T : new()
        {
            T t = new T();
            t.CopyProperties(Data);
            return t;
        }
    }
}
