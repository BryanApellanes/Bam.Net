/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bam.Net.Testing
{
    /// <summary>
    /// Convenience entry point into a test that requires no setup through the SetupContext
    /// </summary>
    public static class When
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actionDescription"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public static TestContext<T> A<T>(string actionDescription, Func<T, object> test) where T : new()
        {
            return A<T>(actionDescription, new T(), test);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actionDescription"></param>
        /// <param name="objectUnderTest"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public static TestContext<T> A<T>(string actionDescription, T objectUnderTest, Func<T, object> test)
        {
            SetupContext setupContext = new SetupContext();
            setupContext.Set<T>(objectUnderTest);
            return new TestContext<T>(setupContext, SetupContext.GetActionDescription<T>(actionDescription), test);
        } 

        /// <summary>
        /// Prepares the test Context with an empty SetupContext instantiating the 
        /// object under test to an instance of T using the default constructor of type T.
        /// </summary>
        /// <typeparam name="T">The type of the </typeparam>
        /// <param name="actionDescription"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public static TestContext<T> A<T>(string actionDescription, Action<T> test) where T: new()
        {
            return A<T>(actionDescription, new T(), test);
        }
        /// <summary>
        /// Prepares the test Context with an empty SetupContext instantiating the 
        /// object under test to an instance of T using the default constructor of type T.
        /// </summary>
        /// <typeparam name="T">The type of the </typeparam>
        /// <param name="actionDescription"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public static TestContext<T> A<T>(string actionDescription, Action<T, SetupContext> test) where T : new()
        {
            return A<T>(actionDescription, new T(), test);
        }

        /// <summary>
        /// Prepares the test Context with an empty SetupContext setting the object under test
        /// to the specified objectUnderTest
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="actionDescription">A description of what the test action will do</param>
        /// <param name="objectUnderTest">The object instance being tested</param>
        /// <param name="test">the test delegate</param>
        /// <returns>Context</returns>
        public static TestContext<T> A<T>(string actionDescription, T objectUnderTest, Action<T> test)
        {
            SetupContext setupContext = new SetupContext();
            setupContext.Set<T>(objectUnderTest);
            return new TestContext<T>(setupContext, SetupContext.GetActionDescription<T>(actionDescription), test);
        }
        /// <summary>
        /// Prepares the test Context with an empty SetupContext setting the object under test
        /// to the specified objectUnderTest
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="actionDescription">A description of what the test action will do</param>
        /// <param name="objectUnderTest">The object instance being tested</param>
        /// <param name="test">the test delegate</param>
        /// <returns>Context</returns>
        public static TestContext<T> A<T>(string actionDescription, T objectUnderTest, Action<T, SetupContext> test)
        {
            SetupContext setupContext = new SetupContext();
            setupContext.Set<T>(objectUnderTest);
            return new TestContext<T>(setupContext, SetupContext.GetActionDescription<T>(actionDescription), test);
        }
    }
}
