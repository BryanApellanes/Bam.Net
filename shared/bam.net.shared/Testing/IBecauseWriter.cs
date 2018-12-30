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
    /// Defines the method used to write the results of a test.
    /// </summary>
    public interface IBecauseWriter
    {
        /// <summary>
        /// When implemented by a derived class should
        /// write the results of a test contained in 
        /// the specified Because instance.
        /// </summary>
        /// <param name="because">The Because object containing test details including results.</param>
        void Write(Because because);
    }
}
