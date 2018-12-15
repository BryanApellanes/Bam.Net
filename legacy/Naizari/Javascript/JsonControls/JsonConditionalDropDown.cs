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
    public class JsonConditionalDropDown : JsonDropDown
    {
        public JsonConditionalDropDown()
            : base()
        {
            this.IncludeInvalidOption = false;
        }

        public string Name { get; set; }
        public string NameCssClass { get; set; }
        public string ValueCssClass { get; set; }
        public bool RenderName { get; set; }
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.RenderName)
            {
                controlToRender.Attributes.Add("class", this.CssClass);
                HtmlGenericControl div = new HtmlGenericControl();
                div.TagName = "div";
                div.Style.Add("display", "inline-block");

                div.Attributes.Add("class", this.NameCssClass);

                div.Controls.Add(new LiteralControl(Name));
                div.RenderControl(writer);
            }
            if (this.CanEdit)
                base.Render(writer);
            else
            {
                this.InputElementTagName = "input";
                JsonHiddenInput hiddenInput = new JsonHiddenInput();
                hiddenInput.JsonId = this.JsonId;
                hiddenInput.Value = this.Value;
                hiddenInput.RenderControl(writer);

                this.controlToRender.Controls.Clear();
                this.controlToRender.TagName = "span";
                this.controlToRender.Attributes.Add("class", this.CssClass);
                this.controlToRender.Controls.Add(new LiteralControl(this.Text));
                this.controlToRender.RenderControl(writer);

            }
        }
    }
}
