/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Javascript.JsonControls;
using System.Web.UI;

namespace Bam.Javascript.DataControls
{
    public class Draggable: DataControl<JQueryOptions>
    {

        public Draggable()
            : base()
        {
            
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.controlToRender.TagName = "div";
            
            this.controlToRender.Attributes.Add("id", this.DomId);
            this.controlToRender.Attributes.Add("jsonid", this.JsonId);

            this.controlToRender.RenderControl(writer);

            if (this.RenderScripts)
                this.RenderConglomerateScript(writer);
        }
    }
}
