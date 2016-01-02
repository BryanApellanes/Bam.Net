/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Naizari.Javascript;
using System.Web.UI;
using System.Reflection;
using Naizari.Extensions;
using System.Web.UI.HtmlControls;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    public abstract class JsonInputToggler : JsonInput, INamingContainer
    {
        protected JsonLabel valueLabel;
        protected JsonInput inputControl;

        public JsonInputToggler()
            : base()
        {
            valueLabel = new JsonLabel();
            //this.AddRequiredScript("JsonInputToggler.js");
            this.CanEdit = true;
            this.InputJsonId = this.JsonId + "_input";
            this.AddRequiredScript(typeof(JsonInputToggler));//"naizari.javascript.jsoncontrols.jsoninputtoggler.js");
        }

        private const string ClientMethod = "JsonInputToggler.TogglePair";

        protected override void Render(HtmlTextWriter writer)
        {
            inputControl.DomId = this.InputJsonId;
            inputControl.ID = this.InputJsonId;
            inputControl.JsonId = this.InputJsonId;
            inputControl.Text = this.Text;

            controlToRender.TagName = "span";
            controlToRender.Attributes.Add("id", this.JsonId);
            controlToRender.Attributes.Add("jsonid", this.JsonId);
            if (!string.IsNullOrEmpty(this.CssClass))
                controlToRender.Attributes.Add("class", this.CssClass);
            styles.AddStyles(controlToRender);

            valueLabel.Styles["display"] = "inline";
            valueLabel.Styles["width"] = this.Styles["width"];

            
           
            valueLabel.Text = this.Text;

            controlToRender.Controls.Add(valueLabel);  

            if (this.CanEdit)
            {
                valueLabel.Attributes.SetAttribute("onclick", string.Format("{0}('" + valueLabel.JsonId + "', '" + inputControl.JsonId + "', false);", ClientMethod));
                inputControl.Attributes.SetAttribute("onblur", string.Format("{0}('" + inputControl.JsonId + "', '" + valueLabel.JsonId + "', true);", ClientMethod));
                inputControl.Styles.SetStyle("display", "none");
                
                inputControl.Styles["width"] = this.Styles["width"];
                inputControl.Styles["height"] = this.Styles["height"];

                controlToRender.Controls.Add(inputControl);
                controlToRender.RenderControl(writer);
            }
            else
            {
                controlToRender.RenderControl(writer);
            }

            CreateRegistrationScript();
            if (this.renderScripts)
                this.RenderConglomerateScript(writer);
        }        

        public string LabelCssClass
        {
            get
            {
                return valueLabel.CssClass;
            }
            set
            {
                valueLabel.CssClass = value;
            }
        }

        public string InputCssClass
        {
            get
            {
                return inputControl.CssClass;
            }
            set
            {
                inputControl.CssClass = value;
            }
        }

        //public bool CanEdit { get; set; }

        /// <summary>
        /// Gets or sets the current value of the control.
        /// </summary>
        public virtual string Value
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }
    }
}
