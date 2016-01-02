/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI;

namespace Naizari.Javascript.JsonControls
{
    public class JsonInvokerLink: JsonInvoker
    {
        public JsonInvokerLink()
            : base()
        {
            this.ClientEventName = "click";
            
        }

        public override void WireScriptsAndValidate()
        {
            base.WireScriptsAndValidate();
            //if (!string.IsNullOrEmpty(this.PreInvokeJsonIds))
            //    this.WirePreInvokeFunctions();
        }
    }
}
