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
    /// Represents an assertion made during a test.
    /// </summary>
    public class Assertion
    {
        /// <summary>
        /// Gets or sets the partial message to display if the assertion passes.
        /// </summary>
        public string SuccessMessage { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating whether the assertion
        /// passed.
        /// </summary>
        public bool Passed { get; set; }
        
        string failureMessage;
        /// <summary>
        /// Gets or sets the partial failure message to display if the assertion fails.
        /// </summary>
        public string FailureMessage
        {
            get
            {
                if(string.IsNullOrWhiteSpace(failureMessage))
                {
                    return string.Format("{0} failed", SuccessMessage);
                }

                return failureMessage;
            }
            set
            {
                failureMessage = value;
            }
        }
    }
}
