/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Naizari.Extensions;

namespace Naizari.Javascript.JsonControls
{
    public class JsonCheckBox: JsonInput
    {
        public JsonCheckBox()
            : base()
        {
            this.type = JsonInputTypes.CheckBox;
        }

        bool check;
        public bool Checked
        {
            get
            {
                return this.check;
            }
            set
            {
                this.check = value;
            }
            
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.check)
                this.Attributes.SetAttribute("checked", "checked");

            //if (!string.IsNullOrEmpty(this.JsonProperty))
            //    this.Attributes.SetAttribute("jsonproperty", this.JsonProperty);

            base.Render(writer);
        }

        public string CssClass { get; set; }

        public override string JsonId
        {
            get
            {
                return base.JsonId;
            }
            set
            {
                base.JsonId = value;
            }
        }

        public override string InputJsonId
        {
            get
            {
                return base.JsonId;
            }
            set
            {
                base.JsonId = value;
            }
        }


    }
}
