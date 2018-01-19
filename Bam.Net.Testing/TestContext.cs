/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Testing
{
    /// <summary>
    /// The context specific to a single set of tests.  Tracks
    /// the SetupContext, the test delegate and the assertions
    /// made during the verification phase of the test.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestContext<T>
    {
        SetupContext setupContext;
        Because because;
        Action<T> testMethod;
        Action<T, SetupContext> altTestMethod;
        Func<T, object> outputAction;

        internal TestContext(SetupContext setupContext, string testDescription)
        {
            this.setupContext = setupContext;
            this.because = new Because(testDescription, setupContext);
            this.testMethod = (o) => { };
            this.altTestMethod = (o, c) => { };
            this.outputAction = (o) => o;
        }

        /// <summary>
        /// Creates a new test Context instance using the specified setupContext and 
        /// test description.
        /// </summary>
        /// <param name="setupContext">The setup or initialization context used for this
        /// test.</param>
        /// <param name="testDescription">The description of the current test.</param>
        /// <param name="testMethod">The delegate containing the test actions</param>
        public TestContext(SetupContext setupContext, string testDescription, Action<T> testMethod)
            : this(setupContext, testDescription)
        {
            this.testMethod = testMethod;
        }

        public TestContext(SetupContext setupContext, string testDescription, Action<T, SetupContext> altTestMethod)
            : this(setupContext, testDescription)
        {
            this.altTestMethod = altTestMethod;
        }

        /// <summary>
        /// Creates a new test Context instance using the specified setupContext and 
        /// test description.
        /// </summary>
        /// <param name="setupContext">The setup or initialization context used for this
        /// test.</param>
        /// <param name="testDescription">The description of the current test.</param>
        /// <param name="outputAction">The delegate containing the test which returns a value
        /// that can be validated during the verification phase of the test.</param>
        public TestContext(SetupContext setupContext, string testDescription, Func<T, object> outputAction)
            : this(setupContext, testDescription)
        {
            this.outputAction = outputAction;
        }

        /// <summary>
        /// Causes the test to run, same as It.
        /// </summary>
        public TestContext<T> TheTest
        {
            get
            {
                return It;
            }
        }

        bool run;
        /// <summary>
        /// Causes the test to run, same as TheTest.
        /// </summary>
        public TestContext<T> It
        {
            get
            {
                if (!run)
                {
                    run = true;
                    try
                    {
                        T objectUnderTest = setupContext.Get<T>();
                        testMethod(objectUnderTest);
                        altTestMethod(objectUnderTest, setupContext);
                        because.Result = outputAction(objectUnderTest);
                        setupContext.ObjectUnderTest = objectUnderTest;
                    }
                    catch (Exception ex)
                    {
                        because.ExceptionWasThrown(ex);
                    }
                }
                return this;
            }
        }

        /// <summary>
        /// The entry point into test validation.  Calls the specified
        /// actionToAssertResults passing it the Because object of the 
        /// current test Context.
        /// </summary>
        /// <param name="actionToAssertResults"></param>
        /// <returns></returns>
        public TestContext<T> ShouldPass(Action<Because> actionToAssertResults)
        {
            actionToAssertResults(because);
            return this;
        }

        /// <summary>
        /// The entry point into test validation.  Calls the specified 
        /// actionToAssertResults passing it the Because object of the
        /// current test context and the object under test.
        /// </summary>
        /// <param name="actionToAssertResults"></param>
        /// <returns></returns>
        public TestContext<T> ShouldPass(Action<Because, T> actionToAssertResults)
        {
            try
            {
                actionToAssertResults(because, (T)setupContext.ObjectUnderTest);
            }
            catch (Exception ex)
            {
                because.ExceptionWasThrown(ex);
            }
            return this;
        }

        public TestContext<T> ShouldPass(Action<Because, AssertionWrapper> actionToAssertResults)
        {
            try
            {
                actionToAssertResults(because, new AssertionWrapper(because, setupContext.ObjectUnderTest, "Object Under Test"));
            }
            catch (Exception ex)
            {
                because.ExceptionWasThrown(ex);
            }
            return this;
        }

        /// <summary>
        /// Calls Write() on the IBecauseWriter for the current test and
        /// marks the test complete.
        /// </summary>
        /// <returns></returns>
        public Because SoBeHappy()
        {
            return because.TestIsDone;
        }

        /// <summary>
        /// Calls Write() on the IBecauseWriter for the current test and
        /// marks the test complete then runs the specified cleanup action.  
        /// Same as Cleanup.
        /// </summary>
        /// <param name="cleanup"></param>
        /// <returns></returns>
        public Because SoBeHappy(Action<SetupContext> cleanup)
        {
            return Cleanup(cleanup);
        }

        /// <summary>
        /// Calls Write() on the IBecauseWriter for the current test and
        /// marks the test complete then runs the specified cleanup action.  
        /// Same as SoBeHappy.
        /// </summary>
        /// <param name="cleanup"></param>
        /// <returns></returns>
        public Because Cleanup(Action<SetupContext> cleanup)
        {
            return because.TestIsDone.CleanUp(cleanup);
        }
    }
}
