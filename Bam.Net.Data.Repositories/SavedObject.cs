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
	public class SavedObject
	{
		public Type Type { get; set; }
		public byte[] Value { get; set; }
	}
}
