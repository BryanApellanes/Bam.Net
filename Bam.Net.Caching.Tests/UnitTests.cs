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

namespace Bam.Net.Caching.Tests
{
	[Serializable]
	public class UnitTests: CommandLineTestInterface
	{
        [UnitTest]
        public void GetStringFromCacheIsFasterThanFromFile()
        {
            string testFilePath = ".\\TestFile1.txt";
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
            string stringFromFile;
            TimeSpan fromFileTime = readFromFile.TimeExecution<string, string>(testFilePath, out stringFromFile);
            string stringFromCache;
            TimeSpan fromCacheTime = readFromCache.TimeExecution<dynamic, string>(new { TextFileCache = cache }, out stringFromCache);

            Expect.IsGreaterThan(fromFileTime.Ticks, fromCacheTime.Ticks);
            OutLine(stringFromFile.First(25), ConsoleColor.Cyan);
            OutLine("****", ConsoleColor.DarkGreen);
            OutLine(stringFromCache.First(25), ConsoleColor.DarkCyan);

            Expect.AreEqual(stringFromFile, stringFromCache);

            OutLineFormat("Time from file: {0}\r\n", fromFileTime.ToString());
            OutLineFormat("Time from cache: {0}\r\n", fromCacheTime.ToString());
        }

        [UnitTest]
        public void GetBytesFromCacheIsFasterThanFromFile()
        {
            string testFilePath = ".\\TestFile1.txt";
            FileInfo testFile = new FileInfo(testFilePath);
            FileCache cache = new BinaryFileCache();
            cache.Load(testFile);

            Func<string, byte[]> readFromFile = (filePath) =>
            {
                return System.IO.File.ReadAllBytes(filePath);
            };
            Func<dynamic, byte[]> readFromCache = (context) =>
            {
                BinaryFileCache textCache = context.BinaryFileCache;
                return textCache.GetBytes(testFile);
            };
            readFromFile(testFilePath); // prime
            readFromCache(new { BinaryFileCache = cache }); //prime
            TimeSpan fromFileTime = readFromFile.TimeExecution(testFilePath, out byte[] bytesFromFile);
            TimeSpan fromCacheTime = readFromCache.TimeExecution(new { BinaryFileCache = cache }, out byte[] bytesFromCache);

            Expect.IsTrue(fromFileTime.CompareTo(fromCacheTime) == 1);
            
            OutLine("****", ConsoleColor.DarkGreen);

            string fromFile = Encoding.UTF8.GetString(bytesFromFile);
            string fromCache = Encoding.UTF8.GetString(bytesFromCache);
            Expect.AreEqual(fromFile, fromCache);

            OutLineFormat("Time from file: {0}\r\n", fromFileTime.ToString());
            OutLineFormat("Time from cache: {0}\r\n", fromCacheTime.ToString());
        }

        [UnitTest]
        public void GetZippedBytesFromCacheIsFasterThanFromFile()
        {
            string testFilePath = ".\\TestFile1.txt";
            FileInfo testFile = new FileInfo(testFilePath);
            FileCache cache = new BinaryFileCache();
            cache.Load(testFile);

            Func<string, byte[]> readFromFile = (filePath) =>
            {
                return System.IO.File.ReadAllBytes(filePath).GZip();
            };
            Func<dynamic, byte[]> readFromCache = (context) =>
            {
                BinaryFileCache textCache = context.BinaryFileCache;
                return textCache.GetZippedBytes(testFile);
            };
            byte[] bytesFromFile;
            TimeSpan fromFileTime = readFromFile.TimeExecution<string, byte[]>(testFilePath, out bytesFromFile);
            byte[] bytesFromCache;
            TimeSpan fromCacheTime = readFromCache.TimeExecution<dynamic, byte[]>(new { BinaryFileCache = cache }, out bytesFromCache);

            Expect.IsGreaterThan(fromFileTime.Ticks, fromCacheTime.Ticks);

            OutLine("****", ConsoleColor.DarkGreen);

            string fromFile = Encoding.UTF8.GetString(bytesFromFile);
            string fromCache = Encoding.UTF8.GetString(bytesFromCache);
            Expect.AreEqual(fromFile, fromCache);

            OutLineFormat("Time from file: {0}\r\n", fromFileTime.ToString());
            OutLineFormat("Time from cache: {0}\r\n", fromCacheTime.ToString());
        }

