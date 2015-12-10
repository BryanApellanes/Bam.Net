/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    /// <summary>
    /// Intended to be used in identical fashion to the 
    /// System.InvalidOperationException.  This exception
    /// extends JsonException so debugging and troubleshooting
    /// will be easier.
    /// </summary>
    public class JsonInvalidOperationException: JsonException
    {
        public JsonInvalidOperationException() : base() { }
        public JsonInvalidOperationException(string message) : base(message) { }
        public JsonInvalidOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
