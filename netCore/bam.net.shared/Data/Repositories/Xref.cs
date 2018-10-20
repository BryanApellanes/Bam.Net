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
	public class Xref
	{
		public Xref() { }
		public Xref(object left, object right)
		{
			this.Left = left;
			this.Right = right;
		}

		public object Left { get; private set; }
		public object Right { get; private set; }
	}
}
