/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Naizari.Data.Common;
using Naizari.Helpers.Web;
using Naizari.Javascript.BoxControls;
using System.IO;
using System.Web;
using Naizari.Configuration;

namespace Naizari.Javascript.DataControls
{
    [JsonProxy("contextMenu")]
    public class ContextMenu: DataControl
    {
        public ContextMenu()
            : base()
        {
            this.AutoRegisterScript = true;
        }

        public override void WireScriptsAndValidate()
        {
            this.controlToRender.TagName = "div";
            this.controlToRender.Attributes.Add("class", "contextMenu");
            this.controlToRender.Style.Add("display", "none");
            this.controlToRender.Attributes.Add("jsonid", this.jsonId);
            this.controlToRender.Attributes.Add("id", this.DomId);

            this.controlToRender.Controls.Add(new LiteralControl("This control is not complete.  It still needs client side wiring and the jQuery contextMenu plugin isn't quite approprate here."));
            
            WriteDefaultTemplateToDisk(typeof(ContextMenuItem));

            foreach (Node file in this.BackingDoodad.Files)
            {
                ContextMenuItem item = ContextMenuItem.FromNode(file);
                this.controlToRender.Controls.Add(new LiteralControl(BoxServer.GetTemplatedString(item, this.TemplateName)));
            }
        }

        public ContextMenuItem[] Options
        {
            get
            {
                List<ContextMenuItem> retVals = new List<ContextMenuItem>();
                foreach (Node file in this.BackingDoodad.Files)
                {
                    retVals.Add(SerializationUtil.FromBinaryBytes<ContextMenuItem>(file.Data));
                }

                return retVals.ToArray();
            }
        }

        [JsonMethod]
        public void AddOption(string text, string action)
        {
            ContextMenuItem item = new ContextMenuItem();
            item.Action = action;
            item.Text = text;
            this.BackingDoodad.AddFile(text, item);
        }

        [JsonMethod(Verbs.POST)]
        public void RemoveOption(long id)
        {
            this.BackingDoodad.DeleteNode(id);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            this.controlToRender.RenderControl(writer);
            if (this.RenderScripts)
                this.RenderConglomerateScript(writer);
        }
    }
}
