/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Naizari.Logging;
using Naizari.Helpers;
using Naizari.Testing;
using Naizari.Configuration;

namespace Naizari.Extensions
{
    [Serializable]
    public abstract class CommandLineTestInterface: CommandLineInterface
    {
        public const string UnitTestAttribute = "UnitTestAttribute";

        protected static ILogger logger;
        public static LogEntryAddedListener MessageToConsole;
        protected static bool interactive;

        protected static MethodInfo DefaultMethod { get; set; }

        protected static void Initialize(string[] args)
        {
            InitLogger();

            ArgsParsedError += delegate(ParsedArguments a)
            {
                throw new ArgumentException(a.Message);
            };

            ArgsParsed += new ConsoleArgsParsedDelegate(ArgumentParsingComplete);

            interactive = false;

            AddValidArgument("i", true);

            ParseArgs(args);

            TestFailed += (o, t) =>
            {
                Out("Test Failed: " + t.ConsoleInvokeableMethod.Information + "\r\n", ConsoleTextColor.Red);
                Out(t.Exception.Message, ConsoleTextColor.Magenta);
                Out();
                Out(t.Exception.StackTrace, ConsoleTextColor.Red);
                Out("---", ConsoleTextColor.Red);
                Out();
            };

            if (!Arguments.Contains("i"))
            {
                if (DefaultMethod != null)
                {
                    Expect.IsTrue(DefaultMethod.IsStatic, "DefaultMethod must be static.");
                    Expect.AreEqual(0, DefaultMethod.GetParameters().Length, "The DefaultMethod must take no parameters.");
                    DefaultMethod.Invoke(null, null);
                    return;
                }
                else
                {
                    RunAllTests(Assembly.GetEntryAssembly());
                    return;
                }
            }

            Interactive();
        }

        protected static void InitLogger()
        {
            logger = Log.CreateLogger("Console");
            ((ConsoleLogger)logger).UseColors = true;
            logger.EntryAdded += new LogEntryAddedListener(logger_EntryAdded);
        }

        protected static void Interactive()
        {
            AddMenu(Assembly.GetEntryAssembly(), "Main", 'm', new ConsoleMenuDelegate(MainMenu));
            AddMenu(Assembly.GetEntryAssembly(), "Test", 't', new ConsoleMenuDelegate(TestMenu));

            MainMenu(Assembly.GetEntryAssembly(), Menus.ToArray(), "Select an action");
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
            OutFormat("{0}:Passed", ConsoleTextColor.Green, text);
        }

        public static void TestMenu(Assembly assemblyToAnalyze, ConsoleMenu[] otherMenus, string header)
        {
            Console.WriteLine(header);
            List<ConsoleInvokeableMethod> tests = GetTests(assemblyToAnalyze);
            ShowMenu(tests);
            Console.WriteLine();
            Console.WriteLine("Q to quit\ttype all to run all tests.");
            string answer = ShowSelectedMenuOrReturnAnswer(otherMenus);

            try
            {
                if (answer.Trim().ToLower().Equals("all"))
                {
                    bool enterManually = true;
                    if (interactive)
                        enterManually = Confirm("Would you like to enter parameters manually? [y][N]");
                    if (enterManually)
                        RunAllTestsInteractively(assemblyToAnalyze);
                    else
                        RunAllTests(assemblyToAnalyze);
                }
                else
                {
                    
                    RunTest(tests, answer);
                }
            }
            catch (Exception ex)
            {                
                Error("An error occurred running tests", ex);                
            }

            if (Confirm("Return to the Test menu? [y][N]"))
                TestMenu(assemblyToAnalyze, otherMenus, header);
            else
                Exit(0);
        }

        protected static void RunTest(List<ConsoleInvokeableMethod> tests, string answer)
        {
            int selectedIndex = -1;
            try
            {
                InvokeSelection(tests, answer, "******* Starting Test " + answer + "********", "******* Test " + answer + " Finished********", out selectedIndex);

                Pass(tests[selectedIndex].Method.Name.PascalSplit(" "));
            }
            catch (Exception ex)
            {
                OnTestFailed(tests[selectedIndex], ex);
            }
            
        }

        protected static void RunAllTests(Assembly assemblyToAnalyze)
        {
            RunAllTests(assemblyToAnalyze, true);
        }

