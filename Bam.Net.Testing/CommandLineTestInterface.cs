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

namespace Bam.Net.Testing
{
    [Serializable]
    public abstract class CommandLineTestInterface: CommandLineInterface
    {
        static CommandLineTestInterface()
        {
            InitLogger();
        }

        public const string UnitTestAttribute = "UnitTestAttribute";

        protected static ILogger Logger { get; set; }
		public static LogEntryAddedListener MessageToConsole { get; set; }
        protected static bool interactive;

        protected static MethodInfo DefaultMethod { get; set; }

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
			ArgsParsed += new ConsoleArgsParsedDelegate(ArgumentParsingComplete);

			interactive = false;

			AddValidArgument("i", true, description: "Run interactively");
			AddValidArgument("?", true, description: "Show usage");
			AddValidArgument("t", true, description: "Run all unit tests");
            AddValidArgument("it", true, description: "Run all integration tests");

			ParseArgs(args);
			int? exitCode = AttachFailedHandler();
			AttachBeforeAndAfterHandlers();

			if (Arguments.Contains("?"))
			{
				Usage(Assembly.GetEntryAssembly());
				Exit(exitCode.Value);
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

		private static int? AttachFailedHandler()
		{
			int? exitCode = 0;
			TestFailed += (o, t) =>
			{
				Out("Test Failed: " + t.ConsoleInvokeableMethod.Information + "\r\n", ConsoleColor.Red);
				Out(t.Exception.Message, ConsoleColor.Magenta);
				Out();
				Out(t.Exception.StackTrace, ConsoleColor.Red);
				Out("---", ConsoleColor.Red);
				Out();
				exitCode = 1;
			};
			return exitCode;
		}

        protected static void InitLogger()
        {
            ConsoleLogger logger = (ConsoleLogger)Log.CreateLogger(typeof(ConsoleLogger));
            logger.UseColors = true;
            logger.ShowTime = true;
			logger.StartLoggingThread();
            logger.EntryAdded += new LogEntryAddedListener(logger_EntryAdded);
            Logger = logger;
        }

        /// <summary>
        /// Attach the before and after handling methods by subscribing the 
        /// methods addorned with BeforeUnitTests, BeforeEachUnitTest, AfterUnitTests
        /// and AfterEachUnitTest attributes to appropriate test events
        /// </summary>
		protected static void AttachBeforeAndAfterHandlers()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			AttachBeforeAndAfterHandlers(assembly);
		}

        /// <summary>
        /// Set TestsStarting, TestsFinished, TestStarting and TestFinished
        /// to null effectively detaching any handlers that may have
        /// been attached
        /// </summary>
		public static void NullifyBeforeAndAfterHandlers()
		{
			if (TestsStarting != null)
			{
				TestsStarting = null;
			}
			if (TestsFinished != null)
			{
				TestsFinished = null;
			}
			if (TestStarting != null)
			{
				TestStarting = null;
			}
			if (TestFinished != null)
			{
				TestFinished = null;
			}
		}

