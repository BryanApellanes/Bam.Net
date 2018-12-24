/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Bam.Net.Logging;
using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net;
using Bam.Net.ExceptionHandling;
using Bam.Net.Testing.Integration;
using Bam.Net.Testing.Unit;
using System.Diagnostics;
using Bam.Net.Testing.Specification;

namespace Bam.Net.Testing
{
    [Serializable]
    public abstract class CommandLineTestInterface: CommandLineInterface
    {
        static CommandLineTestInterface()
        {
            GetUnitTestRunListeners = () => new List<ITestRunListener<UnitTestMethod>>();
            GetSpecTestRunListeners = ()=> new List<ITestRunListener<SpecTestMethod>>();
            InitLogger();
        }
        
        protected static ILogger Logger { get; set; }
		public static LogEntryAddedListener MessageToConsole { get; set; }

        protected static MethodInfo DefaultMethod { get; set; }

        /// <summary>
        /// Prepares commandline arguments for reading.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="parseErrorHandler">The parse error handler.</param>
        public static void Initialize(string[] args, ConsoleArgsParsedDelegate parseErrorHandler = null)
		{
            AssemblyResolve.Monitor(()=>
            {
                ILogger logger = Logger;
                if(logger == null)
                {
                    logger = new ConsoleLogger { AddDetails = false };
                    logger.StartLoggingThread();
                }
                return logger;
            });

            if(parseErrorHandler == null)
            {
                parseErrorHandler = (a)=>
                {
                    throw new ArgumentException(a.Message);
                };
            }

            ArgsParsedError += parseErrorHandler;            

			AddValidArgument("i", true, description: "Run interactively");
			AddValidArgument("?", true, description: "Show usage");
			AddValidArgument("t", true, description: "Run all unit tests");
            AddValidArgument("it", true, description: "Run all integration tests");
            AddValidArgument("spec", true, description: "Run all specification tests");
            AddValidArgument("tag", false, description: "Specify a tag to associate with test executions");

			ParseArgs(args);

			if (Arguments.Contains("?"))
			{
				Usage(Assembly.GetEntryAssembly());
				Exit();
			}
			else if (Arguments.Contains("i"))
			{
				Interactive();
                return;
            }
            else if (Arguments.Contains("t"))
            {
                RunAllUnitTests(Assembly.GetEntryAssembly());
                return;
            }
            else if (Arguments.Contains("spec"))
            {
                RunAllSpecTests(Assembly.GetEntryAssembly());
                return;
            }
            else if (Arguments.Contains("it"))
            {
                IntegrationTestRunner.RunIntegrationTests(Assembly.GetEntryAssembly());
                return;
            }
            else
			{
				if (DefaultMethod != null)
				{
					Expect.IsTrue(DefaultMethod.IsStatic, "DefaultMethod must be static.");
					if (DefaultMethod.GetParameters().Length > 0)
					{
						DefaultMethod.Invoke(null, new object[] { Arguments });
					}
					else
					{
						DefaultMethod.Invoke(null, null);
					}
					return;
				}
			}
		}

        protected static void InitLogger()
        {
            ConsoleLogger logger = (ConsoleLogger)Log.CreateLogger(typeof(ConsoleLogger));
            logger.UseColors = true;
            logger.ShowTime = true;
			logger.StartLoggingThread();
            logger.EntryAdded += new LogEntryAddedListener(LoggerEntryAdded);
            Logger = logger;
        }

		private static void TryInvoke<T>(ConsoleMethod cim)
		{
			try
			{
				cim.Invoke();
			}
			catch (Exception ex)
			{
				OutLineFormat("Exception in {0} method {1}: {2}", ConsoleColor.Magenta, typeof(T).Name, cim.Method.Name, ex.Message);
			}
		}

        public static void RunIntegrationTests()
        {
            IntegrationTestRunner.RunIntegrationTests(Assembly.GetEntryAssembly());
        }

		protected static bool IsInteractive { get; set; }

