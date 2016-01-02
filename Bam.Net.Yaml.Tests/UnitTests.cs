/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Yaml.Serialization;
using System.Yaml;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Data.Schema;
using Bam.Net.Data.Repositories;
using System.Reflection;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;

namespace Bam.Net.Yaml.Tests
{
	public class YamlTestObject
	{
		public string Name { get; set; }
		public string[] StringArrayProperty { get; set; }

		public YamlTestObject Child { get; set; }

		public YamlTestObject[] ObjectArray { get; set; }
	}

	[Serializable]
	public class UnitTests : CommandLineTestInterface
	{
		public FileInfo GetTestFile()
		{
			FileInfo testFile = new FileInfo("Test.yaml");
			if(testFile.Exists)
			{
				File.Delete(testFile.FullName);
			}
			return testFile;
		}

		[UnitTest]
		public void WriteYamlTest()
		{
			YamlTestObject parent = CreateTestObject();
			YamlConfig config = new YamlConfig();
			config.OmitTagForRootNode = true;
			string yaml = parent.ToYaml(config);
			OutLine(yaml, ConsoleColor.Cyan);
			FileInfo testFile = GetTestFile();
			using(StreamWriter sw = new StreamWriter(testFile.FullName))
			{
				sw.Write(yaml);
			}
			//"notepad {0}"._Format(testFile.FullName).Run();
		}

		private YamlTestObject CreateTestObject(bool setChild = true, bool setObjArray = true)
		{
			YamlTestObject obj = new YamlTestObject { Name = 8.RandomLetters() };
			if(setChild)
			{
				obj.Child = CreateTestObject(false);
			}
			if(setObjArray)
			{
				List<YamlTestObject> objList = new List<YamlTestObject>();
				4.Times(i =>
				{
					objList.Add(CreateTestObject(false, false));
				});
				obj.ObjectArray = objList.ToArray();
			}

			obj.StringArrayProperty = new[] { 9.RandomLetters(), 5.RandomLetters(), 4.RandomLetters() };
			return obj;
		}
	}
}
