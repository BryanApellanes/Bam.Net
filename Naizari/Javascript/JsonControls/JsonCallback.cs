/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Naizari.Extensions;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using Naizari.Logging;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    [ToolboxData("<{0}:JsonCallback runat=\"server\">alert('Replace this with your javascript code');</{0}:JsonCallback>")]
    [ParseChildren(typeof(string), ChildrenAsProperties = true, DefaultProperty = "FunctionBody")]
    public class JsonCallback: JsonFunction
    {
        //JsonInvoker invoker;
        //string clientInstanceName;
        //string methodName;

        public JsonCallback():base()
        {
            this.AddParameter("jsonResult");
            this.ParameterAdded += new ParameterAddedDelegate(OnParameterAdded);
            this.ExecutionType = JavascriptExecutionTypes.Call;
            this.AddJsonFunction(this);
            this.ResponseFormat = JsonResponseFormats.json;

            //TODO: get rid of this stuff.  It's kind of a dumb idea
            this.PreInvokeFunctions = new JsonFunctionDelegate();
            this.PreInvokeFunctions.ExecutionType = JavascriptExecutionTypes.Call;
            //this.PostInvokeFunctions = new JsonFunctionDelegate();
            //this.PostInvokeFunctions.ExecutionType = JavascriptExecutionTypes.Call;

            this.PreInvokeFunctions.JsonId = this.JsonId + "_PreInvokeDelegate";
            //this.PostInvokeFunctions.JsonId = this.JsonId + "_PostInvokeDelegate";
            // -- end get rid of this stuff

            //this.AutoRegisterScript = true;
        }

        protected virtual void OnParameterAdded(JsonFunction function, string parameterName)
        {
            if (this.parameters.Count > 1)
                throw new InvalidCallbackFunctionException(this.JsonId);
        }

        public string ParameterName
        {
            get
            {
                return this.parameters[0];
            }
            set
            {
                this.parameters[0] = value;
            }
        }

        public JsonResponseFormats ResponseFormat
        {
            get;
            set;
        }

        /// <summary>
        /// A comma separated list of JsonFunctions JsonIds or regular script function names
        /// to be called before invoking the JsonMethod.
        /// </summary>
        internal string PreInvokeFunctionJsonIds
        {
            get
            {
                return this.PreInvokeFunctions.JsonFunctionJsonIds;
            }
            set
            {
                this.PreInvokeFunctions.JsonFunctionJsonIds = value;
            }
        }

        internal JsonFunctionDelegate PreInvokeFunctions
        {
            get;
            set;
        }

        // TODO: remove this and delete the JsonFunctionDelegate,  it's just a bad idea
        //public JsonFunctionDelegate PostInvokeFunctions
        //{
        //    get;
        //    set;
        //}

        public override void WireScriptsAndValidate()
        {
            this.CreateScript();
        }

        public JsonInvoker Invoker
        {
            get;
            internal set;
        }

        protected override void CreateScript()
        {
            if (this.scriptsCreated)
                return;

            base.CreateScript();



            //if (this.PostInvokeFunctions.Count > 0)
            //    this.AppendScriptBodyFormat("{0};", this.PostInvokeFunctions.JsonId);

            string clientPreInvoke = string.Empty;

            if (this.PreInvokeFunctions.Count > 0)
            {
                
                this.AddJsonFunction(this.PreInvokeFunctions);

                clientPreInvoke = string.Format(", '{0}'", this.PreInvokeFunctions.JsonId);
            }

            this.AddScriptTextLine(
                string.Format(
                    "JSUI.RegisterCallbackSubscriber('{0}', '{1}', '{2}', '{3}', JSUI.Functions['{0}']{4});",
                        this.JsonId,
                        this.Invoker.ClientInstanceName,
                        this.Invoker.JsonMethodProvider.GetType().Name.ToLowerInvariant(),
                        this.Invoker.MethodName,
                        clientPreInvoke));            
        }
 

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (Invoker == null && !this.DesignMode)
                throw new InvokerNotSetException(this);

            if (this.RenderScripts)
                base.Render(writer);
        }

        
    }
}