        public static void Interactive()
        {
            try
            {
                IsInteractive = true;
                AddMenu(Assembly.GetEntryAssembly(), "Main", 'm', new ConsoleMenuDelegate(ShowMenu));
                AddMenu(Assembly.GetEntryAssembly(), "Test", 't', new ConsoleMenuDelegate(UnitTestMenu));

                ShowMenu(Assembly.GetEntryAssembly(), OtherMenus.ToArray(), "Main");
            }
            catch (Exception ex)
            {
                if (ex is ReflectionTypeLoadException typeLoadEx)
                {
                    ex = new ReflectionTypeLoadAggregateException(typeLoadEx);
                }
                OutLine(ex.Message);
                OutLine(ex.StackTrace);
                throw;
            }
        }

        protected static void MainMenu(string header)
        {
            AddMenu(Assembly.GetEntryAssembly(), header, 'm', new ConsoleMenuDelegate(ShowMenu));

            ShowMenu(Assembly.GetEntryAssembly(), OtherMenus.ToArray(), header);
        }

        protected static void Pass(string text)
        {
            OutLineFormat("{0}:Passed", ConsoleColor.Green, text);
        }

        public static void UnitTestMenu(Assembly assembly, ConsoleMenu[] otherMenus, string header)
        {
            Console.WriteLine(header);
            ITestRunner<UnitTestMethod> runner = GetUnitTestRunner(assembly, Log.Default);
            ShowActions(runner.GetTests());
            Console.WriteLine();
            Console.WriteLine("Q to quit\ttype all to run all tests.");
            string answer = ShowSelectedMenuOrReturnAnswer(otherMenus);
            Console.WriteLine();

            try
            {
                answer = answer.Trim().ToLowerInvariant();
                runner.RunSpecifiedTests(answer);
            }
            catch (Exception ex)
            {                
                Error("An error occurred running tests", ex);                
            }

            if (Confirm("Return to the Test menu? [y][N]"))
            {
                UnitTestMenu(assembly, otherMenus, header);
            }
            else
            {
                Exit(0);
            }
        }

        public static void RunAllSpecTests(Assembly assembly, ILogger logger = null, EventHandler passedHandler = null, EventHandler failedHandler = null)
        {
            ITestRunner<SpecTestMethod> runner = GetSpecTestRunner(assembly, logger);
            AttachHandlers<SpecTestMethod>(passedHandler, failedHandler, runner);
            foreach (ITestRunListener<SpecTestMethod> listener in GetSpecTestRunListeners())
            {
                listener.Tag = runner.Tag;
                listener.Listen(runner);
            }
            runner.RunAllTests();
        }

        public static void RunAllUnitTests(Assembly assembly, ILogger logger = null, EventHandler passedHandler = null, EventHandler failedHandler = null)
        {
            ITestRunner<UnitTestMethod> runner = GetUnitTestRunner(assembly, logger);
            AttachHandlers<UnitTestMethod>(passedHandler, failedHandler, runner);
            foreach (ITestRunListener<UnitTestMethod> listener in GetUnitTestRunListeners())
            {
                listener.Tag = runner.Tag;
                listener.Listen(runner);
            }
            runner.RunAllTests();
        }

        private static void AttachHandlers<TTestMethod>(EventHandler passedHandler, EventHandler failedHandler, ITestRunner<TTestMethod> runner) where TTestMethod : TestMethod
        {
            if (passedHandler != null)
            {
                runner.TestPassed += passedHandler;
            }
            if (failedHandler != null)
            {
                runner.TestFailed += failedHandler;
            }
        }

        protected internal static Func<IEnumerable<ITestRunListener<UnitTestMethod>>> GetUnitTestRunListeners
        {
            get;
            set;
        }

        protected internal static Func<IEnumerable<ITestRunListener<SpecTestMethod>>> GetSpecTestRunListeners
        {
            get;
            set;
        }

        protected internal static ITestRunner<SpecTestMethod> GetSpecTestRunner(Assembly assembly, ILogger logger)
        {
            return GetTestRunner<SpecTestMethod>(assembly, logger);
        }

        protected internal static ITestRunner<UnitTestMethod> GetUnitTestRunner(Assembly assembly, ILogger logger)
        {
            return GetTestRunner<UnitTestMethod>(assembly, logger);
        }

