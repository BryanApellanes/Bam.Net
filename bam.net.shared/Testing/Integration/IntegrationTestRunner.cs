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
    // TODO: Refactor this to follow the same pattern as unit and spec tests
	public class IntegrationTestRunner: CommandLineTestInterface
	{
        /// <summary>
        /// Event that fires when a test fails.
        /// </summary>
        public static event EventHandler<TestExceptionEventArgs> IntegrationTestFailed;

        /// <summary>
        /// Event that fires when a test passes.
        /// </summary>
        public static event EventHandler<ConsoleMethod> IntegrationTestPassed;

        public static void RunIntegrationTests(FileInfo file, EventHandler<Exception> onFailed = null)
		{
			// get all the IntegrationTestContainers
			Assembly assembly = Assembly.LoadFrom(file.FullName);
			RunIntegrationTests(assembly, onFailed);
		}

		public static void RunIntegrationTests(Assembly assembly, EventHandler<Exception> onFailed = null)
		{
			assembly.GetTypes().Where(type => type.HasCustomAttributeOfType<IntegrationTestContainerAttribute>()).Each(type =>
			{
				RunIntegrationTests(type, onFailed);
			});
		}

		public static void RunIntegrationTests(Type containerType, EventHandler<Exception> onFailed = null)
		{
			IntegrationTestContainerAttribute containerAttr = containerType.GetCustomAttributeOfType<IntegrationTestContainerAttribute>();
			if (containerAttr == null)
			{
				throw new InvalidOperationException("The specified type ({0}) is not a valid IntegrationTestcontainer, it is missing the IntegrationTestContainer attribute"._Format(containerType.Name));
			}
			string containerDescription = string.IsNullOrEmpty(containerAttr.Description) ? containerType.Name.PascalSplit(" ") : containerAttr.Description;
			OutLineFormat("Running ({0})", ConsoleColor.DarkGreen, containerDescription);
			object testContainer = containerType.Construct();
			// get the IntegrationTestSetup and run them
			MethodInfo setup = containerType.GetMethods().FirstOrDefault(methodInfo => methodInfo.HasCustomAttributeOfType<IntegrationTestSetupAttribute>());
			if (setup != null)
			{
				setup.Invoke(testContainer, null);
			}
			// get all the IntegrationTests and run them
			IEnumerable<MethodInfo> testMethods = containerType.GetMethods().Where(methodInfo => methodInfo.HasCustomAttributeOfType<IntegrationTestAttribute>());
			OutLineFormat("Found {0} Integration Tests", ConsoleColor.Cyan, testMethods.Count());
			testMethods.Each(testMethod =>
			{
				try
				{
					IntegrationTestAttribute attr = testMethod.GetCustomAttribute<IntegrationTestAttribute>();
					string description = string.IsNullOrEmpty(attr.Description) ? testMethod.Name.PascalSplit(" ") : attr.Description;
					OutLineFormat("Starting: {0}", ConsoleColor.Green, description);
					testMethod.Invoke(testContainer, null);
					Pass(description);
                    try
                    {
                        IntegrationTestPassed?.Invoke(null, new ConsoleMethod(testMethod));
                    }
                    catch (Exception ex)
                    {
                        Log.AddEntry("Exception in IntegrationTestPassed event handler: {0}", LogEventType.Warning, ex.Message);
                    }
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
                    onFailed?.Invoke(testContainer, ex);
                    try
                    {
                        IntegrationTestFailed?.Invoke(null, new TestExceptionEventArgs(new IntegrationTestMethod(testMethod), ex));
                    }
                    catch (Exception e)
                    {
                        Log.AddEntry("Exception in IntegrationTestFailed event handler: {0}", LogEventType.Warning, e.Message);
                    }
                }
			});
			// get the IntegrationTestCleanup and run it
			MethodInfo cleanup = containerType.GetMethods().FirstOrDefault(methodInfo => methodInfo.HasCustomAttributeOfType<IntegrationTestCleanupAttribute>());
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
	}
}
