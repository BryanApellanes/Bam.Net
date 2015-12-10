/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Distributed
{
	public abstract class Operation
	{
		/// <summary>
		/// The
		/// </summary>
		public string Originator { get; set; }
		public Operation()
		{
			this.Uuid = System.Guid.NewGuid().ToString();
		}

		public string Uuid { get; set; }

		public T Execute<T>(ComputeNode nodeToExecuteOn)
		{
			return (T)Execute();
		}

		public abstract object Execute();
		
	}
}
