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
	public class YamlSchema: Loggable
	{
		public YamlSchema(DirectoryInfo root, ILogger logger = null)
			: base()
		{
			this.YamlFiles = new List<YamlFile>();
			this.RootDirectory = root;
			this.Failures = new List<YamlDeserializationFailure>();
			if (logger != null)
			{
				this.Subscribe(logger);
			}
		}

		public DirectoryInfo RootDirectory { get; private set; }
		public List<YamlFile> YamlFiles { get; private set; }
		
		public FileInfo[] Files
		{
			get
			{
				return YamlFiles.Select(yf => yf.File).ToArray();
			}
		}

		public void AddFile(FileInfo file)
		{
			AddFile(new YamlFile(file));
		}

		public void AddFile(YamlFile file)
		{
			YamlFiles.Add(file);
		}

		public void AddFiles(List<FileInfo> files)
		{
			files.Each(file => AddFile(file));
		}

		public void AddFiles(List<YamlFile> files)
		{
			YamlFiles.AddRange(files);
		}

		[Verbosity(VerbosityLevel.Warning)]
		public event EventHandler YamlDeserializationFailed;
		public List<Type> GetDynamicTypes()
		{
			List<Type> dynamicTypes = new List<Type>();
			YamlFiles.Each(new { List = dynamicTypes }, (ctx, yf) =>
			{
				try
				{
					ctx.List.AddRange(yf.DynamicTypes);
				}
				catch (Exception ex)
				{
					FireEvent(YamlDeserializationFailed, new YamlEventArgs { Schema = this, Files = this.Files, CurrentFile = yf, Exception = ex });
					Failures.Add(new YamlDeserializationFailure(yf, ex));
				}
			});

			return dynamicTypes;
		}

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
