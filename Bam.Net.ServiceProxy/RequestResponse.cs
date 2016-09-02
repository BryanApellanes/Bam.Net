/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public abstract class RequestResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
		public T DataAs<T>() where T : class
		{
			return Data as T;
		}
        public T DataFromJObject<T>()
        {
            return Data.FromJObject<T>();
        }
    }
}
