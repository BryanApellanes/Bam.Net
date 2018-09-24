/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Bam.Net.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidIHasRequiredPropertiesImplementationException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidIHasRequiredPropertiesImplementationException"/> class.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="propertyName">Name of the property.</param>
        public InvalidIHasRequiredPropertiesImplementationException(Type t, string propertyName)
            : base("The Type " + t.Name + " is an invalid implementation of IHasRequiredProperties.  The RequiredProperties property reported a non existent property on the object (" + propertyName + ").")
        { }
    }
}
