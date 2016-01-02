/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Naizari.Javascript.JsonControls
{
    public class JsonSessionTimeOut: JsonFunction
    {
        public JsonSessionTimeOut()
            : base()
        {
            this.AddJsonFunction(this);
            this.AutoRegisterScript = true;
            this.JsonId = "SessionTimeout";
            this.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
        }

        public JsonSessionTimeOut(int clientSessionTimeout)
            : this()
        {
            this.ClientSessionTimeout = clientSessionTimeout;
        }

        //public string ClientProxyName { get; internal set; }

        public int ClientSessionTimeout { get; set; }

        public string ExpireMessage { get; set; }

        protected override void CreateScript()
        {
            if (this.scriptsCreated)
                return;

            string functionBody = string.Format("Session.expireSessionInMinutes('{0}');", ClientSessionTimeout);
            if (!string.IsNullOrEmpty(ExpireMessage))
                functionBody += string.Format("\r\nSession.TimeoutMessage = '{0}';", this.ExpireMessage);

            //functionBody += string.Format("\r\nSession.Abandon = function(){0}{1}{2}", "{", this.ClientProxyName, ".ExpireSession();}");
            this.FunctionBody = functionBody;
            
            base.CreateScript();            
        }
    }
}