        protected internal static ITestRunner<TTestMethod> GetTestRunner<TTestMethod>(Assembly assembly, ILogger logger) where TTestMethod : TestMethod
        {
            ITestRunner<TTestMethod> runner = TestRunner<TTestMethod>.Create(assembly, logger);
            if (Arguments != null && Arguments.Contains("tag"))
            {
                runner.Tag = Arguments["tag"];
            }
            runner.NoTestsDiscovered += (o, e) => OutLineFormat("No tests were found in {0}", ConsoleColor.Yellow, assembly.FullName);
            runner.TestsDiscovered += (o, e) =>
            {
                TestsDiscoveredEventArgs<TTestMethod> args = (TestsDiscoveredEventArgs<TTestMethod>)e;
                OutLineFormat("Running all tests in {0}", ConsoleColor.Green, args.Assembly.FullName);
                OutLineFormat("\tFound {0} tests", ConsoleColor.Cyan, args.Tests.Count);
            };
            runner.TestPassed += (o, e) =>
            {
                TestEventArgs<TTestMethod> args = (TestEventArgs<TTestMethod>)e;
                Pass(args.Test.Information);
            };
            runner.TestFailed += (o, t) =>
            {
                TestExceptionEventArgs args = (TestExceptionEventArgs)t;
                Out("Test Failed: " + args.TestMethod.Information + "\r\n", ConsoleColor.Red);
                Out(args.Exception.Message, ConsoleColor.Magenta);
                Out();
                Out(args.Exception.StackTrace, ConsoleColor.Red);
                Out("---", ConsoleColor.Red);
                Out();
            };
            runner.TestsFinished += (o, e) =>
            {
                TestEventArgs<TTestMethod> args = (TestEventArgs<TTestMethod>)e;
                TestRunnerSummary summary = args.TestRunner.TestSummary;
                Out();
                OutLine("********");
                if (summary.FailedTests.Count > 0)
                {
                    OutLineFormat("({0}) tests passed", ConsoleColor.Green, summary.PassedTests.Count);
                    OutLineFormat("({0}) tests failed", ConsoleColor.Red, summary.FailedTests.Count);
                    summary.FailedTests.ForEach(cim =>
                    {
                        Out("\t");
                        MethodInfo method = cim.Test.Method;
                        Type type = method.DeclaringType;
                        string testIdentifier = $"{type.Namespace}.{type.Name}.{method.Name}";
                        OutLineFormat("{0}: ({1})", new ConsoleColorCombo(ConsoleColor.Yellow, ConsoleColor.Red), cim.Test.Information, testIdentifier);
                    });
                }
                else
                {
                    OutLineFormat("All ({0}) tests passed", ConsoleColor.Green, summary.PassedTests.Count);
                }
                OutLine("********");
            };
            return runner;
        }

        public static void Warn(string message)
        {
            Warn(message, new object[] { });
        }

        /// <summary>
        /// Outputs a warning to the console.
        /// </summary>
        /// <param name="messageSignature">The message text to output</param>
        /// <param name="ex">The Exception that occurred.</param>
        /// <param name="signatureVariableValues"></param>
        public static void Warn(string messageSignature, params object[] signatureVariableValues)
        {
            Logger.AddEntry(messageSignature, LogEventType.Warning, ToStringArray(signatureVariableValues));
            Logger.BlockUntilEventQueueIsEmpty();
            Logger.RestartLoggingThread();
        }

        public static void Error(string message, Exception ex)
        {
            Error(message, ex, new object[] { });
        }
        /// <summary>
        /// Outputs an error to the console.
        /// </summary>
        /// <param name="messageSignature">The message text to output</param>
        /// <param name="ex">The Exception that occurred.</param>
        /// <param name="signatureVariableValues"></param>
        public static void Error(string messageSignature, Exception ex, params object[] signatureVariableValues)
        {
            Logger.AddEntry(messageSignature, ex, ToStringArray(signatureVariableValues));
            Logger.BlockUntilEventQueueIsEmpty();
            Logger.RestartLoggingThread();
        }

		private static string[] ToStringArray(object[] signatureVariableValues)
		{
			List<string> variableValues = new List<string>(signatureVariableValues.Length);
			foreach (object obj in signatureVariableValues)
			{
				variableValues.Add(obj.ToString());
			}
			return variableValues.ToArray();
		}

		private static void LoggerEntryAdded(string applicationName, LogEvent logEvent)
		{
            MessageToConsole?.Invoke(applicationName, logEvent);

            if (logEvent.Severity == LogEventType.Fatal)
			{
				Environment.Exit(1);
			}
		}
    }
}
