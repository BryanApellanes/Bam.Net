using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class ServiceResponse<T>: ServiceResponse where T : new()
    {
        public static implicit operator T(ServiceResponse<T> response)
        {
            return response.TypedData();
        }

        public T TypedData()
        {
            T t = new T();
            t.CopyProperties(Data);            
            return t;
        }

        public O Cast<O>()
        {
            return (O)Data;
        }

        public A[] ToArray<A>() where A : new()
        {
            return Cast<object[]>().CopyAs<A>().ToArray();
        }
    }

    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }    
    }
}
