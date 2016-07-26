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
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using Bam.Net.Encryption;
using System.Data.SQLite;
using Bam.Net.Data.SQLite;

namespace Bam.Net.Data.Tests.Integration
{
	[Serializable]
	class Program : CommandLineTestInterface
	{
		static void Main(string[] args)
		{
            IsolateMethodCalls = false;
			PreInit();

			AddValidArgument("t", true);
			DefaultMethod = typeof(Program).GetMethod("Start");

			Initialize(args);
		}

		[ConsoleAction]
		public void RunIntegrationTests()
		{
			IntegrationTestRunner.RunAllIntegrationTests(typeof(Program).Assembly);
		}

        [ConsoleAction]
        public void RunSchemaExtractorTests()
        {
            IntegrationTestRunner.RunIntegrationTests(typeof(SchemaExtractorTests));
        }

		[ConsoleAction]
		public void RunQueryTests()
		{
			IntegrationTestRunner.RunIntegrationTests(typeof(DaoQueryTests));
		}

		[ConsoleAction]
		public void RunCrudTests()
		{
			IntegrationTestRunner.RunIntegrationTests(typeof(DaoCrudTests));
		}

		[ConsoleAction]
        public void RunTestsInAssemblyList()
        {
            string assemblyList = @"Z:\Workspace\FailedAssemblies.txt";
            string[] assemblies = File.ReadAllLines(assemblyList);
            assemblies.Each(assemblyName =>
            {
                assemblyName = assemblyName.Trim();
                "bamtestrunner /dir:{0} /search:{1}"._Format(
                    @"C:\src\Bam.Net\Bam.Net\BuildScripts\Bam\BuildOutput\Release\v4.5", 
                    assemblyName
                    ).Run(o =>
                {
                    Out(o, ConsoleColor.Cyan);
                }, e =>
                {
                    Out(e, ConsoleColor.Red);
                });
            });
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
		}

		#region do not modify
		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
                IntegrationTestRunner.RunAllIntegrationTests(typeof(Program).Assembly);
            }
			else
			{
				Interactive();
			}
		}
		#endregion
	}
}
