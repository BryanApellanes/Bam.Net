/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Caching;

namespace Bam.Net.Server
{
	public class HtmlCache: TextFileCache
	{
		public HtmlCache()
		{
			this.FileExtension = ".html";
		}
	}
}
