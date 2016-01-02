/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Caching;
using System.IO;

namespace Bam.Net.Server
{
	public class JsCache: TextFileCache
	{
		public JsCache()
		{
			this.FileExtension = ".js";
		}

	}
}
