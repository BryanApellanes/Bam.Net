/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class InvalidCallbackFunctionException: JsonException
    {
        public InvalidCallbackFunctionException(string functionJsonId)
            : base(string.Format("The JsonCallback with JsonId '{0}' is invalid, a json callback must take exactly 1 parameter.", functionJsonId))
        { }
    }
}
