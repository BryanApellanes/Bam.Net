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

        protected static ILogger logger;
		public static LogEntryAddedListener MessageToConsole { get; set; }
        protected static bool interactive;

        protected static MethodInfo DefaultMethod { get; set; }

		public static void Initialize(string[] args, ConsoleArgsParsedDelegate parseErrorHandler = null)
		{
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

			AddValidArgument("i", true, "Run interactively");
			AddValidArgument("?", true, "Show usage");
			AddValidArgument("t", true, "Run all unit tests");
            AddValidArgument("it", true, "Run all integration tests");

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
				else if (Arguments.Contains("t"))
				{
					RunAllUnitTests(Assembly.GetEntryAssembly());
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
            logger = Log.CreateLogger(typeof(ConsoleLogger));
            ((ConsoleLogger)logger).UseColors = true;
			logger.StartLoggingThread();
            logger.EntryAdded += new LogEntryAddedListener(logger_EntryAdded);
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
			List<ConsoleInvokeableMethod> beforeAll = GetConsoleInvokeableMethods<BeforeUnitTests>(assembly);
			List<ConsoleInvokeableMethod> beforeEach = GetConsoleInvokeableMethods<BeforeEachUnitTest>(assembly);
			List<ConsoleInvokeableMethod> afterAll = GetConsoleInvokeableMethods<AfterUnitTests>(assembly);
			List<ConsoleInvokeableMethod> afterEach = GetConsoleInvokeableMethods<AfterEachUnitTest>(assembly);

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

		private static void TryInvoke<T>(ConsoleInvokeableMethod cim)
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


		protected static bool IsInteractive { get; set; }

        public static void Interactive()
        {
			IsInteractive = true;
            AddMenu(Assembly.GetEntryAssembly(), "Main", 'm', new ConsoleMenuDelegate(ShowMenu));
            AddMenu(Assembly.GetEntryAssembly(), "Test", 't', new ConsoleMenuDelegate(UnitTestMenu));

            ShowMenu(Assembly.GetEntryAssembly(), OtherMenus.ToArray(), "Main");
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

        public static void UnitTestMenu(Assembly assemblyToAnalyze, ConsoleMenu[] otherMenus, string header)
        {
            Console.WriteLine(header);
            List<ConsoleInvokeableMethod> tests = GetUnitTests(assemblyToAnalyze);
            ShowActions(tests);
            Console.WriteLine();
            Console.WriteLine("Q to quit\ttype all to run all tests.");
            string answer = ShowSelectedMenuOrReturnAnswer(otherMenus);
            Console.WriteLine();

            try
            {
                answer = answer.Trim().ToLowerInvariant();
				RunSpecifiedTests(assemblyToAnalyze, tests, answer);
            }
            catch (Exception ex)
            {                
                Error("An error occurred running tests", ex);                
            }

            if (Confirm("Return to the Test menu? [y][N]"))
            {
                UnitTestMenu(assemblyToAnalyze, otherMenus, header);
            }
            else
            {
                Exit(0);
            }
        }

        protected static void RunTest(List<ConsoleInvokeableMethod> tests, string answer)
        {
			OnTestStarting();
            int selectedNumber = -1;
            try
            {
                ConsoleInvokeableMethod test;
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

        public static void RunAllUnitTests(Assembly assemblyToAnalyze)
        {
            RunAllUnitTests(assemblyToAnalyze, true);
        }

        protected static void RunAllTestsInteractively(Assembly assemblyToAnalyze)
        {
            RunAllUnitTests(assemblyToAnalyze, false);
        }

        /// <summary>
        /// Event that fires when a test fails.
        /// </summary>
        public static event EventHandler<TestExceptionEventArgs> TestFailed;

		/// <summary>
		/// Event that fires when a test passes.
		/// </summary>
	    public static event EventHandler<ConsoleInvokeableMethod> TestPassed;

		public static event EventHandler TestsStarting;
		public static event EventHandler TestsFinished;
		public static event EventHandler TestStarting;
		public static event EventHandler TestFinished;

		protected internal static void RunAllUnitTests(Assembly assemblyToAnalyze, bool generateParameters, bool finalOut = true)
		{
			List<ConsoleInvokeableMethod> tests = GetUnitTests(assemblyToAnalyze);
			string assemblyName = assemblyToAnalyze.FullName;
			if (tests.Count == 0)
			{
				OutLineFormat("No tests were found in {0}", ConsoleColor.Yellow, assemblyName);
				return;
			}

			OutLineFormat("Running all tests in {0}", ConsoleColor.Green, assemblyName);
			OutLineFormat("\tFound {0} tests", ConsoleColor.Cyan, tests.Count);
			RunAllUnitTests(generateParameters, finalOut, tests);
		}

        public static List<ConsoleInvokeableMethod> GetUnitTests(Assembly assemblyToAnalyze )
        {
            List<ConsoleInvokeableMethod> tests =  new List<ConsoleInvokeableMethod>();
			tests.AddRange(GetConsoleInvokeableMethods(assemblyToAnalyze, typeof(UnitTest)));
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }
		
		public static List<ConsoleInvokeableMethod> GetUnitTests(Type unitTestContainingClass)
		{
			List<ConsoleInvokeableMethod> tests = new List<ConsoleInvokeableMethod>();
			tests.AddRange(GetConsoleInvokeableMethods(unitTestContainingClass, typeof(UnitTest)));
			tests.Sort((l, r) => l.Information.CompareTo(r.Information));
			return tests;
		}

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
            logger.AddEntry(messageSignature, ToStringArray(signatureVariableValues));
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
            logger.AddEntry(messageSignature, LogEventType.Warning, ToStringArray(signatureVariableValues));
            logger.BlockUntilEventQueueIsEmpty();
            logger.RestartLoggingThread();
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
            logger.AddEntry(messageSignature, ex, ToStringArray(signatureVariableValues));
            logger.BlockUntilEventQueueIsEmpty();
            logger.RestartLoggingThread();
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
            logger.AddEntry(messageSignature, LogEventType.Fatal, ex, ToStringArray(signatureVariableValues));
            logger.BlockUntilEventQueueIsEmpty();
            logger.RestartLoggingThread();
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

		private static void RunSpecifiedTests(Assembly assemblyToAnalyze, List<ConsoleInvokeableMethod> tests, string answer)
		{
			string[] individuals = answer.DelimitSplit(",", true);
			string[] range = answer.DelimitSplit("-", true);
			if (answer.Equals("all"))
			{
				PromptAndRunAllTests(assemblyToAnalyze);
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

		private static void RunTestSet(List<ConsoleInvokeableMethod> tests, string[] individuals)
		{
			OnTestsStarting();
			individuals.Each(num =>
			{
				RunTest(tests, num);
			});
			OnTestsFinished();
		}

		private static void RunTestRange(List<ConsoleInvokeableMethod> tests, string[] range)
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

		private static void PromptAndRunAllTests(Assembly assemblyToAnalyze)
		{
			bool enterManually = true;
			if (interactive)
			{
				enterManually = Confirm("Would you like to enter parameters manually? [y][N]");
			}
			if (enterManually)
			{
				RunAllTestsInteractively(assemblyToAnalyze);
			}
			else
			{
				RunAllUnitTests(assemblyToAnalyze);
			}
		}

		private static void RunAllUnitTests(bool generateParameters, bool finalOut, List<ConsoleInvokeableMethod> tests)
		{
			int passedCount = 0;
			int failedCount = 0;
			OnTestsStarting();
			foreach (ConsoleInvokeableMethod consoleMethod in tests)
			{
				OnTestStarting();
				try
				{
					InvokeTest(consoleMethod, generateParameters);
					Pass(consoleMethod.Information);
					passedCount++;
					OnTestPassed(consoleMethod);
				}
				catch (Exception ex)
				{
                    if (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }

					OnTestFailed(consoleMethod, ex);
					failedCount++;
				}
				OnTestFinished();
			}
			if (finalOut)
			{
				Out();
				OutLine("********");
				if (failedCount > 0)
				{
					OutLineFormat("({0}) tests passed", ConsoleColor.Green, passedCount);
					OutLineFormat("({0}) tests failed", ConsoleColor.Red, failedCount);
				}
				else
				{
					OutLineFormat("All ({0}) tests passed", ConsoleColor.Green, passedCount);
				}
				OutLine("********");
			}
			OnTestsFinished();
		}

		private static void OnTestPassed(ConsoleInvokeableMethod consoleMethod)
		{
			if (TestPassed != null)
			{
				TestPassed(null, consoleMethod);
			}
		}

		private static void OnTestFailed(ConsoleInvokeableMethod consoleMethod, Exception ex)
		{
			if (TestFailed != null)
			{
				TestFailed(null, new TestExceptionEventArgs(consoleMethod, ex));
			}
		}

		private static void OnTestsStarting()
		{
			if (TestsStarting != null)
			{
				TestsStarting(null, EventArgs.Empty);
			}
		}

		private static void OnTestStarting()
		{
			if (TestStarting != null)
			{
				TestStarting(null, EventArgs.Empty);
			}
		}

		private static void OnTestsFinished()
		{
			if (TestsFinished != null)
			{
				TestsFinished(null, EventArgs.Empty);
			}
		}
		private static void OnTestFinished()
		{
			if (TestFinished != null)
			{
				TestFinished(null, EventArgs.Empty);
			}
		}

		private static void InvokeTest(ConsoleInvokeableMethod consoleMethod)
		{
			InvokeTest(consoleMethod, true);
		}

		private static void InvokeTest(ConsoleInvokeableMethod consoleMethod, bool generateParameters)
		{
			object[] parameters = GetParameterInput(consoleMethod.Method, generateParameters);
			MethodInfo invoke = typeof(ConsoleInvokeableMethod).GetMethod("Invoke");
			if (consoleMethod.Method.IsStatic)
			{
				if (IsolateMethodCalls)
				{
					InvokeInSeparateAppDomain(invoke, consoleMethod);
				}
				else
				{
					InvokeInCurrentAppDomain(invoke, consoleMethod);
				}
			}
			else
			{
				string typeName = consoleMethod.Method.DeclaringType.Name;
				ConstructorInfo ctor = consoleMethod.Method.DeclaringType.GetConstructor(Type.EmptyTypes);
				if (ctor == null)
					ExceptionHelper.ThrowInvalidOperation("The declaring type {0} of method {1} does not have a parameterless constructor, test cannot be run.", typeName, consoleMethod.Method.Name);

				object instance = ctor.Invoke(null);
				Expect.IsNotNull(instance, string.Format("Unable to instantiate declaring type {0} of method {1}", typeName, consoleMethod.Method.Name));

				consoleMethod.Provider = instance;
				if (IsolateMethodCalls)
				{
					InvokeInSeparateAppDomain(invoke, consoleMethod);
				}
				else
				{
					InvokeInCurrentAppDomain(invoke, consoleMethod);
				}
			}
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
