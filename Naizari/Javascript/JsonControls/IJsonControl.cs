/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Naizari.Javascript.JsonControls
{
    public interface IJsonControl
    {
        string JsonId { get; set; }
        string DomId { get; set; }
        string Text { get; set; }
        JsonFunction[] Scripts { get; }
        bool RenderScripts { get; }
    }
}
