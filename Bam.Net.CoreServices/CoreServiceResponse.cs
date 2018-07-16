using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class CoreServiceResponse<T>: CoreServiceResponse where T : new()
    {
        public static implicit operator T(CoreServiceResponse<T> response)
        {
            return response.TypedData();
        }

        public CoreServiceResponse() { }
        public CoreServiceResponse(T value)
        {
            Data = value;
            TypedData(value);
        }

        T _typedData;
        public T TypedData(T typedData = default(T))
        {
            if (typedData != null)
            {
                _typedData = typedData;
            }
            return _typedData;
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

    public class CoreServiceResponse
    {
        public CoreServiceResponse() { }

        public CoreServiceResponse(object data)
        {
            Data = data;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }          
    }
}
