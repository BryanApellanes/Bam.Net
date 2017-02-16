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

    }
}
