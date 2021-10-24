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
	public class YamlFile
	{
		public YamlFile(FileInfo file)
		{
			this.File = file;
		}

		public FileInfo File { get; private set; }

		protected internal YamlSchema Schema
		{
			get;
			set;
		}
		protected internal string Namespace
		{
			get
			{
				if (Schema != null)
				{
					return Schema.GetNamespace(File);
				}

				return "Yaml";
			}
		}
	}
}
