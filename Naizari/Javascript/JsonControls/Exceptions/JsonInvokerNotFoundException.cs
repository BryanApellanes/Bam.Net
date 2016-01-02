/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class JsonInvokerNotFoundException: JsonException
    {
        public JsonInvokerNotFoundException(string invokerJsonId)
            : base(string.Format("The JsonInvoker with JsonId {0} was not found.", invokerJsonId))
        { }
        
    }
}
