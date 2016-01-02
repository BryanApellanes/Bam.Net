/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Extensions;

namespace Naizari.Javascript.JsonControls
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class JsonEventSource: JsonFunction
    {
        public JsonEventSource() : base() 
        {
            this.AddJsonFunction(this);
            this.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
            this.DomId = StringExtensions.RandomString(8, false, false);
            this.ClientEventName = StringExtensions.RandomString(8, false, false);
        }

        /// <summary>
        /// The name of the client event to listen for on the current JsonEventSource instance.
        /// </summary>
        public string ClientEventName { get; set; }

        public override void WireScriptsAndValidate()
        {
            string eventParameters = "null";
            if (this.parameters.Count > 0)
                eventParameters = string.Format("[{0}]", this.Parameters);
            this.PrependScript("var " + this.DomId + " = {" + this.ClientEventName + ": []};");//string.Format("var {0} = new Object();", this.DomId));
            this.PrependScript(string.Format("JSUI.RegisterEventSource('{0}', {0});", this.DomId));
            string functionBody = string.Format("JSUI.FireEvent('{0}', '{1}', {2});", this.DomId, this.ClientEventName, eventParameters);
            this.FunctionBody = functionBody;
            base.WireScriptsAndValidate();
        }
    }
}
