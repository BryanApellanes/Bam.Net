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
    public abstract class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Relevant data returned in response
        /// to a request
        /// </summary>
        public object Data { get; set; }
		public T DataAs<T>() where T : class
		{
			return Data as T;
		}
        public T DataFromJObject<T>()
        {
            return Data.FromJObject<T>();
        }

        public T DataTo<T>()
        {
            return Data.ToJson().FromJson<T>();
        }
    }
}
