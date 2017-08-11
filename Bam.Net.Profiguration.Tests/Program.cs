/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Bam.Net.Configuration;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Profiguration;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Profiguration.Tests
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
			AddValidArgument("t", true, description: "run all tests");
			DefaultMethod = typeof(Program).GetMethod("Start");
		}

		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
				RunAllUnitTests(typeof(Program).Assembly);
			}
			else
			{
				Interactive();
			}
		}

        [ConsoleAction("List .prof files")]
        public void ListProfFiles()
        {
            string dirPath = Prompt("Please enter the directory path");
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            FileInfo[] files = dir.GetFiles("*.prof");
            foreach (FileInfo file in files)
            {
                OutFormat("{0}", ConsoleColor.Yellow, file.Name);
            }
        }

        [ConsoleAction("Show Profiguration Values")]
        public void ShowProfigurationValues()
        {
            string filePath = Prompt("Please enter the path to the file", ConsoleColor.Cyan);
            Profiguration prof = Profiguration.Load(filePath);
            Out("*** AppSettings ***", ConsoleColor.Cyan);
            foreach (string key in prof.AppSettings.Keys)
            {
                OutFormat("{0}={1}\r\n", ConsoleColor.Yellow, key, prof.AppSettings[key]);                
            }
            Out();
            Out("*** Connection Strings ***", ConsoleColor.Cyan);
            foreach (string key in prof.ConnectionStrings.Names)
            {
                OutFormat("{0}::{1}={2}", ConsoleColor.Yellow, prof.ConnectionStrings[key].Provider, prof.ConnectionStrings[key].ConnectionString.Key, prof.ConnectionStrings[key].ConnectionString.Value);
            }
        }

        [UnitTest]
        public void ShouldBeAbleToUseProfigurationSetToNameProfigurations()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            ProfigurationSet profSet = new ProfigurationSet(dir);
            string randomName = "Test_".RandomLetters(4);
            Profiguration prof = profSet.Get(randomName);
            
            Expect.IsNotNull(prof);
            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, randomName)));
            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, randomName + ".key")));            
        }

        [UnitTest]
        public void ShouldBeAbleToSaveAProfigurationProvidingAName()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            ProfigurationSet set = new ProfigurationSet(dir);
            string randomName = "Test_".RandomLetters(4);
            Profiguration prof = new Profiguration();
            prof.AppSettings["test"] = "monkey";

            set.Save(randomName, prof);

            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, randomName)));
            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, randomName + ".key")));

            Profiguration verify = set.Get(randomName);
            Expect.AreEqual("monkey", verify.AppSettings["test"]);
        }

        [UnitTest]
        public void FileWithNoExtensionShouldReturnEmptyString()
        {
            string withExtension = "file.txt";
            string withoutExtension = "file";

            Expect.AreEqual(".txt", Path.GetExtension(withExtension));
            Expect.AreEqual(string.Empty, Path.GetExtension(withoutExtension));
        }

        [UnitTest]
        public void ExtensionMethodsForCheckingExtensionsShouldWork()
        {
            FileInfo withExtension = new FileInfo("file.txt");
            FileInfo withoutExtension = new FileInfo("file");

            Expect.IsTrue(withExtension.HasExtension(".txt"));
            Expect.IsTrue(withoutExtension.HasNoExtension());
        }

        [UnitTest]
        public void IndexedProfigurationShouldAddName()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            if (dir.Exists)
            {
                dir.Delete(true);
            }

            ProfigurationSet set = new ProfigurationSet(dir);
            string name = "Test_".RandomLetters(6);
            string key = "Key_".RandomLetters(4);
            string value = "Value_".RandomLetters(4);

            Expect.IsTrue(set.ProfigurationNames.Length == 0);

            Profiguration prof = set[name];

            Expect.IsTrue(set.ProfigurationNames.Length == 1);
        }

        [UnitTest]
        public void SettingAProfigurationValueShouldReflectImmediately()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            ProfigurationSet set = new ProfigurationSet(dir);
            string name = "Test_".RandomLetters(6);
            string key = "Key_".RandomLetters(4);
            string value = "Value_".RandomLetters(4);

            Profiguration prof = set[name];
            prof.AppSettings[key] = value;

            Expect.AreEqual(value, prof.AppSettings[key], "value didn't get set");
        }

        [UnitTest]
        public void ShouldBeAbleToUseIndexedProfiguration()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            ProfigurationSet set = new ProfigurationSet(dir);
            string name = "Test_".RandomLetters(6);
            string key = "Key_".RandomLetters(4);
            string value = "Value_".RandomLetters(4);

            Profiguration prof = set[name];
            prof.AppSettings[key] = value;            

            Expect.AreEqual(value, prof.AppSettings[key], "value didn't get set");

            set.Save();

            ProfigurationSet validate = new ProfigurationSet(dir);

            Expect.AreEqual(value, validate[name].AppSettings[key]);
        }
    }

}
