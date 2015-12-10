/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using System.Runtime.Serialization;

namespace Naizari.Javascript.BoxControls
{
    public class BoxException: JsonException
    {
         public BoxException() : base() { }

        public BoxException(string message) : base(message) { }

        protected BoxException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public BoxException(string message, Exception innerException) : base(message, innerException) { }
    }
}
