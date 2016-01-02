/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

using Bam.Net.Caching;
using Bam.Net.Testing;
using Bam.Net.CommandLine;
using System.IO;

namespace Bam.Net.Caching.Tests
{
	[Serializable]
	public class UnitTests: CommandLineTestInterface
	{
		[UnitTest]
		public void RegisterTextFileCacheManagerTest()
		{
			CacheManager.Default.Clear();
			Cache fileCache = CacheManager.Default.CacheFor<FileInfo>();
			Type typeBeforeRegistering = fileCache.GetType();
			TextFileCache.RegisterWithCacheManager();
			fileCache = CacheManager.Default.CacheFor<FileInfo>();
			Type checkType = fileCache.GetType();
			Expect.IsFalse(typeBeforeRegistering.Equals(checkType));
			Expect.AreEqual(typeof(TextFileCache), checkType);
		}

		public class CantCacheAsTextFile
		{

		}
		[UnitTest]
		public void TryingToCacheSomethingOtherThanStringOrFileShouldThrow()
		{
			TextFileCache.RegisterWithCacheManager();
			Cache textFileCache = CacheManager.Default.CacheFor<FileInfo>();
			Expect.Throws(() =>
			{
				CantCacheAsTextFile test = new CantCacheAsTextFile();
				textFileCache.Add(test);
			}, "Exception wasn't thrown as expected");
		}

		[UnitTest]
		public void FetchFromCacheShouldBeFasterThanFromFile()
		{
			TextFileCache.RegisterWithCacheManager();
			string testFilePath = ".\\TestFile1.txt";
			FileInfo testFile = new FileInfo(testFilePath);
			Cache textFileCache = CacheManager.Default.CacheFor<FileInfo>();
			textFileCache.MaxBytes = File.ReadAllBytes(testFilePath).Length * 2;
			textFileCache.Subscribe(new ConsoleLogger());
			CacheItem cacheItem = textFileCache.Add(testFile);			

			Func<string, string> readFromFile = (filePath) =>
			{
				string txtFromFile;
				using (StreamReader sr = new StreamReader(filePath))
				{
					txtFromFile = sr.ReadToEnd();
				}
				return txtFromFile;
			};
			Func<dynamic, string> readFromCache = (context) =>
			{
				TextFileCache cache = context.TextFileCache;
				CacheItem item = context.CacheItem;
				return cache.Retrieve(item.Id).ValueAs<string>();
			};
			string stringFromFile;
			TimeSpan fromFileTime = readFromFile.TimeExecution<string, string>(testFilePath, out stringFromFile);
			string stringFromCache;
			TimeSpan fromCacheTime = readFromCache.TimeExecution<dynamic, string>(new { TextFileCache = textFileCache, CacheItem = cacheItem }, out stringFromCache);

			Expect.IsGreaterThan(fromFileTime.Ticks, fromCacheTime.Ticks);
			OutLine(stringFromFile.First(25), ConsoleColor.Cyan);
			OutLine("****", ConsoleColor.DarkGreen);
			OutLine(stringFromCache.First(25), ConsoleColor.DarkCyan);

			Expect.AreEqual(stringFromFile, stringFromCache);

			OutLineFormat("Time from file: {0}", fromFileTime.ToString());
			OutLineFormat("Time from cache: {0}", fromCacheTime.ToString());
		}
	}
}
