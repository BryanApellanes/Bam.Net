/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net;
using Bam.Net.Incubation;

namespace Bam.Net.Testing
{
    /// <summary>
    /// Represents the setup or initialization context used for a test.
    /// Different from the Context.
    /// </summary>
    /// <remarks>
    /// Though it isn't required that the SetupContext be used for test 
    /// initialization it is provided as an option.
    /// </remarks>
    public class SetupContext: Incubator
    {
        Dictionary<Type, object> dictionary;
        internal SetupContext()
        {
            this.dictionary = new Dictionary<Type, object>();
            this.Set<IBecauseWriter>(new ConsoleBecauseWriter());
        }
                      
        /// <summary>
        /// Prepares the test context for object under test of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object under test</typeparam>
        /// <param name="actionDescription">A description of what action is being tested</param>
        /// <param name="test">The Action to run, the object under test will be provided as a 
        /// parameter so the developer can interact with it.</param>
        /// <returns></returns>
        public TestContext<T> WhenA<T>(string actionDescription, Action<T> test)
        {
            return new TestContext<T>(this, GetActionDescription<T>(actionDescription), test);
        }

        public TestContext<T> WhenA<T>(string actionDescription, Action<T, SetupContext> test)
        {
            return new TestContext<T>(this, GetActionDescription<T>(actionDescription), test);
        }

        /// <summary>
        /// Prepares the test context for object under test of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object under test</typeparam>
        /// <param name="actionDescription">A description of what action is being tested</param>
        /// <param name="test">The Func to run, the object under test will be provided as a 
        /// parameter so the developer can interact with it. Must return a value.</param>
        /// <returns></returns>
        public TestContext<T> WhenA<T>(string actionDescription, Func<T, object> test)
        {
            return new TestContext<T>(this, GetActionDescription<T>(actionDescription), test);
        }
        
        internal object ObjectUnderTest
        {
            get;
            set;
        }
                
        internal static string GetActionDescription<T>(string actionDescription)
        {
            return string.Format("Testing when a {0} {1}", typeof(T).Name, actionDescription);
        }
    }
}
