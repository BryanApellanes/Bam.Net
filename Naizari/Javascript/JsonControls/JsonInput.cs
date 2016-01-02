/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Naizari.Extensions;
using Naizari.Javascript.BoxControls;

namespace Naizari.Javascript.JsonControls
{
    public abstract class JsonInput: JsonControl, IJsonInput, IJsonRightClickable, INamingContainer
    {
        protected JsonInputTypes type;

        public JsonInput()
            : base()
        {
            this.DomId = this.JsonId;
            //this.ID = this.JsonId;
            this.InputElementTagName = "input";
            InputJsonId = this.JsonId;
            this.AddRequiredScript(typeof(JsonInput));
        }

        public virtual bool CanEdit { get; set; }
        public virtual string InputJsonId { get; set; }
        public virtual string InputElementTagName { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            this.ApplyElementAttributes();

            controlToRender.TagName = "input";
            controlToRender.Attributes.Add("type", type.ToString().ToLower());

            controlToRender.Attributes.Add("value", this.Text);

            if (!string.IsNullOrEmpty(CssClass))
                controlToRender.Attributes.Add("class", CssClass);

            controlToRender.Attributes.Add("id", DomId);
            if (!string.IsNullOrEmpty(Value))
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

            //if (IsRightClickable)
            //    RightClickMenu.RenderControl(writer);
           
        }

        public override void WireScriptsAndValidate()
        {

            //CreateRegistrationScript(null);
        }

        /// <summary>
        /// Overrides the default implementation to ensure that the CreateRegistrationScript is called.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="includeScriptTags"></param>
        internal override void RenderConglomerateScript(HtmlTextWriter writer, bool includeScriptTags)
        {
            CreateRegistrationScript();
            base.RenderConglomerateScript(writer, includeScriptTags);
        }

        JsonFunction registrationScript;
        protected void CreateRegistrationScript()
        {
            if (registrationScript == null)
            {
                registrationScript = new JsonFunction(string.Format("JsonInput.RegisterAsSource('{0}', '{1}');", this.InputJsonId, this.InputElementTagName));
                this.AddJsonFunction(registrationScript);
            }
        }

        #region IJsonRightClickable Members
        JsonContextMenu rightClickMenu;
        public string RightClickMenuJsonId
        {
            get
            {
                if (IsRightClickable)
                    return RightClickMenu.JsonId;

                return string.Empty;
            }
            set
            {
                if (ParentJavascriptPage != null)
                {
                    ParentJavascriptPage.FindJsonControlAs<JsonContextMenu>(value);
                }
            }
        }
        public JsonContextMenu RightClickMenu
        {
            get
            {
                return rightClickMenu;
            }
            set
            {
                rightClickMenu = value;
            }
        }

        public bool IsRightClickable
        {
            get
            {
                return RightClickMenu != null;
            }
        }

        #endregion
    }
}
