using Bam.Net.CommandLine;
using Bam.Net.ExceptionHandling;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Unit
{
    public class UnitTestRunner: Loggable
    {
        public UnitTestRunner(Assembly assembly, ILogger logger = null)
        {
            Assembly = assembly;
            ParameterProvider = CommandLineInterface.GetParameters;
            Subscribe(logger ?? Log.Default);
        }

        public event EventHandler TestPassed;
        public event EventHandler TestFailed;

        public event EventHandler TestsStarting;
        public event EventHandler TestsFinished;
        public event EventHandler TestStarting;
        public event EventHandler TestFinished;

        public event EventHandler TestsDiscovered;
        public event EventHandler NoTestsDiscovered;

        public Action<Exception> BeforeAllExceptionHandler { get; set; }
        public Action<Exception> BeforeEachExceptionHandler { get; set; }

        public Action<Exception> AfterAllExceptionHandler { get; set; }
        public Action<Exception> AfterEachExceptionHandler { get; set; }

        public TestSummary TestSummary { get; set; }

        public Func<MethodInfo, object[]> ParameterProvider { get; set; }

        public bool IsolateMethodCalls { get; set; }
        public void RunAllUnitTests()
        {
            AttachBeforeHandlers();
            AttachAfterHandlers();
            List<ConsoleMethod> tests = UnitTest.FromAssembly(Assembly);
            if (tests.Count == 0)
            {
                FireEvent(NoTestsDiscovered);
                return;
            }

            FireEvent(TestsDiscovered, new UnitTestsDiscoveredEventArgs { Assembly = Assembly, UnitTestRunner = this, Tests = tests });
            FireEvent(TestsStarting);
            foreach (ConsoleMethod test in tests)
            {
                UnitTestEventArgs args = new UnitTestEventArgs { CurrentTest = test, UnitTestRunner = this };
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
                    FireEvent(TestFailed, new UnitTestExceptionEventArgs(test, ex, this));
                }
                catch (Exception ex)
                {
                    TestSummary.FailedTests.Add(new FailedTest { Test = test, Exception = ex });
                    FireEvent(TestFailed, new UnitTestExceptionEventArgs(test, ex, this));
                }
                FireEvent(TestFinished, args);
            }
            FireEvent(TestsFinished, new UnitTestEventArgs { UnitTestRunner = this });
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
        protected Assembly Assembly { get; set; }
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

    }
}
