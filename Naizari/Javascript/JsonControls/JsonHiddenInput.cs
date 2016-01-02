/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class JsonHiddenInput: JsonInput
    {
        public JsonHiddenInput()
            : base()
        {
            this.type = JsonInputTypes.Hidden;
        }

        //protected override void Render(System.Web.UI.HtmlTextWriter writer)
        //{
        //    base.Render(writer);
        //    this.CreateRegistrationScript();

        //}
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
