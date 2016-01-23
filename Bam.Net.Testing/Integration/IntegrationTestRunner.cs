/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Bam.Net.CommandLine;
using Bam.Net.Testing.Integration;
using Bam.Net.Testing.Specification;
using Bam.Net.Logging;

namespace Bam.Net.Testing.Integration
{
	public class IntegrationTestRunner: CommandLineTestInterface
	{
        /// <summary>
        /// Event that fires when a test fails.
        /// </summary>
        public static event EventHandler<TestExceptionEventArgs> IntegrationTestFailed;

        /// <summary>
        /// Event that fires when a test passes.
        /// </summary>
        public static event EventHandler<ConsoleInvokeableMethod> IntegrationTestPassed;

        public static void RunAllIntegrationTests(FileInfo file, EventHandler<Exception> onFailed = null)
		{
			// get all the IntegrationTestContainers
			Assembly assembly = Assembly.LoadFrom(file.FullName);
			RunAllIntegrationTests(assembly, onFailed);
		}

		public static void RunAllIntegrationTests(Assembly assembly, EventHandler<Exception> onFailed = null)
		{
			assembly.GetTypes().Where(type => type.HasCustomAttributeOfType<IntegrationTestContainerAttribute>()).Each(type =>
			{
				RunIntegrationTests(type, onFailed);
			});
		}

		public static void RunIntegrationTests(Type type, EventHandler<Exception> onFailed = null)
		{
			IntegrationTestContainerAttribute containerAttr = type.GetCustomAttributeOfType<IntegrationTestContainerAttribute>();
			if (containerAttr == null)
			{
				throw new InvalidOperationException("The specified type ({0}) is not a valid IntegrationTestcontainer, it is missing the IntegrationTestContainer attribute"._Format(type.Name));
			}
			string containerDescription = string.IsNullOrEmpty(containerAttr.Description) ? type.Name.PascalSplit(" ") : containerAttr.Description;
			OutLineFormat("Running ({0})", ConsoleColor.DarkGreen, containerDescription);
			object testContainer = type.Construct();
			// get the IntegrationTestSetup and run them
			MethodInfo setup = type.GetMethods().FirstOrDefault(methodInfo => methodInfo.HasCustomAttributeOfType<IntegrationTestSetupAttribute>());
			if (setup != null)
			{
				setup.Invoke(testContainer, null);
			}
			// get all the IntegrationTests and run them
			MethodInfo[] testMethods = type.GetMethods().Where(methodInfo => methodInfo.HasCustomAttributeOfType<IntegrationTestAttribute>()).ToArray();
			OutLineFormat("Found {0} Integration Tests", ConsoleColor.Cyan, testMethods.Length);
			testMethods.Each(testMethod =>
			{
				try
				{
					IntegrationTestAttribute attr = testMethod.GetCustomAttribute<IntegrationTestAttribute>();
					string description = string.IsNullOrEmpty(attr.Description) ? testMethod.Name.PascalSplit(" ") : attr.Description;
					OutLineFormat("Starting: {0}", ConsoleColor.Green, description);
					testMethod.Invoke(testContainer, null);
					Pass(description);
				}
				catch (Exception ex)
				{
					if (ex.InnerException != null)
					{
						ex = ex.InnerException;
					}

					string msgFormat = "Integration Test failed: {0}\r\n";
					Log.AddEntry(msgFormat, ex, ex.Message);
					OutFormat(msgFormat, ConsoleColor.Red, ex.Message);
					if (onFailed != null)
					{
						onFailed(testContainer, ex);
					}
				}
			});
			// get the IntegrationTestCleanup and run it
			MethodInfo cleanup = type.GetMethods().FirstOrDefault(methodInfo => methodInfo.HasCustomAttributeOfType<IntegrationTestCleanupAttribute>());
			if (cleanup != null)
			{
				try
				{
					OutLine("Running cleanup", ConsoleColor.Yellow);
					cleanup.Invoke(testContainer, null);
				}
				catch (Exception ex)
				{
					if (ex.InnerException != null)
					{
						ex = ex.InnerException;
					}
					OutFormat("Cleanup failed: {0}", ConsoleColor.Red, ex.Message);
				}
			}
		}

        private void OnIntegrationTestPassed() { }     
	}
}
