/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: TagPrefix("KLGates.Javascript.JsonControls", "json")]
namespace KLGates.Javascript.JsonControls
{

    /// <summary>
    /// This class may be a little overkill and is not currently used.
    /// </summary>
    public class ClientEventSubscription: JsonFunction 
    {
        JsonFunction function;
        public ClientEventSubscription()
            : base()
        {
        }

        public string EventSourceDomIds { get; set; }
        public string EventName { get; set; }//TODO: turn this into an enum of all valid client events

        public JsonFunction FunctionToExecute 
        {
            get
            {
                return function;
            }
            set
            {
                function = value;
            }
        }

        public override void WireScriptsAndValidate()
        {
            if (this.FunctionToExecute == null)
                ThrowInvalidOperationException("FunctionToExecute cannot be null.");

            this.FunctionToExecute.ExecutionType = JavascriptExecutionTypes.Call;

            if (string.IsNullOrEmpty(EventSourceDomIds))
                ThrowInvalidOperationException("EventSourceDomIds was not specified.");

            if (string.IsNullOrEmpty(EventName))
                ThrowInvalidOperationException("ClientEventName was not specified.");


            base.WireScriptsAndValidate();
            this.AddJsonFunction(FunctionToExecute);

            JsonFunction parseFunction = new JsonFunction();
            parseFunction.ExecutionType = JavascriptExecutionTypes.OnParse;
            parseFunction.FunctionBody = string.Format("JSUI.AddEventHandler('{0}', {1}, '{2}');", this.EventSourceDomIds, FunctionToExecute.JsonId, this.EventName);
            this.AddJsonFunction(parseFunction);
        }

        private void ThrowInvalidOperationException(string message)
        {
            throw new JsonInvalidOperationException(string.Format("{2} [jsonid: '{0}', aspid: '{1}']", this.JsonId, this.ID, message));
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //base.Render(writer);
            this.CreateScript();
            this.RenderConglomerateScript(writer);
        }
    }
}
