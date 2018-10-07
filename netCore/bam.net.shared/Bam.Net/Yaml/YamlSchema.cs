/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Schema;
using System.Reflection;

namespace Bam.Net.Yaml
{
	public partial class YamlSchema: Loggable
	{

		public DirectoryInfo RootDirectory { get; private set; }		


		public List<YamlDeserializationFailure> Failures
		{
			get;
			private set;
		}

		/// <summary>
		/// Returns the full directory path of the specified file
		/// with the root removed (based on RootDirectory) and 
		/// the path separator changed to "."
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public string GetNamespace(FileInfo file)
		{
			string rootFullName = RootDirectory.FullName;
			string fileFullName = file.Directory.FullName;
			if (!fileFullName.StartsWith(rootFullName))
			{
				throw new InvalidOperationException("The specified file is not under the root of this schema: \r\nSchema Root: {0}\r\nFile Path: {1}"._Format(RootDirectory.FullName, file.FullName));
			}

			string ns = fileFullName.TruncateFront(rootFullName.Length).Replace("\\", ".").Replace("/", ".").PascalCase(true, " ");
			if (ns.StartsWith("."))
			{
				ns = ns.TruncateFront(1);
			}
			return ns;
		}
	}
}
