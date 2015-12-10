/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class JsonScriptObjectEventSource: JsonEventSource
    {
        public JsonScriptObjectEventSource()
            : base()
        {
            this.ExecutionType = JavascriptExecutionTypes.Call;
        }
    }
}
