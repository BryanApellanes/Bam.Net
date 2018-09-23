/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
	public class RepositoryEventArgs<T> : RepositoryEventArgs
	{
		public RepositoryEventArgs(T data)
			: base(data)
		{
			this.DataAs = data;
		}

		public RepositoryEventArgs(Exception ex) : base(ex) { }

		public T DataAs { get; set; }
	}

	public class RepositoryEventArgs: EventArgs
	{
		public RepositoryEventArgs() { }
        public RepositoryEventArgs(object data)
        {
            Data = data;
        }
        public RepositoryEventArgs(object data, Type type) : this(data)
        {
            Type = type;
        }
		public RepositoryEventArgs(Exception ex)
		{
			this.Message = ex.Message;
			if (!string.IsNullOrEmpty(ex.StackTrace))
			{
				this.Message = string.Format("{0}:\r\nStackTrace: \t{1}", Message, ex.StackTrace);
			}
		}
        public Type Type { get; set; }
		public object Data { get; private set; }

		public string Message { get; set; }
	}
}
