/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ionic.Zip;
using System.Web;
using System.Net;

namespace Bam.Net.Server
{
	/// <summary>
	/// A manager of content files in a bam application
	/// </summary>
	public class ContentManager
	{
		public const string AppsRoot = "apps";
		public const string FileListName = "bamtoolkit_files.txt";
		public ContentManager()
		{
			FileNotFoundHandler = (s) =>
			{
				Console.WriteLine("File not found: {0}", s);
			};
			DirectoryNotFoundHandler = (d) =>
			{
				Console.WriteLine("Directory not found: {0}", d);
			};
		}

		public Action<string> FileNotFoundHandler
		{
			get;
			set;
		}

		public Action<string> DirectoryNotFoundHandler
		{
			get;
			set;
		}

		public void RestoreApplication(string fromZipPath, string applicationName, string toRoot)
		{
			string appRoot = Path.Combine(toRoot, AppsRoot, applicationName);
			BackupIfExists(appRoot);

			ZipFile fromZip = ZipFile.Read(fromZipPath);
			fromZip.Each(entry =>
			{
				entry.Extract(toRoot, ExtractExistingFileAction.DoNotOverwrite);
			});
		}

		public void RestoreToolkit(string fromZipPath, string toRoot)
		{
			BackupIfExists(toRoot);

			ZipFile fromZip = ZipFile.Read(fromZipPath);			
			fromZip.Each(entry =>
			{
				entry.Extract(toRoot, ExtractExistingFileAction.DoNotOverwrite);
			});
		}

		public ZipFile PackServer(string root, Action<string> directoryNotFoundHandler = null)
		{
			ZipFile server = new ZipFile();
			if(directoryNotFoundHandler == null)
			{
				directoryNotFoundHandler = DirectoryNotFoundHandler;
				server = null;
			}

			if (Directory.Exists(root))
			{
				BackupInPlace(root);
				server.AddDirectory(root);
				return server;
			}
			else
			{
				directoryNotFoundHandler(root);
				server = null;
			}

			return server;
		}

		public ZipFile PackApplication(string root, string applicationName)
		{
			return PackApplication(root, applicationName, DirectoryNotFoundHandler);
		}

		public ZipFile PackApplication(string root, string applicationName, Action<string> directoryNotFoundHandler)
		{
			ZipFile application = new ZipFile();
			if (directoryNotFoundHandler == null)
			{
				directoryNotFoundHandler = DirectoryNotFoundHandler;
				application = null;
			}

			DirectoryInfo rootDir = new DirectoryInfo(root);
			if (!rootDir.Exists)
			{
				directoryNotFoundHandler(rootDir.FullName);
				application = null;
			}
			else
			{
				DirectoryInfo applicationDir = new DirectoryInfo(Path.Combine(rootDir.FullName, AppsRoot, applicationName));
				if(!applicationDir.Exists)
				{
					directoryNotFoundHandler(applicationDir.FullName);
					application = null;
				}
				else
				{
					string includeFilePath = Path.Combine(rootDir.FullName, "include.js");
					if (File.Exists(includeFilePath))
					{
						application.AddFile(includeFilePath, "");
					}
					application.AddDirectory(applicationDir.FullName, "");
				}
			}

			return application;
		}

		public ZipFile PackToolkit(string toolkitSourceDir)
		{
			return PackToolkit(toolkitSourceDir, FileNotFoundHandler);
		}
		public ZipFile PackToolkit(string toolkitSourceDir, Action<string> fileNotFoundHandler)
		{
			if (fileNotFoundHandler == null)
			{
				fileNotFoundHandler = FileNotFoundHandler;
			}
			FileInfo fileList = new FileInfo(".\\{0}"._Format(FileListName));
			ZipFile toolkit = new ZipFile();
			string[] lines = File.ReadLines(fileList.FullName).ToArray();
			lines.Each(fsName =>
			{
				if(!string.IsNullOrEmpty(fsName.Trim()))
				{
					string fullPath = Path.Combine(toolkitSourceDir, fsName);
					if (File.Exists(fullPath))
					{
						toolkit.AddFile(fullPath, string.Empty);
					}
					else if (Directory.Exists(fullPath))
					{
						toolkit.AddDirectory(fullPath, fsName);
					}
					else
					{
						FileNotFoundHandler(fullPath);
					}
				}
			});

			return toolkit;
		}

		public void BackupInPlace(string directoryPath)
		{
			DirectoryInfo directory = new DirectoryInfo(directoryPath);
			int num = 0;
			Instant now = new Instant();
			string instantString = "{Month}{Day}{Year}_{Hour}_{Minute}_{Second}_{Millisecond}".NamedFormat(now);
			string directoryTarget = "{0}_{1}_backup"._Format(directory.FullName, instantString);
			while (Directory.Exists(directoryTarget))
			{
				num++;
				directoryTarget = "{0}_{1}_{2}_backup"._Format(directory.FullName, instantString, num);
			}
			directory.Copy(directoryTarget);
		}
		
		private void BackupIfExists(string toRoot)
		{
			if (Directory.Exists(toRoot))
			{
				BackupInPlace(toRoot);
			}
		}

	}
}
