/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.CommandLine
{
	public class ExecutableDirectory
	{
		public ExecutableDirectory()
		{
			this.ExeExtensions = new string[] { ".exe", ".cmd", ".bat" };
		}

		public ExecutableDirectory(DirectoryInfo directory)
			: this()
		{
			this.Directory = directory;
		}

		public ExecutableDirectory(string path)
			: this(new DirectoryInfo(path))
		{
		}

		DirectoryInfo _directory;
		public DirectoryInfo Directory
		{
			get
			{
				return _directory;
			}
			private set
			{
				_directory = value;
				_executables = null;
			}
		}

		public string[] ExeExtensions
		{
			get;
			set;
		}

		List<string> _executables;
		public string[] Executables
		{
			get
			{
				if (_executables == null)
				{
					_executables = Directory.GetFiles().Where(fi => ExeExtensions.Contains(fi.Extension)).Select(fi => fi.Name).ToList();
				}
				return _executables.ToArray();
			}
		}

		public ProcessOutput Run(string executable, bool promptForAdmin = false, int timeout = 60000)
		{
			Validate(executable);

			string exePath = Path.Combine(Directory.FullName, executable);
			return exePath.Run(promptForAdmin, timeout);
		}

		public ProcessOutput Run(string executable, bool promptForAdmin = false, StringBuilder output = null, StringBuilder error = null,  int timeout = 600000)
		{
			Validate(executable);

			string exePath = Path.Combine(Directory.FullName, executable);
			return exePath.Run(promptForAdmin, output, error, timeout);
		}
		
		private void Validate(string executable)
		{
			if (!_executables.Contains(executable))
			{
				Args.Throw<ArgumentException>("Specified executable was not found {0}", executable);
			}
		}

	}
}
