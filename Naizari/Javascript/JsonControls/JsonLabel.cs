/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Extensions;
using System.Web.UI;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    public class JsonLabel: JsonControl
    {
        public JsonLabel()
            : base()
        {
            this.Text = "JsonLabel Text";
            this.DomId = this.JsonId;
            this.controlToRender.TagName = "div";
        }

        public JsonLabel(string text)
            : this()
        {
            this.Text = text;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            controlToRender.Attributes.Add("jsonid", JsonId);
            if (!string.IsNullOrEmpty(CssClass))
                controlToRender.Attributes.Add("class", CssClass);
            
            controlToRender.Attributes.Add("id", DomId);
            attributes.AddAttributes(controlToRender);
            styles.AddStyles(controlToRender);
            controlToRender.Controls.Add(new LiteralControl(this.Text));
            controlToRender.RenderControl(writer);
        }

    }
}