        protected static void RunAllTestsInteractively(Assembly assemblyToAnalyze)
        {
            RunAllTests(assemblyToAnalyze, false);
        }

        /// <summary>
        /// Event fires when a test fails.
        /// </summary>
        public static event EventHandler<TestExceptionEventArgs> TestFailed;

        protected static void RunAllTests(Assembly assemblyToAnalyze, bool generateParameters)
        {
            List<ConsoleInvokeableMethod> tests = GetTests(assemblyToAnalyze);
            if (tests.Count == 0)
            {
                Info("No tests were found, make sure that the version of the Microsoft.VisualStudio.QualityTools.UnitTestFramework matches the one referenced by your project");
                return;
            }

            Info("Running all tests");
            
            foreach (ConsoleInvokeableMethod consoleMethod in tests)
            {
               
                try
                {
                    InvokeTest(consoleMethod, generateParameters);
                    Pass(consoleMethod.Method.Name.PascalSplit(" "));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        ex = ex.InnerException;

                    OnTestFailed(consoleMethod, ex);
                }
            }
        }

        private static void OnTestFailed(ConsoleInvokeableMethod consoleMethod, Exception ex)
        {
            if (TestFailed != null)
            {
                TestFailed(null, new TestExceptionEventArgs(consoleMethod, ex));
            }
        }

        private static void InvokeTest(ConsoleInvokeableMethod consoleMethod)
        {
            InvokeTest(consoleMethod, false);
        }

        private static void InvokeTest(ConsoleInvokeableMethod consoleMethod, bool generateParameters)
        {
            object[] parameters = GetParameterInput(consoleMethod.Method, generateParameters);
            if (consoleMethod.Method.IsStatic)
            {
                InvokeInSeparateAppDomain(consoleMethod.Method, null);
                //consoleMethod.Method.Invoke(null, parameters);              
            }
            else
            {
                string typeName = consoleMethod.Method.DeclaringType.Name;
                ConstructorInfo ctor = consoleMethod.Method.DeclaringType.GetConstructor(Type.EmptyTypes);
                if (ctor == null)
                    ExceptionHelper.ThrowInvalidOperation("The declaring type {0} of method {1} does not have a parameterless constructor, test cannot be run.", typeName, consoleMethod.Method.Name);

                object instance = ctor.Invoke(null);
                Expect.IsNotNull(instance, string.Format("Unable to instantiate declaring type {0} of method {1}", typeName, consoleMethod.Method.Name));

                InvokeInSeparateAppDomain(consoleMethod.Method, instance);
                //consoleMethod.Method.Invoke(instance, null);
            }
        }

        static void logger_EntryAdded(string applicationName, LogEvent logEvent)
        {
            if (MessageToConsole != null)
                MessageToConsole(applicationName, logEvent);

            if (logEvent.Severity == LogEventType.Fatal)
            {
                Environment.Exit(1);
            }
        }

        public static List<ConsoleInvokeableMethod> GetTests(Assembly assemblyToAnalyze )
        {
            Type[] testMethodAttributes = GetTestMethodAttributeTypes();
            List<ConsoleInvokeableMethod> tests =  new List<ConsoleInvokeableMethod>();
            foreach (Type testMethodAttribute in testMethodAttributes)
            {
                 tests.AddRange(GetActions(assemblyToAnalyze, testMethodAttribute));
            }

            foreach (ConsoleInvokeableMethod method in tests)
            {
                MethodInfo info = method.Method;
                ConsoleAction actionInfo = null;
                if (HasCustomAttributeOfType<ConsoleAction>(info, out actionInfo))
                {
                    method.Attribute = actionInfo;
                }
            }

            return tests;
        }

        private static Type[] GetTestMethodAttributeTypes()
        {
            List<Type> returnValues = new List<Type>();
            returnValues.Add(typeof(UnitTest));

            string attrName = DefaultConfiguration.GetAppSetting(UnitTestAttribute);
            Type fromConfig = Type.GetType(attrName);
            if (fromConfig != null)
            {
                returnValues.Add(fromConfig);
            }
            
            return returnValues.ToArray();
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
            //List<string> variableValues = ToStringArray(signatureVariableValues);
            logger.AddEntry(messageSignature, ToStringArray(signatureVariableValues));
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
            Exit(1);
        }


    }
}
