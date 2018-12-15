/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Naizari.Javascript.JsonControls
{
    [ToolboxData("<{0}:JsonUpdateLabel runat=\"server\">Your Text Here</{0}:JsonUpdateLabel")]
    [ParseChildren(true, "OptionHtml")]
    public class JsonDropDown: JsonInput
    {
        List<MenuItem> menuItems;
        public JsonDropDown()
            : base()
        {
            menuItems = new List<MenuItem>();
            this.InputElementTagName = "select";
            this.Text = "--Select An Option--";
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.WireScriptsAndValidate();
            base.OnPreRender(e);
        }

        public bool IncludeInvalidOption { get; set; }
        bool wired;
        public override void WireScriptsAndValidate()
        {
            if (!wired)
            {
                this.InputJsonId = this.JsonId;
                if (IncludeInvalidOption)
                {
                    MenuItem first = new MenuItem(this.Text, "-1");
                    first.Selected = true;
                    this.AddOption(first);
                }

                if (!string.IsNullOrEmpty(this.OptionHtml))
                    controlToRender.Controls.Add(new LiteralControl(this.OptionHtml));

                foreach (MenuItem item in this.menuItems)
                {
                    AddOption(item);
                }
                wired = true;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.WireScriptsAndValidate();
            this.ApplyElementAttributes();
            controlToRender.TagName = "select";
            controlToRender.ID = this.DomId;
            controlToRender.Attributes.Add("jsonid", this.JsonId);
            if (!string.IsNullOrEmpty(this.JsonProperty))
                controlToRender.Attributes.Add("jsonproperty", this.JsonProperty);

            if (!string.IsNullOrEmpty(this.CssClass))
                controlToRender.Attributes.Add("class", this.CssClass);

            this.Attributes.AddAttributes(this.controlToRender);
            this.Styles.AddStyles(this.controlToRender);
            controlToRender.RenderControl(writer);
            if (this.RenderScripts)
                this.RenderConglomerateScript(writer);
        }

        public string OptionHtml { get; set; }

        private void AddOption(MenuItem item)
        {
            HtmlGenericControl option = new HtmlGenericControl("option");
            option.Attributes.Add("value", item.Value);
            option.InnerHtml = item.Text;
            if (item.Selected)
            {
                option.Attributes.Add("selected", "selected");
                this.Text = item.Text;
                this.Value = item.Value;
            }

            controlToRender.Controls.Add(option);
        }

        public void AddItem(MenuItem item)
        {
            menuItems.Add(item);
        }

        public void RemoveItem(MenuItem item)
        {
            if (menuItems.Contains(item))
                menuItems.Remove(item);
        }
    }
}
