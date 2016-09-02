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

namespace Bam.Net.Caching
{
	/// <summary>
	/// A caching mechanism for text files. 
	/// </summary>
	public class TextFileCache: Cache
	{
		public TextFileCache() { this.FileExtension = ".txt"; }

		public string FileExtension { get; protected set; }
		public static void RegisterWithCacheManager()
		{
			CacheManager.Default.CacheFor<FileInfo>(new TextFileCache());
		}        
		public override CacheItem Add(object value)
		{
			Args.ThrowIfNull(value, "value");

			string toAdd = value as string;
			if(string.IsNullOrEmpty(toAdd))
			{
				FileInfo file = value as FileInfo;
				if (file != null)
				{
					toAdd = File.ReadAllText(file.FullName);
				}
				else
				{
					throw new NotSupportedException("The specified object of type ({0}) is not supported by the {1}"._Format(value.GetType().Name, typeof(TextFileCache).Name));
				}
			}
			return base.Add((object)toAdd);
		}

		public void AddFile(string path)
		{
			AddFile(new FileInfo(path));
		}
		public CacheItem AddFile(FileInfo file)
		{
			if (file.Extension.ToLowerInvariant().Equals(FileExtension))
			{
				return Add((object)file);
			}
			return null;
		}

		public List<CacheItem> AddDirectory(string path)
		{
			return AddDirectory(new DirectoryInfo(path));
		}
		public List<CacheItem> AddDirectory(DirectoryInfo directory)
		{
			return AddDirectory(directory, FileExtension);
		}
		public List<CacheItem> AddDirectory(DirectoryInfo directory, string fileExtension)
		{
			List<CacheItem> results = new List<CacheItem>();
			foreach (FileInfo file in directory.GetFiles("*{0}"._Format(fileExtension)))
			{
				results.Add(AddFile(file));
			}

			return results;
		}
	}
}
