/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Data;
using Bam.Net;

namespace Bam.Net.CoreServices.Distributed
{
	public class DistributedObjectReaderWriter: ObjectReaderWriter
	{
		public DistributedObjectReaderWriter(string rootDirectory)
			: base(rootDirectory)
		{ }
	}
}