        /// <summary>
        /// Attach the before and after handling methods by subscribing the 
        /// methods addorned with BeforeUnitTests, BeforeEachUnitTest, AfterUnitTests
        /// and AfterEachUnitTest attributes to appropriate test events
        /// </summary>
        /// <param name="assembly"></param>
		public static void AttachBeforeAndAfterHandlers(Assembly assembly)
		{
            List<ConsoleMethod> beforeAll = ConsoleMethod.FromAssembly<BeforeUnitTests>(assembly);
			List<ConsoleMethod> beforeEach = ConsoleMethod.FromAssembly<BeforeEachUnitTest>(assembly);
			List<ConsoleMethod> afterAll = ConsoleMethod.FromAssembly<AfterUnitTests>(assembly);
			List<ConsoleMethod> afterEach = ConsoleMethod.FromAssembly<AfterEachUnitTest>(assembly);

			TestsStarting += (o, e) =>
			{
				beforeAll.Each(cim =>
				{
					TryInvoke<BeforeUnitTests>(cim);
				});
			};
			TestsFinished += (o, e) =>
			{
				afterAll.Each(cim =>
				{
					TryInvoke<AfterUnitTests>(cim);
				});
			};
			TestStarting += (o, e) =>
			{
				beforeEach.Each(cim =>
				{
					TryInvoke<BeforeEachUnitTest>(cim);
				});
			};
			TestFinished += (o, e) =>
			{
				afterEach.Each(cim =>
				{
					TryInvoke<AfterEachUnitTest>(cim);
				});
			};
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
                ReflectionTypeLoadException typeLoadEx = ex as ReflectionTypeLoadException;
                if(typeLoadEx != null)
                {
                    ex = new ReflectionTypeLoadAggregateException(typeLoadEx);
                }
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        protected static void MainMenu(string header)
        {
            AddMenu(Assembly.GetEntryAssembly(), header, 'm', new ConsoleMenuDelegate(ShowMenu));

            ShowMenu(Assembly.GetEntryAssembly(), OtherMenus.ToArray(), header);
        }

        private static void ArgumentParsingComplete(ParsedArguments arguments)
        {
            if (arguments.Contains("i"))
                interactive = true;
        }


        protected static void Pass()
        {
            Pass("");
        }

        protected static void Pass(string text)
        {
            OutLineFormat("{0}:Passed", ConsoleColor.Green, text);
        }

        public static void UnitTestMenu(Assembly assembly, ConsoleMenu[] otherMenus, string header)
        {
            Console.WriteLine(header);
            List<ConsoleMethod> tests = UnitTest.FromAssembly(assembly);
            ShowActions(tests);
            Console.WriteLine();
            Console.WriteLine("Q to quit\ttype all to run all tests.");
            string answer = ShowSelectedMenuOrReturnAnswer(otherMenus);
            Console.WriteLine();

            try
            {
                answer = answer.Trim().ToLowerInvariant();
				RunSpecifiedTests(assembly, tests, answer);
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

        protected static void RunTest(List<ConsoleMethod> tests, string answer)
        {
			OnTestStarting();
            int selectedNumber = -1;
            try
            {
                ConsoleMethod test;
                string testName = answer;
                if (int.TryParse(answer.ToString(), out selectedNumber) && (selectedNumber - 1) > -1 && (selectedNumber - 1) < tests.Count)
                {
                    test = tests[selectedNumber - 1];
                    testName = "({0}) {1}"._Format(answer, test.Information);

                    StringBuilder header = new StringBuilder();
                    header.AppendFormat("******* Starting Test {0} ********\r\n", testName);
                    StringBuilder footer = new StringBuilder();
                    footer.AppendFormat("******* Finished Test {0} ********\r\n", testName);
                    InvokeSelection(tests, header.ToString(), footer.ToString(), selectedNumber);

					OnTestPassed(test);
                }
                else
                {
                    Console.WriteLine("Invalid entry");
                    Environment.Exit(1);
                }

                Pass(testName);
            }
            catch (Exception ex)
            {
                OnTestFailed(tests[selectedNumber - 1], ex);
            }
			OnTestFinished();
        }

        public static void RunAllUnitTests(Assembly assembly, ILogger logger = null)
        {
            UnitTestRunner runner = new UnitTestRunner(assembly, logger);
            runner.NoTestsDiscovered += (o, e) => OutLineFormat("No tests were found in {0}", ConsoleColor.Yellow, assembly.FullName);
            runner.TestsDiscovered += (o, e) =>
            {
                UnitTestsDiscoveredEventArgs args = (UnitTestsDiscoveredEventArgs)e;
                OutLineFormat("Running all tests in {0}", ConsoleColor.Green, args.Assembly.FullName);
                OutLineFormat("\tFound {0} tests", ConsoleColor.Cyan, args.Tests.Count);
            };
            runner.TestPassed += (o, e) =>
            {
                UnitTestEventArgs args = (UnitTestEventArgs)e;
                Pass(args.CurrentTest.Information);
            };
            runner.TestsFinished += (o, e) =>
            {
                UnitTestEventArgs args = (UnitTestEventArgs)e;
                TestSummary summary = args.UnitTestRunner.TestSummary;
                Out();
                OutLine("********");
                if (summary.FailedTests.Count > 0)
                {
                    OutLineFormat("({0}) tests passed", ConsoleColor.Green, summary.PassedTests.Count);
                    OutLineFormat("({0}) tests failed", ConsoleColor.Red, summary.FailedTests.Count);
                    summary.FailedTests.ForEach(cim =>
                    {
                        Out("\t");
                        OutLineFormat("{0}", new ConsoleColorCombo(ConsoleColor.Yellow, ConsoleColor.Red), cim.Test.Information);
                    });
                }
                else
                {
                    OutLineFormat("All ({0}) tests passed", ConsoleColor.Green, summary.PassedTests.Count);
                }
                OutLine("********");
            };
        }

        /// <summary>
        /// Event that fires when a test fails.
        /// </summary>
        public static event EventHandler<UnitTestExceptionEventArgs> TestFailed; //TODO: create a UnitTestEventArgs class and encapsulate unit test related data to be saved; add a test run id to UnitTestResult

		/// <summary>
		/// Event that fires when a test passes.
		/// </summary>
	    public static event EventHandler<ConsoleMethod> TestPassed;

		public static event EventHandler TestsStarting;
		public static event EventHandler TestsFinished;
		public static event EventHandler TestStarting;
		public static event EventHandler TestFinished;

        public static void Info(string message)
        {
            Info(message, new object[] { });
        }
        /// <summary>
        /// Outputs an information message to the console.
        /// </summary>
        /// <param name="message">The message text to output.</param>
        public static void Info(string messageSignature, params object[] signatureVariableValues)
        {
            Logger.AddEntry(messageSignature, ToStringArray(signatureVariableValues));
        }


        public static void Warn(string message)
        {
            Warn(message, new object[] { });
        }
        /// <summary>
        /// Outputs a warning to the console.
        /// </summary>
        /// <param name="message">The message text to output</param>
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
        /// <param name="message">The message text to output</param>
        /// <param name="ex">The Exception that occurred.</param>
        public static void Error(string messageSignature, Exception ex, params object[] signatureVariableValues)
        {
            Logger.AddEntry(messageSignature, ex, ToStringArray(signatureVariableValues));
            Logger.BlockUntilEventQueueIsEmpty();
            Logger.RestartLoggingThread();
        }

        public static void Fatal(string message, Exception ex)
        {
            Fatal(message, ex, new object[] { });
        }
        /// <summary>
        /// Outputs an error to the console and exits.
        /// </summary>
        /// <param name="message">The message to output.</param>
        /// <param name="ex">The Exception that occurred.</param>
        public static void Fatal(string messageSignature, Exception ex, params object[] signatureVariableValues)
        {
            Logger.AddEntry(messageSignature, LogEventType.Fatal, ex, ToStringArray(signatureVariableValues));
            Logger.BlockUntilEventQueueIsEmpty();
            Logger.RestartLoggingThread();
            Exit(1);
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

		private static void RunSpecifiedTests(Assembly assemblyToAnalyze, List<ConsoleMethod> tests, string answer)
		{
			string[] individuals = answer.DelimitSplit(",", true);
			string[] range = answer.DelimitSplit("-", true);
			if (answer.Equals("all"))
			{
                RunAllUnitTests(assemblyToAnalyze);
			}
			else if (range.Length == 2)
			{
				RunTestRange(tests, range);
			}
			else if (individuals.Length > 1)
			{
				RunTestSet(tests, individuals);
			}
			else
			{
				RunTest(tests, answer);
			}
		}

		private static void RunTestSet(List<ConsoleMethod> tests, string[] individuals)
		{
			OnTestsStarting();
			individuals.Each(num =>
			{
				RunTest(tests, num);
			});
			OnTestsFinished();
		}

		private static void RunTestRange(List<ConsoleMethod> tests, string[] range)
		{
			OnTestsStarting();
			int first = Convert.ToInt32(range[0]);
			int end = Convert.ToInt32(range[1]);
			List<string> answers = new List<string>();
			for (int i = first; i <= end; i++)
			{
				answers.Add(i.ToString());
			}
			answers.Each(num =>
			{
				RunTest(tests, num);
			});
			OnTestsFinished();
		}

        private static void OnTestPassed(ConsoleMethod consoleMethod)
		{
            TestPassed?.Invoke(null, consoleMethod);
        }

		private static void OnTestFailed(ConsoleMethod consoleMethod, Exception ex)
		{
            TestFailed?.Invoke(null, new UnitTestExceptionEventArgs(consoleMethod, ex, null));
        }

		private static void OnTestsStarting()
		{
            TestsStarting?.Invoke(null, EventArgs.Empty);
        }

		private static void OnTestStarting()
		{
            TestStarting?.Invoke(null, EventArgs.Empty);
        }

		private static void OnTestsFinished()
		{
            TestsFinished?.Invoke(null, EventArgs.Empty);
        }
		private static void OnTestFinished()
		{
            TestFinished?.Invoke(null, EventArgs.Empty);
        }

		private static void logger_EntryAdded(string applicationName, LogEvent logEvent)
		{
			if (MessageToConsole != null)
				MessageToConsole(applicationName, logEvent);

			if (logEvent.Severity == LogEventType.Fatal)
			{
				Environment.Exit(1);
			}
		}
    }
}
