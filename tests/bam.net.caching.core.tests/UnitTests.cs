/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Caching;
using Bam.Net.Testing;
using Bam.Net.CommandLine;
using System.IO;
using Bam.Net.Caching.File;
using System.Threading;
using Bam.Net.Data.Repositories;
using Bam.Net.Testing.Unit;
using Bam.Net.Caching.Tests.TestData;

namespace Bam.Net.Caching.Tests
{
	[Serializable]
	public class UnitTests: CommandLineTool
	{
        string _testFilePath = "./TestFile1.txt";
        [UnitTest]
        public void GetStringFromCacheIsFasterThanFromFile()
        {
            string testFilePath = _testFilePath;
            FileInfo testFile = new FileInfo(testFilePath);
            FileCache cache = new TextFileCache();
            cache.Load(testFile);

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
                TextFileCache textCache = context.TextFileCache;
                return textCache.GetText(testFile);
            };
            TimeSpan fromFileTime = readFromFile.TimeExecution<string, string>(testFilePath, out string stringFromFile);
            TimeSpan fromCacheTime = readFromCache.TimeExecution<dynamic, string>(new { TextFileCache = cache }, out string stringFromCache);

            Expect.IsGreaterThan(fromFileTime.Ticks, fromCacheTime.Ticks);
            OutLine(stringFromFile.First(25), ConsoleColor.Cyan);
            OutLine("****", ConsoleColor.DarkGreen);
            OutLine(stringFromCache.First(25), ConsoleColor.DarkCyan);

            Expect.AreEqual(stringFromFile, stringFromCache);

            Message.PrintLine("Time from file: {0}\r\n", fromFileTime.ToString());
            Message.PrintLine("Time from cache: {0}\r\n", fromCacheTime.ToString());
        }

        [UnitTest]
        public void GetBytesFromCacheIsFasterThanFromFile()
        {
            string testFilePath = _testFilePath;
            FileInfo testFile = new FileInfo(testFilePath);
            FileCache cache = new BinaryFileCache();
            cache.Load(testFile);

            Func<string, byte[]> readFromFile = System.IO.File.ReadAllBytes;
            Func<byte[]> readFromCache = () => cache.GetBytes(testFile);
            readFromFile(testFilePath); // prime
            readFromCache(); //prime
            TimeSpan fromFileTime = readFromFile.TimeExecution(testFilePath, out byte[] bytesFromFile);
            TimeSpan fromCacheTime = readFromCache.TimeExecution(out byte[] bytesFromCache);

            (fromFileTime.CompareTo(fromCacheTime) == 1).IsTrue($"Expected the time from cache to be faster: cacheTime={fromCacheTime}, fileTime={fromFileTime}");
            
            OutLine("****", ConsoleColor.DarkGreen);

            string fromFile = Encoding.UTF8.GetString(bytesFromFile);
            string fromCache = Encoding.UTF8.GetString(bytesFromCache);
            Expect.AreEqual(fromFile, fromCache);

            Message.PrintLine("Time from file: {0}\r\n", fromFileTime.ToString());
            Message.PrintLine("Time from cache: {0}\r\n", fromCacheTime.ToString());
        }

        [UnitTest]
        public void GetZippedBytesFromCacheIsFasterThanFromFile()
        {
            string testFilePath = _testFilePath;
            FileInfo testFile = new FileInfo(testFilePath);
            FileCache cache = new BinaryFileCache();
            cache.Load(testFile);

            Func<string, byte[]> readFromFile = (filePath) => System.IO.File.ReadAllBytes(filePath).GZip();
            Func<dynamic, byte[]> readFromCache = (context) =>
            {
                BinaryFileCache textCache = context.BinaryFileCache;
                return textCache.GetZippedBytes(testFile);
            };
            TimeSpan fromFileTime = readFromFile.TimeExecution<string, byte[]>(testFilePath, out byte[] bytesFromFile);
            TimeSpan fromCacheTime = readFromCache.TimeExecution<dynamic, byte[]>(new { BinaryFileCache = cache }, out byte[] bytesFromCache);

            Expect.IsGreaterThan(fromFileTime.Ticks, fromCacheTime.Ticks);

            OutLine("****", ConsoleColor.DarkGreen);

            string fromFile = Encoding.UTF8.GetString(bytesFromFile);
            string fromCache = Encoding.UTF8.GetString(bytesFromCache);
            Expect.AreEqual(fromFile, fromCache);

            Message.PrintLine("Time from file: {0}\r\n", fromFileTime.ToString());
            Message.PrintLine("Time from cache: {0}\r\n", fromCacheTime.ToString());
        }

