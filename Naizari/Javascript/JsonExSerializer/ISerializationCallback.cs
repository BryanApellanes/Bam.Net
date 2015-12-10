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
    /// Interface for an object to receive events during serialization.  The OnBeforeSerialization
    /// will be called immediately before an object is serialized.  The OnAfterSerialization method
    /// will be called after the object has been serialized.
    /// </summary>
    public interface ISerializationCallback
    {
        /// <summary>
        /// Called before serialization of an object implementing the interface
        /// </summary>
        void OnBeforeSerialization();

        /// <summary>
        /// Called after serialization of the object
        /// </summary>
        void OnAfterSerialization();
    }
}
