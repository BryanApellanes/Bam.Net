/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Yaml
{
	public class YamlDeserializationFailure
	{
		public YamlDeserializationFailure(YamlFile file, Exception ex)
		{
			this.File = file;
			this.Exception = ex;
		}

		public YamlFile File { get; private set; }
		public Exception Exception { get; private set; }
	}
}