        [UnitTest]
        public void NowIsDifferentEachTime()
        {
            DateTime then = DateTime.UtcNow;
            Thread.Sleep(300);
            DateTime now  = DateTime.UtcNow;
            Expect.IsFalse(now.Equals(then));
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void QueryCacheTest()
        {
            GetRepos(nameof(QueryCacheTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);

            string name = 8.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            QueryCache cache = new QueryCache();
            bool reloaded = false;
            cache.Reloaded += (o, c) => reloaded = true;
            IEnumerable<object> results = cache.Results(typeof(TestMonkey), mongoRepo, Filter.Where("Name") == name);
            reloaded.IsTrue();
            reloaded = false;
            results = cache.Results(typeof(TestMonkey), mongoRepo, Filter.Where("Name") == name);
            reloaded.IsFalse();
            Expect.AreEqual(1, results.Count());
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void CachingRepoQueryStringParameterTest()
        {
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            object result = cachingRepo.Query("Name", name).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void CachingRepoQueryTypeDictionaryParameterTest()
        {
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            object result = cachingRepo.Query(typeof(TestMonkey), new Dictionary<string, object>()
            {
                { "Name", name}
            }).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void CachingRepoQueryGenericTypeDictionaryParameterTest()
        {
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            object result = cachingRepo.Query<TestMonkey>(new Dictionary<string, object>()
            {
                { "Name", name}
            }).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void CachingRepoQueryGenericTypeDynamicParameterTest()
        {
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            object result = cachingRepo.Query<TestMonkey>(new { Name = name }).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void CachingRepoQueryGenericTypeFuncParameterTest()
        {
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            object result = cachingRepo.Query<TestMonkey>((o) => o.Name.Equals(name)).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void CachingRepoQueryTypeDynamicParameterTest()
        {
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            object result = cachingRepo.Query(typeof(TestMonkey), new { Name = name }).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void CachingRepoQueryTypeFuncParameterTest()
        {
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            object result = cachingRepo.Query(typeof(TestMonkey), (o)=> o.Property("Name").ToString() == name).First();
            
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);

            object result2 = cachingRepo.Query(typeof(TestMonkey), (o) => o.Property("Name").ToString().Equals(name)).First();
            Expect.IsNotNull(result2);
            Expect.AreEqual(typeof(TestMonkey), result2.GetType());
            Expect.CanCast<TestMonkey>(result2);
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void CachingRepoQueryTypeQueryFilterTest()
        {
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            object result = cachingRepo.Query(typeof(TestMonkey), QueryFilter.Where("Name") == name).ToArray().First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest(IgnoreBecause = "MongoRepository is not fully implemented")]
        public void CachingRepoQueryGenericQueryFilterTest()
        {
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out MongoRepository mongoRepo, out CachingRepository cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            mongoRepo.Save(data);
            object result = cachingRepo.Query<TestMonkey>(QueryFilter.Where("Name") == name).ToArray().First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        private void GetRepos(string dbName, out MongoRepository mongoRepo, out CachingRepository cachingRepo)
        {
            mongoRepo = new MongoRepository(databaseName: dbName);
            mongoRepo.AddType<TestMonkey>();
            cachingRepo = new CachingRepository(mongoRepo);
        }
    }

}