        [UnitTest]
        public void NowIsDifferentEachTime()
        {
            DateTime then = DateTime.UtcNow;
            Thread.Sleep(300);
            DateTime now  = DateTime.UtcNow;
            Expect.IsFalse(now.Equals(then));
        }

        [UnitTest]
        public void QueryCacheTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(QueryCacheTest), out daoRepo, out cachingRepo);

            string name = 8.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            QueryCache cache = new QueryCache();
            bool reloaded = false;
            cache.Reloaded += (o, c) => reloaded = true;
            IEnumerable<object> results = cache.Results(typeof(TestMonkey), daoRepo, Filter.Where("Name") == name);
            Expect.IsTrue(reloaded);
            reloaded = false;
            results = cache.Results(typeof(TestMonkey), daoRepo, Filter.Where("Name") == name);
            Expect.IsFalse(reloaded);
            Expect.AreEqual(1, results.Count());
        }

        [UnitTest]
        public void CachingRepoQueryDynamicParameterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryDynamicParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query(new { Type = typeof(TestMonkey), Name = name }).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest]
        public void CachingRepoQueryStringParameterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query("Name", name).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest]
        public void CachingRepoQueryTypeDictionaryParameterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query(typeof(TestMonkey), new Dictionary<string, object>()
            {
                { "Name", name}
            }).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }
        [UnitTest]
        public void CachingRepoQueryGenericTypeDictionaryParameterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query<TestMonkey>(new Dictionary<string, object>()
            {
                { "Name", name}
            }).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }
        [UnitTest]
        public void CachingRepoQueryGenericTypeDynamicParameterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query<TestMonkey>(new { Name = name }).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }
        [UnitTest]
        public void CachingRepoQueryGenericTypeFuncParameterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query<TestMonkey>((o) => o.Name.Equals(name)).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest]
        public void CachingRepoQueryTypeDynamicParameterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query(typeof(TestMonkey), new { Name = name }).First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest]
        public void CachingRepoQueryTypeFuncParameterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query(typeof(TestMonkey), (o)=> o.Property("Name").ToString() == name).First();
            
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);

            object result2 = cachingRepo.Query(typeof(TestMonkey), (o) => o.Property("Name").ToString().Equals(name)).First();
            Expect.IsNotNull(result2);
            Expect.AreEqual(typeof(TestMonkey), result2.GetType());
            Expect.CanCast<TestMonkey>(result2);
        }

        [UnitTest]
        public void CachingRepoQueryTypeQueryFilterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query(typeof(TestMonkey), QueryFilter.Where("Name") == name).ToArray().First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }

        [UnitTest]
        public void CachingRepoQueryGenericQueryFilterTest()
        {
            DaoRepository daoRepo;
            CachingRepository cachingRepo;
            GetRepos(nameof(CachingRepoQueryStringParameterTest), out daoRepo, out cachingRepo);
            string name = 6.RandomLetters();
            TestMonkey data = new TestMonkey { Name = name };
            daoRepo.Save(data);
            object result = cachingRepo.Query<TestMonkey>(QueryFilter.Where("Name") == name).ToArray().First();
            Expect.IsNotNull(result);
            Expect.AreEqual(typeof(TestMonkey), result.GetType());
            Expect.CanCast<TestMonkey>(result);
        }
        private void GetRepos(string dbName, out DaoRepository daoRepo, out CachingRepository cachingRepo)
        {
            SQLiteDatabase db = new SQLiteDatabase(dbName);
            daoRepo = new DaoRepository(db);
            daoRepo.DaoNamespace = $"{typeof(TestMonkey).Namespace}.Dao";
            daoRepo.WarningsAsErrors = false;
            daoRepo.AddType<TestMonkey>();
            cachingRepo = new CachingRepository(daoRepo);
        }
    }

    [Serializable]
    public class TestMonkey : RepoData
    {
        public string Name { get; set; }
    }
}
