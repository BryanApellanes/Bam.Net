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
using System.Reflection;
using Naizari.Test;

namespace Naizari.Javascript.JsonControls
{
    [ToolboxData("<{0}:JsonDropDownLabel runat=\"server\">Your Text Here</{0}:JsonUpdateLabel>")]
    [ParseChildren(true, "OptionHtml")]
    public class JsonDropDownLabel : JsonInputToggler
    {
        public JsonDropDownLabel()
            : base()
        {
            this.inputControl = new JsonDropDown();
            this.Items = new object[] { };
            this.InputElementTagName = "select";
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.WireScriptsAndValidate();
            base.OnPreRender(e);
        }
        bool wired;
        public override void WireScriptsAndValidate()
        {

            if (this.Items.Length > 0 && !wired)
            {
                string errorMsg = @"The specified property was not found, 
ensure that all objects in the Items array are of the same type and that 
the TextProperty and ValueProperty are set to the name of a property 
of the object type in the array";

                Type objType = this.Items[0].GetType();
                PropertyInfo textProperty = objType.GetProperty(this.TextProperty);
                Expect.IsNotNull(textProperty, errorMsg);
                PropertyInfo valueProperty = objType.GetProperty(this.ValueProperty);
                Expect.IsNotNull(valueProperty, errorMsg);

                JsonDropDown input = (JsonDropDown)this.inputControl;
                foreach (object item in this.Items)
                {
                    string text = textProperty.GetValue(item, null).ToString();
                    string value = valueProperty.GetValue(item, null).ToString();
                    MenuItem menuItem = new MenuItem(text, value);
                    input.AddItem(menuItem);
                }

                wired = true;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            ((JsonDropDown)this.inputControl).WireScriptsAndValidate();
            base.Render(writer);
        }

        public string OptionHtml
        {
            get { return ((JsonDropDown)this.inputControl).OptionHtml; }
            set { ((JsonDropDown)this.inputControl).OptionHtml = value; }
        }

        public object[] Items { get; set; }
        public string TextProperty { get; set; }
        public string ValueProperty { get; set; }
    }
}
