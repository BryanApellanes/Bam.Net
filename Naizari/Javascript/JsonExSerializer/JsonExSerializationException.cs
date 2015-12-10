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
using System.Runtime.Serialization;

namespace Naizari.Javascript.JsonExSerialization
{
    /// <summary>
    /// Base class for all serialization exceptions
    /// </summary>
    [Serializable]
    public class JsonExSerializationException : Exception
    {
        public JsonExSerializationException()
            : base()
        {
        }

        public JsonExSerializationException(string message)
            : base(message)
        {
        }

        public JsonExSerializationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected JsonExSerializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
