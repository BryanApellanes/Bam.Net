using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class ServiceResponse<T>: ServiceResponse where T : new()
    {
        public static implicit operator T(ServiceResponse<T> response)
        {
            return response.TypedData();
        }

        public ServiceResponse() { }
        public ServiceResponse(T value)
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

    public class ServiceResponse
    {
        public ServiceResponse() { }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }          
    }
}
