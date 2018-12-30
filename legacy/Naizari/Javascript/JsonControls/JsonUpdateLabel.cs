/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Naizari.Javascript.JsonControls
{
    [ToolboxData("<{0}:JsonUpdateLabel runat=\"server\">Your Text Here</{0}:JsonUpdateLabel")]
    [ParseChildren(true, "Text")]
    public class JsonUpdateLabel: JsonInputToggler
    {
        public JsonUpdateLabel()
            : base()
        {
            this.inputControl = new JsonTextBox();
        }
    }
}
