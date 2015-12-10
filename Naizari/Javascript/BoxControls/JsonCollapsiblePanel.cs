/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Javascript.JsonControls;
using Bam.Helpers.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bam.Helpers;

namespace Bam.Javascript.BoxControls
{
    [Obsolete("Bam DotNet: This class is kinda dumb, not sure what I was thinking.  This kind of functionality is better implemented as a primarily 'client' side control with basic markup provided by the server.")]
    public class JsonCollapsiblePanel: JsonControl
    {
        JsonImageSequence togglerImage;
        JsonFunction togglerScript;
        JsonFunction setupScript;

        public JsonCollapsiblePanel()
            : base()
        {
            this.togglerScript = new JsonFunction();
            this.togglerScript.JsonId = this.JsonId + "_toggle";
            this.setupScript = new JsonFunction();
            this.setupScript.JsonId = this.JsonId + "_setup";
            this.EnableViewState = true;
            this.Width = -1;
        }

        string collapsedImageUrl;
        public string CollapsedImageUrl
        {
            get { return this.collapsedImageUrl; }
            set
            {
                this.collapsedImageUrl = value;
                
            }
        }
        public string ExpandedImageUrl { get; set; }

        public bool Expanded 
        {
            get
            {
                if (this.ViewState[this.JsonId] != null)
                    return (bool)this.ViewState[this.JsonId];

                return false;
            }
            set
            {
                this.ViewState[this.JsonId] = value;
            }
        }

        //public bool ShowContent { get; set; }

        public bool UseImage { get { return !string.IsNullOrEmpty(this.CollapsedImageUrl) && !string.IsNullOrEmpty(this.ExpandedImageUrl); } }

        public int Height { get; set; }
        public int Width { get; set; }
        /// <summary>
        /// The text to display in the header of the collapsible panel.  The same as the Text property.
        /// </summary>
        public string HeaderText
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        JsonStyles headerStyles;
        public JsonStyles HeaderStyles
        {
            get
            {
                if (headerStyles == null)
                    headerStyles = new JsonStyles();

                return headerStyles;
            }
        }

        public string HeaderStyle
        {
            get
            {
                return this.HeaderStyles.ToString();
            }
            set
            {
                this.headerStyles = JsonStyles.FromString(value);
            }
        }

        public string HeaderCssClass { get; set; }
        public string ContentDomId { get; set; }

        protected override void OnInit(EventArgs e)
        {
            if (this.UseImage)
            {
                this.togglerImage = new JsonImageSequence(this.CollapsedImageUrl);
                this.togglerImage.AddImage(this.JsonId + "_expandImg", this.ExpandedImageUrl);
                this.togglerImage.ClientClickFunction = this.togglerScript.JsonId;
                
                this.ParentJavascriptPage.AddJsonControl(this.togglerImage);

            }
            base.OnInit(e);
        }
       
        public override void WireScriptsAndValidate()
        {
            if (string.IsNullOrEmpty(this.ContentDomId))
                throw ExceptionHelper.CreateException<JsonInvalidOperationException>("ContentDomId property not set: {0}", this.ToString());

            //string startAction = "GrowDown";
            //if(!Expanded)
            //    startAction = "ShrinkUp";


            string setupScriptBody = string.Empty;//string.Format("JSUI.SetElementAsLastChild('{0}', '{1}');\r\n", this.DomId, this.ContentDomId);
            setupScriptBody += string.Format("var {0} = new Animation.SizerClass('{1}', {2}, {3});", this.JsonId, this.ContentDomId, new JsonDimensions(this.Width, this.Height).ToString(), "true");//this.ShowContent ? "true" : "false");

            if (Expanded)
                setupScriptBody += string.Format("{0}.GrowDown();", this.JsonId);
            else
                setupScriptBody += string.Format("JSUI.HideElement('{0}');", this.ContentDomId);
            
            setupScriptBody += string.Format("JSUI.SetHandCursor('{0}');", this.DomId + "_head");
            setupScriptBody += string.Format("JSUI.AddEventHandler('{0}', {1}, 'click');", this.DomId + "_head", this.togglerScript.JsonId);
            setupScript.FunctionBody = setupScriptBody;
            setupScript.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;

            this.AddJsonFunction(setupScript);

            togglerScript.AddParameter("evt");
            string togglerScriptBody = string.Format("if(JSUI.GetElement('{0}').style.display == 'none')", this.ContentDomId);
            
            togglerScriptBody += string.Format("{0}\r\nAnimation.SizerClassInstances['{1}'].GrowDown('{1}');\r\n{2}else{0}", "{", this.ContentDomId, "}");
            togglerScriptBody += string.Format("\r\nAnimation.SizerClassInstances['{0}'].ShrinkUp('{0}');\r\n{1}", this.ContentDomId, "}");

            if (UseImage)
                togglerScriptBody += string.Format("if(JSUI.GetEventSourceElement(evt).tagName == 'DIV'){0}\r\nJSUI.ImageSequenceClassInstances['{1}'].ShowNext();{2}", "{", this.togglerImage.DomId, "}");
            

            togglerScript.FunctionBody = togglerScriptBody;
            togglerScript.ExecutionType = JavascriptExecutionTypes.Call;

            this.AddJsonFunction(this.togglerScript);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.renderScripts)
                this.RenderConglomerateScript(writer);

            this.Styles.SetStyle("height", "auto");
            this.Styles.SetStyle("display", "block");
            base.Render(writer);
            controlToRender.TagName = "div";
            controlToRender.Attributes.Add("id", this.DomId);
            controlToRender.Attributes.Add("jsonid", this.JsonId);

            HtmlGenericControl header = ControlHelper.CreateHtmlDiv(this.DomId + "_head");
            header.Controls.Add(new LiteralControl(this.HeaderText));
            
            if(this.headerStyles != null)
                this.headerStyles.AddStyles(header);
            if (this.Width != -1)
                header.Style.Add("width", this.Width + "px");
             
            if (!string.IsNullOrEmpty(this.HeaderCssClass))
                header.Attributes.Add("class", this.HeaderCssClass);

            if (this.togglerImage != null)
                header.Controls.Add(this.togglerImage);

            controlToRender.Controls.Add(header);

            controlToRender.RenderControl(writer);
          
        }
    }
}
