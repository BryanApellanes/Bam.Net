/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    public class JsonInvokerButton: JsonInvoker
    {
        public JsonInvokerButton()
            : base()
        {
            this.type = JsonInvokerTypes.input;
            this.ClientEventName = "click";
            this.Styles.SetStyle("height", "20px");
            this.Styles.SetStyle("width", "100px");
        }
    }
}
