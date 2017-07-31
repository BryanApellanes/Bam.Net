using Bam.Net.CommandLine;
using Bam.Net.ExceptionHandling;
using Bam.Net.Logging;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    public abstract class TestRunner<TTestMethod> : Loggable, ITestRunner<TTestMethod> where TTestMethod : TestMethod
    {
        static TestRunner()
        {
            _factory = new Dictionary<Type, Func<Assembly, ILogger, ITestRunner<TTestMethod>>>
            {
                { typeof(UnitTestMethod), (a, l)=> (ITestRunner<TTestMethod>)new UnitTestRunner(a, l) }
                // TODO: add IntegrationTest here
            };
        }

        Lazy<List<TTestMethod>> _tests;
        public TestRunner(Assembly assembly, TestMethodProvider<TTestMethod> testMethodProvider, ILogger logger = null)
        {
            Assembly = assembly;
            ParameterProvider = CommandLineInterface.GetParameters;
            TestMethodProvider = testMethodProvider;
            IsolateMethodCalls = true;
            logger = logger ?? Log.Default;

            _tests = new Lazy<List<TTestMethod>>(() => TestMethodProvider.GetTests());
            
            Subscribe(logger);
        }
        static Dictionary<Type, Func<Assembly, ILogger, ITestRunner<TTestMethod>>> _factory;
        public static ITestRunner<TTestMethod> Create(Assembly assembly, ILogger logger = null)
        {
            if (!_factory.ContainsKey(typeof(TTestMethod)))
            {
                Args.Throw<InvalidOperationException>("Invalid TTestMethod type specified: {0}", typeof(TTestMethod).Name);
            }
            return _factory[typeof(TTestMethod)](assembly, logger);
        }

        public event EventHandler TestPassed;
        public event EventHandler TestFailed;

        public event EventHandler TestsStarting;
        public event EventHandler TestsFinished;
        public event EventHandler TestStarting;
        public event EventHandler TestFinished;

        public event EventHandler TestsDiscovered;
        public event EventHandler NoTestsDiscovered;

        public event EventHandler InvalidTestNumberSpecified;

        public Action<Exception> BeforeAllExceptionHandler { get; set; }
        public Action<Exception> BeforeEachExceptionHandler { get; set; }

        public Action<Exception> AfterAllExceptionHandler { get; set; }
        public Action<Exception> AfterEachExceptionHandler { get; set; }

        public TestSummary TestSummary { get; set; }

        public Func<MethodInfo, object[]> ParameterProvider { get; set; }

        public TestMethodProvider<TTestMethod> TestMethodProvider { get; set; }

        public bool IsolateMethodCalls { get; set; }
        public void RunAllTests()
        {
            AttachBeforeHandlers();
            AttachAfterHandlers();
            List<TTestMethod> tests = TestMethodProvider.GetTests();
            if (tests.Count == 0)
            {
                FireEvent(NoTestsDiscovered);
                return;
            }

            FireEvent(TestsDiscovered, new TestsDiscoveredEventArgs<TTestMethod> { Assembly = Assembly, TestRunner = this, Tests = tests.Select(t=> (TestMethod)t).ToList() });
            FireEvent(TestsStarting);
            foreach (TestMethod test in tests)
            {
                RunTest(test);
            }
            FireEvent(TestsFinished, new TestEventArgs<TTestMethod> { TestRunner = this });
        }

        /// <summary>
        /// Run the tests specified by testIdentifier which should be in the 
        /// following forms: N1,N2,N3... or N1-Nn or N or "all"
        /// </summary>
        /// <param name="testIdentifier"></param>
        public void RunSpecifiedTests(string testIdentifier)
        {
            string[] individualTests = testIdentifier.DelimitSplit(",", true);
            string[] range = testIdentifier.DelimitSplit("-", true);
            if (testIdentifier.Equals("all", StringComparison.InvariantCultureIgnoreCase))
            {
                RunAllTests();
            }
            else if (range.Length == 2)
            {
                RunTestRange(range[0], range[1]);
            }
            else if (individualTests.Length > 1)
            {
                RunTestSet(individualTests);
            }
            else
            {
                RunTest(testIdentifier.Trim());
            }
        }

        public void RunTestRange(string fromNumber, string toNumber)
        {
            if(IsInRange(fromNumber, out int fromInt) && IsInRange(toNumber, out int toInt))
            {
                for(int i = fromInt; i <= toInt; i++)
                {
                    RunTest(Tests[i]);
                }
            }
        }

        public void RunTestSet(string[] testNumbers)
        {
            foreach(string testNumber in testNumbers)
            {
                RunTest(testNumber);
            }
        }

        public void RunTest(string testNumber)
        {
            if (IsInRange(testNumber, out int selectedNumber))
            {
                RunTest(Tests[selectedNumber]);
            }
            else
            {
                FireEvent(InvalidTestNumberSpecified, new TestEventArgs<TTestMethod> { TestRunner = this });
            }
        }

        public void RunTest(TestMethod test)
        {
            TestEventArgs<TTestMethod> args = new TestEventArgs<TTestMethod> { CurrentTest = test, TestRunner = this };
            FireEvent(TestStarting, args);
            try
            {
                InvokeTest(test, ParameterProvider, IsolateMethodCalls);
                TestSummary.PassedTests.Add(test);
                FireEvent(TestPassed, args);
            }
            catch (ReflectionTypeLoadException rtle)
            {
                Exception ex = new ReflectionTypeLoadAggregateException(rtle);
                TestSummary.FailedTests.Add(new FailedTest { Test = test, Exception = ex });
                FireEvent(TestFailed, new TestExceptionEventArgs(test, ex));
            }
            catch (Exception ex)
            {
                TestSummary.FailedTests.Add(new FailedTest { Test = test, Exception = ex });
                FireEvent(TestFailed, new TestExceptionEventArgs(test, ex));
            }
            FireEvent(TestFinished, args);
        }

        protected Assembly Assembly { get; set; }

        protected List<TTestMethod> Tests
        {
            get
            {
                return _tests.Value;
            }
        }

        protected bool IsInRange(string testNumber, out int selectedNumber)
        {
            return int.TryParse(testNumber, out selectedNumber) && (selectedNumber - 1) > -1 && (selectedNumber - 1) < Tests.Count;
        }

        protected internal static void InvokeTest(ConsoleMethod consoleMethod, Func<MethodInfo, object[]> parameterProvider, bool isolateMethodCalls = true)
        {
            object[] parameters = parameterProvider(consoleMethod.Method);
            MethodInfo invokeTarget = typeof(ConsoleMethod).GetMethod("Invoke");
            if (consoleMethod.Method.IsStatic)
            {
                if (isolateMethodCalls)
                {
                    CommandLineInterface.InvokeInSeparateAppDomain(invokeTarget, consoleMethod);
                }
                else
                {
                    CommandLineInterface.InvokeInCurrentAppDomain(invokeTarget, consoleMethod);
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
                if (isolateMethodCalls)
                {
                    CommandLineInterface.InvokeInSeparateAppDomain(invokeTarget, consoleMethod);
                }
                else
                {
                    CommandLineInterface.InvokeInCurrentAppDomain(invokeTarget, consoleMethod);
                }
            }
        }

        protected void AttachBeforeHandlers()
        {
            List<ConsoleMethod> beforeAll = ConsoleMethod.FromAssembly<BeforeUnitTests>(Assembly);
            List<ConsoleMethod> beforeEach = ConsoleMethod.FromAssembly<BeforeEachUnitTest>(Assembly);
            TestsStarting += (o, e) =>
            {
                beforeAll.Each(cim =>
                {
                    cim.TryInvoke(BeforeAllExceptionHandler);
                });
            };
            TestStarting += (o, e) =>
            {
                beforeEach.Each(cim =>
                {
                    cim.TryInvoke(BeforeEachExceptionHandler);
                });
            };
        }

        protected void AttachAfterHandlers()
        {
            List<ConsoleMethod> afterAll = ConsoleMethod.FromAssembly<AfterUnitTests>(Assembly);
            List<ConsoleMethod> afterEach = ConsoleMethod.FromAssembly<AfterEachUnitTest>(Assembly);
            TestsFinished += (o, e) =>
            {
                afterAll.Each(cim =>
                {
                    cim.TryInvoke(AfterAllExceptionHandler);
                });
            };
            TestFinished += (o, e) =>
            {
                afterEach.Each(cim =>
                {
                    cim.TryInvoke(AfterEachExceptionHandler);
                });
            };
        }

        public List<TTestMethod> GetTests()
        {
            return Tests;
        }
    }
}
