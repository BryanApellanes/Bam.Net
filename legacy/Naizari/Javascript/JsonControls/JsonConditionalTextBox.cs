/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Naizari.Javascript.JsonControls
{
    public class JsonConditionalTextBox : JsonTextBox
    {
       
        public string Name { get; set; }
        public string NameCssClass { get; set; }
        public bool RenderName { get; set; }
        protected override void Render(HtmlTextWriter writer)
        {
            this.InputJsonId = this.JsonId;
            this.InputElementTagName = "input";
            if (this.RenderName)
            {
                HtmlGenericControl div = new HtmlGenericControl();
                div.TagName = "div";
                //div.Style.Add("display", "inline-block");

                div.Attributes.Add("class", this.NameCssClass);

                div.Controls.Add(new LiteralControl(this.Name));
                div.RenderControl(writer);
            }
            if (this.CanEdit)
                base.Render(writer);
            else
            {
                string displayValue = !string.IsNullOrEmpty(this.Value) ? this.Value : this.Text;
                this.controlToRender.TagName = "span";
                if (!string.IsNullOrEmpty(this.CssClass))
                    this.controlToRender.Attributes.Add("class", this.CssClass);
                this.controlToRender.Controls.Add(new LiteralControl(displayValue));
                this.controlToRender.RenderControl(writer);

                JsonHiddenInput hiddenInput = new JsonHiddenInput();
                hiddenInput.JsonId = this.JsonId;
                hiddenInput.Value = displayValue;
                hiddenInput.RenderControl(writer);
            }
        }
    }
}
