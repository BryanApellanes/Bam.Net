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
    /// This class exists to ensure that the proxy for an ascx file is available when the ascx file is included
    /// in a page using the standard &lt;%@ Register ... /&gt; tags.  It is not necessary to use this
    /// class in any other situation.  All BoxUserControls use a JsonClientParentProxy internally and 
    /// will have their proxies registered/ensured if loaded as a "BoxResponse" (client side server method invocation
    /// with the response format set to "html" or "box".)
    /// </summary>
    public class JsonClientParentProxy: JsonFunction
    {
        JsonFunction ensure;
        public JsonClientParentProxy()
            : base()
        {
            this.ensure = new JsonFunction();
            this.ensure.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
            this.AddJsonFunction(this.ensure); 
            //this.AddJsonFunction(this);                       

            //this.ExecutionType = JavascriptExecutionTypes.Call;
        }

        public override JavascriptExecutionTypes ExecutionType
        {
            get
            {
                return this.ensure.ExecutionType;
            }
            set
            {
                //base.ExecutionType = value;
                this.ensure.ExecutionType = value;
            }
        }

        public override void WireScriptsAndValidate()
        {
            base.WireScriptsAndValidate();
            JavascriptServer.RegisterProvider(this.Parent);
            this.ensure.AppendScriptBodyFormat("JSUI.EnsureProxy('{0}', '{1}', \"window.{0} = ProxyUtil.GetProxy('{0}', true);\");", JavascriptServer.GetDefaultVarName(this.Parent.GetType()), JavascriptServer.GetClientTypeName(this.Parent.GetType()));//this.Parent.GetType().Name.ToLowerInvariant(), this.JsonId);
    
        }

        public string VarName { get; set; }

       
    }
}
