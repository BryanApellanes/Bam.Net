/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using Naizari.Extensions;
using System.ComponentModel;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    [ToolboxData("<{0}:JsonTextBox JsonId='JsonId' runat=\"server\"></{0}:JsonTextBox>")]
    public class JsonTextBox: JsonInput
    {
        public JsonTextBox()
            : base()
        {
            this.type = JsonInputTypes.Text;
            this.Rows = -1;
            this.Columns = -1;
        }

       
        public string CssClass { get; set; }

        public override void WireScriptsAndValidate()
        {
            this.DomId = this.JsonId;
            this.InputJsonId = this.JsonId;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.CssClass))
                this.controlToRender.Attributes.Add("class", this.CssClass);
            if (!string.IsNullOrEmpty(this.CssStyle))
                this.controlToRender.Attributes.Add("style", this.CssStyle);
            //if (!string.IsNullOrEmpty(this.JsonProperty))
            //    this.controlToRender.Attributes.Add("jsonproperty", this.JsonProperty);
            if (this.TextMode != TextBoxMode.MultiLine)
                base.Render(writer);
            else
            {
                this.ApplyElementAttributes();

                controlToRender.TagName = "textarea";
                if (this.Columns != -1)
                    controlToRender.Attributes.Add("cols", this.Columns.ToString());
                if (this.Rows != -1)
                    controlToRender.Attributes.Add("rows", this.Rows.ToString());

                controlToRender.Attributes.Add("value", this.Text);

                if (!string.IsNullOrEmpty(CssClass))
                    controlToRender.Attributes.Add("class", CssClass);

                controlToRender.Attributes.Add("id", DomId);
                //if (!string.IsNullOrEmpty(Value))
                controlToRender.Attributes.Add("value", Value);

                if (!string.IsNullOrEmpty(this.JsonProperty))
                    controlToRender.Attributes.Add("jsonproperty", this.JsonProperty);

                controlToRender.Attributes.Add("jsonid", this.JsonId);

                styles.AddStyles(controlToRender);
                attributes.AddAttributes(controlToRender);

                controlToRender.RenderControl(writer);

                CreateRegistrationScript();

                if (this.renderScripts)
                    this.RenderConglomerateScript(writer);
            }
        }

        public override string JsonId
        {
            get
            {
                return base.JsonId;
            }
            set
            {
                base.JsonId = value;
                this.InputJsonId = value;
            }
        }

        public TextBoxMode TextMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of rows.  Only valid in TextMode.Multiline.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Gets or sets the number of columns.  Only valid in TextMode.Multiline.
        /// </summary>
        public int Columns { get; set; }
    }
}
