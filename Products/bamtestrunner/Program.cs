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
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Logging;

namespace Bam.Net.Testing
{
    [Serializable]
    class Program : CommandLineTestInterface
	{
	    private const string _exitOnFailure = "exitOnFailure";
		private const string _programName = "bamtestrunner";
		static DaoRepository _repo;
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
            AddValidArgument("search", false, "The search pattern to use to locate test assemblies");
            AddValidArgument("dir", false, "The directory to look for test assemblies in");
			AddValidArgument("debug", true, "If specified, the runner will pause to allow for a debugger to be attached to the process");
			AddValidArgument("data", false, "The path to save the results to, default is the current directory if not specified");
			AddValidArgument(_exitOnFailure, true);
			
            DefaultMethod = typeof(Program).GetMethod("Start");
        }

        public static void Start()
        {			
			if(Arguments.Contains("debug"))
			{
				Pause("Attach the debugger now");
			}

			PrepareResultRepository();
            string startDirectory = Environment.CurrentDirectory;
			DirectoryInfo testDir = GetTestDirectory();
            Environment.CurrentDirectory = testDir.FullName;

			FileInfo[] files = GetTestFiles(testDir);

            TestFailed += TestFailedHandler;
			TestPassed += TestPassedHandler;
			bool exceptionOccurred = false;
			for (int i = 0; i < files.Length; i++)
			{
				FileInfo fi = files[i];
				try
				{
					Assembly assembly = Assembly.LoadFrom(fi.FullName);
					AttachBeforeAndAfterHandlers(assembly);
					RunAllTests(assembly);
					NullifyBeforeAndAfterHandlers();
                    Environment.CurrentDirectory = startDirectory;
				}
				catch (Exception ex)
				{
                    Environment.CurrentDirectory = startDirectory;
					exceptionOccurred = true;
					OutLineFormat("bamtestrunner: {0}", ConsoleColor.DarkRed, ex.Message);
					if (Arguments.Contains(_exitOnFailure))
					{
						Exit(1);
					}
				}
			}

			OutLineFormat("Passed: {0}", ConsoleColor.Green, _passedCount);
			OutLineFormat("Failed: {0}", ConsoleColor.Red, _failedCount);

			if (_failedCount > 0 || exceptionOccurred)
			{				
				Exit(1);
			}
			else
			{
				Exit(0);
			}
        }

		private static void PrepareResultRepository()
		{
			string directory = Arguments.Contains("data") ? Arguments["data"] : ".";
			_repo = new DaoRepository(new SQLiteDatabase(directory, "BamTests_{0}"._Format(DateTime.Now.Date.ToString("MM_dd_yyyy"))));
			_repo.AddType(typeof(UnitTestResult));
			_repo.EnsureDaoAssembly();
		}

		private static FileInfo[] GetTestFiles(DirectoryInfo testDir)
		{
			FileInfo[] files = null;
			if (Arguments.Contains("search"))
			{
				files = testDir.GetFiles(Arguments["search"]);
			}
			else
			{
				files = testDir.GetFiles();
			}
			return files;
		}

		static int _passedCount = 0;
		static int _failedCount = 0;
		
        private static void TestFailedHandler(object sender, TestExceptionEventArgs e)
        {
			_failedCount++;
			_repo.Save(new UnitTestResult(e));
			if (Arguments.Contains(_exitOnFailure))
			{
				Exit(1);
			}
        }

		private static void TestPassedHandler(object sender, ConsoleInvokeableMethod cim)
		{
			_passedCount++;
			_repo.Save(new UnitTestResult(cim));
		}
		
		private static DirectoryInfo GetTestDirectory()
		{
			DirectoryInfo testDir = new DirectoryInfo(".");
			if (Arguments.Contains("dir"))
			{
				string dir = Arguments["dir"];
				testDir = new DirectoryInfo(dir);
				if (!testDir.Exists)
				{
					OutLineFormat("The specified directory ({0}) was not found", ConsoleColor.Red, dir);
					Exit(1);
				}
			}
			return testDir;
		}
    }

}
