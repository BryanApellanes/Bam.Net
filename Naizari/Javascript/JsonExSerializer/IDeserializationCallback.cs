/*
	Copyright © Bryan Apellanes 2015  
*/
/*
 * Copyright (c) 2007, Ted Elliott
 * Code licensed under the New BSD License:
 * http://code.google.com/p/jsonexserializer/wiki/License
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript.JsonExSerialization
{
    /// <summary>
    /// An interface to control deserialization.  The OnAfterDeserialization method
    /// is called after an object has been deserialized.  All properties will be set before
    /// the method is called.
    /// </summary>
    public interface IDeserializationCallback
    {
        /// <summary>
        /// Called after an object has been deserialized
        /// </summary>
        void OnAfterDeserialization();
    }
}
