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
	public partial class YamlEventArgs: EventArgs
	{
		public YamlFile CurrentFile { get; set; }	
	}
}
