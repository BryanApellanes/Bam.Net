/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bam.Net
{
    /// <summary>
    /// A utility for making assertions
    /// </summary>
    public static class Expect
    {
        public static bool ShouldHtmlEncodeExceptions { get; set; }

		/// <summary>
		/// Asserts that the current boolean is true
		/// </summary>
		/// <param name="boolToCheck"></param>
        public static void IsTrue(this bool boolToCheck)
        {
            IsTrue(boolToCheck, "Expected <true>, Actual <false>");
        }
        
        public static void IsTrue(this bool boolToCheck, string message) 
        {
            if (!boolToCheck)
            {
                if (string.IsNullOrEmpty(message))
                    throw new ExpectFailedException(true, boolToCheck, ShouldHtmlEncodeExceptions);
                else
                    throw new ExpectFailedException(message);
            }
        }
        
        public static void FileExists(string filePath)
        {
            FileExists(filePath, "File not found.");
        }

        public static void FileExists(string filePath, string message)
        {
            Expect.IsTrue(File.Exists(filePath), message);
        }

        public static void IsFalse(bool boolToCheck)
        {
            IsFalse(boolToCheck, string.Empty);
        }

        public static void IsFalse(bool boolToCheck, string message)
        {
            if (boolToCheck)
            {
                if (string.IsNullOrEmpty(message))
                    throw new ExpectFailedException(false, boolToCheck, ShouldHtmlEncodeExceptions);
                else
                    throw new ExpectFailedException(message);
            }
        }

        /// <summary>
        /// Executes the specified actionThatThrowsException action passing the exception to the specified 
        /// catchDelegate and throws an ExpectFailedException if the actionThatThrowsException doesn't
        /// throw an Exception
        /// </summary>
        /// <param name="actionThatThrowsException"></param>
        /// <param name="catchDelegate"></param>
        /// <param name="message"></param>
        public static void Throws(Action actionThatThrowsException, string message = null)
        {
            Throws(actionThatThrowsException, null, message);
        }

        /// <summary>
        /// Executes the specified actionThatThrowsException action passing the exception to the specified 
        /// catchDelegate and throws an ExpectFailedException if the actionThatThrowsException doesn't
        /// throw an Exception
        /// </summary>
        /// <param name="actionThatThrowsException"></param>
        /// <param name="catchDelegate"></param>
        /// <param name="message"></param>
        public static void Throws(Action actionThatThrowsException, Action<Exception> catchDelegate = null, string message = null)
        {
            if (catchDelegate == null)
            {
                catchDelegate = (e) =>
                {
                    
                };
            }
            bool thrown = false;
            try
            {
                actionThatThrowsException();
            }
            catch (Exception ex)
            {
                catchDelegate(ex);
                thrown = true;
            }

            if (!thrown)
            {
                if (string.IsNullOrEmpty(message))
                {
                    message = "Exception was not thrown";
                }

                throw new ExpectFailedException(message);
            }
        }

        /// <summary>
        /// Checks if the specified "left" value is greater than the specified "right" value.
        /// </summary>
        /// <param name="left">int on the left of &gt;</param>
        /// <param name="right">int on the right of &gt;</param>
        public static void IsGreaterThan(int left, int right)
        {
            IsGreaterThan((long)left, (long)right);
        }

        /// <summary>
        /// Checks if the specified "left" value is greater than the specified "right" value.
        /// </summary>
        /// <param name="left">int on the left of &gt;</param>
        /// <param name="right">int on the right of &gt;</param>
        public static void IsGreaterThan(long left, long right)
        {
            IsGreaterThan(left, right, string.Format("{0} is not greater than {1}", left, right));
        }
        /// <summary>
        /// Checks if the specified "left" value is greater than the specified "right" value.
        /// </summary>
        /// <param name="left">int on the left of &gt;</param>
        /// <param name="right">int on the right of &gt;</param>
        public static void IsGreaterThan(long left, long right, string message)
        {
            if (!(left > right))
                throw new ExpectFailedException(message);

        }

        public static void IsGreaterThanOrEqualTo(int left, int right)
        {
            IsGreaterThanOrEqualTo(left, right, string.Format("{0} is not greater than or equal to {1}", left, right));
        }
        /// <summary>
        /// Checks if the specified "left" value is greater than or equal to the specified "right" value. 
        /// </summary>
        /// <param name="left">int on the left of &gt;=</param>
        /// <param name="right">int on the right of &gt;=</param>
        public static void IsGreaterThanOrEqualTo(int left, int right, string message)
        {
            if (!(left >= right))
                throw new ExpectFailedException(message);
        }

        /// <summary>
        /// Checks if the specified objects are the same using == (!=).
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AreSame(object expected, object actual)
        {
            AreSame(expected, actual, string.Empty);
        }
        
        /// <summary>
        /// Checks if the specified objects are the same using == (!=).
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AreSame(object expected, object actual, string message)
        {
            if (expected != actual)
            {
                if (!string.IsNullOrEmpty(message))
                    throw new ExpectFailedException(message, ShouldHtmlEncodeExceptions);

                throw new ExpectFailedException(expected.ToString(), actual.ToString(), ShouldHtmlEncodeExceptions);
            }
        }

        public static void ReferenceEquals(object one, object two)
        {
            ReferenceEquals(one, two, string.Empty);
        }

        public static void ReferenceEquals(object one, object two, string message)
        {
            if (!object.ReferenceEquals(one, two))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ExpectFailedException("References weren't equal");
                }

                throw new ExpectFailedException(message, ShouldHtmlEncodeExceptions);
            }
        }

        public static void AreEqual(int expected, int actual)
        {
            AreEqual((long)expected, (long)actual);
        }

        public static void AreEqual(long expected, long actual)
        {
            AreEqual(expected, actual, "");
        }

        public static void AreEqual(long expected, long actual, string message)
        {
            if (expected != actual)
            {
                if (string.IsNullOrEmpty(message))
                {
                    throw new ExpectFailedException(expected.ToString(), actual.ToString(), ShouldHtmlEncodeExceptions);
                }
                else
                {
                    throw new ExpectFailedException(message);
                }
            }
        }

        /// <summary>
        /// Does an equality comparison using expected.Equals()
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        public static void AreEqual(object expected, object actual)
        {
            AreEqual(expected, actual, "");
        }

        /// <summary>
        /// Does an equality comparison using expected.Equals()
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        public static void AreEqual(string expected, string actual)
        {
            AreEqual(expected, actual, "");
        }

        /// <summary>
        /// Does an equality comparison using expected.Equals()
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="failureMessage">The message to display if the comparison fails</param>
        public static void AreEqual(string expected, string actual, string failureMessage)
        {
            AreEqual((object)expected, (object)actual, failureMessage);
        }

        /// <summary>
        /// Checks if the specified objects are equal using the Equals() method.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        public static void AreEqual(object expected, object actual, string failureMessage)
        {
            if (((expected == null && actual != null) ||
                (actual == null && expected != null)) ||
                !expected.Equals(actual))
            {
                if (string.IsNullOrEmpty(failureMessage))
                {
                    string expectString = expected == null ? "null" : expected.ToString();
                    string actualString = actual == null ? "null" : actual.ToString();
                    throw new ExpectFailedException(expectString, actualString, ShouldHtmlEncodeExceptions);
                }
                else
                {
                    throw new ExpectFailedException(failureMessage);
                }
            }
        }
		
        /// <summary>
        /// Throws an ExpectFailedException if the type doesn't 
        /// derive from the specified generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        public static void DerivesFromType<T>(this object objectToCheck)
        {
            DerivesFromType<T>(objectToCheck, string.Empty);
        }

        public static void DerivesFromType<T>(this object objectToCheck, string failureMessage)
        {
            Type checkType = objectToCheck.GetType();
            if (!checkType.IsSubclassOf(typeof(T)))
            {
                if (string.IsNullOrEmpty(failureMessage))
                {
                    throw new ExpectFailedException(typeof(T), objectToCheck, ShouldHtmlEncodeExceptions);
                }
                else
                {
                    throw new ExpectFailedException(failureMessage);
                }
            }
        }

        /// <summary>
        /// Asserts that the current instance is of the specified generic type.
        /// Throws an excpetion if the assertion fails.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        public static void IsObjectOfType<T>(this object objectToCheck)
        {
            IsObjectOfType<T>(objectToCheck, string.Empty);
        }

        /// <summary>
        /// Checks if the specified object is of type T using GetType().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        public static void IsObjectOfType<T>(this object objectToCheck, string failureMessage)
        {
            if (objectToCheck.GetType() != typeof(T))
            {
                if (string.IsNullOrEmpty(failureMessage))
                    throw new ExpectFailedException(typeof(T), objectToCheck, ShouldHtmlEncodeExceptions);
                else
                    throw new ExpectFailedException(failureMessage);
            }
        }

        /// <summary>
        /// Asserts that the object is an instance of the specified generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        /// <param name="failureMessage"></param>
        public static void IsInstanceOfType<T>(this object objectToCheck, string failureMessage = "")
        {
            if (!typeof(T).IsInstanceOfType(objectToCheck))
            {
                if (string.IsNullOrWhiteSpace(failureMessage))
                {
                    throw new ExpectFailedException(typeof(T), objectToCheck, ShouldHtmlEncodeExceptions);
                }
                else
                {
                    throw new ExpectFailedException(failureMessage);
                }
            }
        }
        /// <summary>
        /// Asserts that the specified string is null or empty.  Throws
        /// an exception if the assertion fails.
        /// </summary>
        /// <param name="stringToCheck"></param>
        public static void IsNullOrEmpty(string stringToCheck)
        {
            IsNullOrEmpty(stringToCheck, string.Empty);
        }

        /// <summary>
        /// Asserts that the specified string is null or empty.  Throws
        /// an exception if the assertion fails.
        /// </summary>
        /// <param name="stringToCheck"></param>
        public static void IsNullOrEmpty(string stringToCheck, string failureMessage)
        {
            if (!string.IsNullOrEmpty(stringToCheck))
            {
                if (string.IsNullOrEmpty(failureMessage))
                    throw new ExpectFailedException("null or empty string", stringToCheck);
                else
                    throw new ExpectFailedException(failureMessage);
            }
        }

        public static void IsNotNullOrEmpty(string stringToCheck)
        {
            IsNotNullOrEmpty(stringToCheck, "");
        }

        public static void IsNotNullOrEmpty(string stringToCheck, string failureMessage)
        {
            if (string.IsNullOrEmpty(stringToCheck))
            {
                if (string.IsNullOrEmpty(failureMessage))
                    throw new ExpectFailedException("string with value", "null or empty string");
                else
                    throw new ExpectFailedException(failureMessage);
            }
        }

        /// <summary>
        /// Checks if the specified object extends type T using the "is" operator.  The same as Extends&lt;T&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        public static void IsExtenderOfType<T>(object objectToCheck)
        {
            Extends<T>(objectToCheck);
        }

        /// <summary>
        /// Checks if the specified object extends type T using the "is" operator.
        /// </summary>
        /// <typeparam name="T">The type to be extended.</typeparam>
        /// <param name="objectToCheck">The object to check if it extends the specified type T.</param>
        public static void Extends<T>(object objectToCheck)
        {
            if (!(objectToCheck is T))
                throw new ExpectFailedException(string.Format("{0} doesn't extend {1}", objectToCheck.GetType().Name, typeof(T).Name), ShouldHtmlEncodeExceptions);
        }

        public static void IsNull(object objectToCheck)
        {
            IsNull(objectToCheck, "");
        }
        
        /// <summary>
        /// Throws an exception if the specified objectToCheck is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToCheck"></param>
        /// <param name="failureMessage"></param>
        public static void IsNull(object objectToCheck, string failureMessage) 
        {
            if (objectToCheck != null)
            {
                if (!string.IsNullOrEmpty(failureMessage))
                    throw new ExpectFailedException(failureMessage);
                else
                    throw new ExpectFailedException("null", objectToCheck.GetType().Name, ShouldHtmlEncodeExceptions);
            }
        }

        public static void IsNotNull(object objectToCheck)
        {
            IsNotNull(objectToCheck, string.Empty);
        }

        public static void IsNotNull(object objectToCheck, string failureMessage) 
        {
            if (objectToCheck == null)
            {
                if (!string.IsNullOrEmpty(failureMessage))
                    throw new ExpectFailedException(failureMessage);
                else
                    throw new ExpectFailedException("object", "null", ShouldHtmlEncodeExceptions);
            }
        }    
        public static void Fail()
        {
            Fail("Expect.Fail() was called to throw this exception.");
        }

        public static void Fail(string message)
        {
            throw new ExpectFailedException(message, ShouldHtmlEncodeExceptions);
        }
    }
}
