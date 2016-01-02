/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class JsonWindowResizeEventSource: JsonEventSource
    {
        public JsonWindowResizeEventSource()
            : base()
        {
            this.ExecutionType = JavascriptExecutionTypes.OnWindowResize;
        }
    }
}
