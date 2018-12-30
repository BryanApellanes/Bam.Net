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
    /// Convenience entry point into creating and initializing the 
    /// SetupContext for a test.
    /// </summary>
    public static class After
    {
        /// <summary>
        /// Instantiates the SetupContext for a test and passes that
        /// instance to the specified setup Action delegate.
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
        public static SetupContext Setup(Action<SetupContext> setup)
        {
            SetupContext setupInstance = new SetupContext();
            setup(setupInstance);
            return setupInstance;
        }
    }
}
