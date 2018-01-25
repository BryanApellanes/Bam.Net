/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bam.Net.Testing
{
    /// <summary>
    /// Provides a mechanism by which assertions are tracked for a test.
    /// </summary>
    public class Because
    {
        SetupContext setupContext;
        List<Assertion> assertions;
        internal Because(string testDescription, SetupContext setupContext)
        {
            this.TestDescription = testDescription;
            this.assertions = new List<Assertion>();
            this.setupContext = setupContext;
        }

        /// <summary>
        /// Gets the desciption of the current test being run
        /// </summary>
        public string TestDescription
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the SetupContext instance for the current test.
        /// </summary>
        public SetupContext SetupContext
        {
            get
            {
                return this.setupContext;
            }
        }

        /// <summary>
        /// Gets the object under test from the underlying SetupContext.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ObjectUnderTest<T>()
        {
            return (T)this.setupContext.ObjectUnderTest;
        }

        /// <summary>
        /// Gets a value indicating whether the current test has passed.
        /// </summary>
        public bool Passed
        {
            get
            {
                return (from item in assertions
                        where item.Passed == false
                        select item).FirstOrDefault() == null;
            }
        }

        /// <summary>
        /// Asserts that the specified value is true
        /// </summary>
        /// <param name="descriptionOfTrueAssertion">A description of the true value.  Read as:  ItsTrue "Michael Jordan is the best of all time"</param>
        /// <param name="shouldBeTrue"></param>
        /// <param name="failureMessage"></param>
        public void ItsTrue(string descriptionOfTrueAssertion, bool shouldBeTrue, string failureMessage = "")
        {
            assertions.Add(
                new Assertion 
                { 
                    Passed = shouldBeTrue == true, 
                    SuccessMessage = descriptionOfTrueAssertion,
                    FailureMessage = failureMessage
                }); 
        }

        public void ItsTrue(string descriptionOfTrueAssertion, Action doesntThrow, string failureMessage = "")
        {
            assertions.Add(
                new Assertion
                {
                    Passed = doesntThrow.Try(),
                    SuccessMessage = descriptionOfTrueAssertion,
                    FailureMessage = failureMessage
                });
        }
                
        /// <summary>
        /// Asserts that the specified value is false
        /// </summary>
        /// <param name="descriptionOfFalseAssertion">A descriptioni of the false value.  Read as: ItsFalse "John Stockton was the " </param>
        /// <param name="shouldBeFalse"></param>
        /// <param name="failureMessage"></param>
        public void ItsFalse(string descriptionOfFalseAssertion, bool shouldBeFalse, string failureMessage = "")
        {
            assertions.Add(
                new Assertion 
                { 
                    Passed = shouldBeFalse == false, 
                    SuccessMessage = descriptionOfFalseAssertion,
                    FailureMessage = failureMessage
                });
        }
        
        /// <summary>
        /// Asserts that the type of the result of the test Function 
        /// is the same as the type specified by generic type T.  Only valid
        /// if the test method returned a value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ResultIs<T>()
        {
            Type type = typeof(T);
            assertions.Add(
                new Assertion
                {
                    Passed = Result == null ? false: Result.GetType() == typeof(T),
                    SuccessMessage = string.Format("result is of type {0}", type.Name),
                    FailureMessage = string.Format("result is NOT of type {0}", type.Name)
                });
        }

        /// <summary>
        /// Asserts that the result of the test function
        /// is equal to the specified object using the .Equals 
        /// method.
        /// </summary>
        /// <param name="obj"></param>
        public void ResultEquals(object obj)
        {
            assertions.Add(
                new Assertion
                {
                    Passed = Result.Equals(obj),
                    SuccessMessage = string.Format("result equals the specified value ({0})", obj.ToString()),
                    FailureMessage = string.Format("result does NOT equal the specified value ({0})", obj.ToString())
                });
        }

        /// <summary>
        /// Asserts that the result of the test function
        /// is the same as the specified object using
        /// the equality comparison operator ==
        /// </summary>
        /// <param name="obj"></param>
        public void ResultIsSameAs(object obj)
        {
            assertions.Add(
                new Assertion
                {
                    Passed = Result == obj,
                    SuccessMessage = string.Format("result is same as the specified value ({0})", obj.ToString()),
                    FailureMessage = string.Format("result is NOT same as the specified value ({0})", obj.ToString())
                });
        }

        /// <summary>
        /// Does not perform an assertion, rather outputs the string representation of the specified obj
        /// using ToString().
        /// </summary>
        /// <param name="obj"></param>
        public void IllLookAtIt(object obj)
        {
            assertions.Add(
                new Assertion
                {
                    Passed = true,
                    SuccessMessage = string.Format("I'll inspect the value ({0})", obj.ToString())
                });
        }

        /// <summary>
        /// Does not perform an assertion, rather outputs the properties of the
        /// result of the test function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void IllLookAtTheResultsProperties<T>()
        {
            IllLookAtItsProperties<T>((T)Result);
        }

        /// <summary>
        /// Does not perform an assertion, rather outputs the properties of the specified obj.
        /// </summary>
        /// <param name="obj"></param>
        public void IllLookAtItsProperties<T>(T obj)
        {
            assertions.Add(
                new Assertion
                {
                    Passed = true,
                    SuccessMessage = string.Format("I'll inspect the properties:\r\n{0}", obj.PropertiesToString())
                });
        }
        
        /// <summary>
        /// Asserts that the result inherits from (derives from/is a subclass of) the specified
        /// generic type T.  Only valid if the test method returned a value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ResultDerivesFrom<T>()
        {
            Type type = typeof(T);
            assertions.Add(
                new Assertion
                {
                    Passed = Result.GetType().IsSubclassOf(type),
                    SuccessMessage = string.Format("result is a subclass of type {0}", type.Name),
                    FailureMessage = string.Format("result is NOT of type {0}", type.Name)
                });
        }

        internal void ExceptionWasThrown(Exception ex)
        {
            assertions.Add(new Assertion
            {
                Passed = false,
                FailureMessage =
                    string.Format("an exception was thrown ({0}):\r\n{1}", ex.Message, ex.StackTrace)
            }); 
        }

        internal Assertion[] Assertions
        {
            get
            {
                return this.assertions.ToArray();
            }
        }

        public T ResultAs<T>()
        {
            return (T)Result;
        }

        /// <summary>
        /// The return value of the test method execution
        /// </summary>        
        public object Result { get; set; }

        bool testIsDone;
        internal Because TestIsDone
        {
            get
            {
                if (!testIsDone)
                {
                    testIsDone = true;
                    setupContext.Get<IBecauseWriter>().Write(this);                    
                }
                return this;
            }
        }

        internal Because CleanUp(Action<SetupContext> cleanup)
        {
            cleanup(setupContext);
            return this;
        }

        /// <summary>
        /// Throws an exception if the test failed.  Same as ThrowExceptionIfTheTestFailed. 
        /// </summary>
        /// <param name="message"></param>
        public void OrNot(string message = "TestFailed")
        {
            ThrowExceptionIfTheTestFailed(message);
        }

        /// <summary>
        /// Throws an exception if the test failed.  Same as OrNot.
        /// </summary>
        /// <param name="message"></param>
        public void ThrowExceptionIfTheTestFailed(string message = "Test Failed")
        {
            if (!Passed)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// Throws an exception if the test failed.  Same as ThrowExceptionIfTheTestFailed.
        /// </summary>
        public void UnlessItFailed(string message = "The test failed, please see above for more information.")
        {
            ThrowExceptionIfTheTestFailed(message);
        }
    }
}
