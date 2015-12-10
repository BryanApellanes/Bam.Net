/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Naizari.Javascript.JsonControls
{
    public class JsonException:Exception
    {
        public JsonException() : base() { }

        public JsonException(string message) : base(message) { }

        protected JsonException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public JsonException(string message, Exception innerException) : base(message, innerException) { }
    }
}
