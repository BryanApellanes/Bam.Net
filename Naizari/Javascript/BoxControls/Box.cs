/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using System.Web.UI;
using System.Web;
using System.IO;
using Naizari.Extensions;
using System.Reflection;

[assembly: TagPrefix("Naizari.Javascript.BoxControls", "box")]
namespace Naizari.Javascript.BoxControls
{
    public class Box: JsonControl
    {
        string virtualBoxPath;
        string defaultTemplateName;
        public Box():base()
        {
            this.defaultTemplateName = "Default" + this.GetType().Name + ".ascx"; 
            this.virtualBoxPath = BoxServer.BoxDataTemplateDirectory + defaultTemplateName;
            controlToRender.TagName = "div";
            this.PopulateOnPageLoad = true;
            this.AddRequiredScript("naizari.javascript.boxcontrols.box.js");
        }

        public override void WireScriptsAndValidate()
        {
            string boxFile = HttpContext.Current.Server.MapPath(this.virtualBoxPath);
            if (!File.Exists(boxFile))
            {
                Resources.Load(Assembly.GetExecutingAssembly());
                StringExtensions.SafeWriteFile(boxFile, string.Format(Resources.TextFilesByName["DefaultBoxTemplate_ascx.txt"], "Default" + this.GetType().Name));
                StringExtensions.SafeWriteFile(string.Format("{0}.cs", boxFile), Resources.TextFilesByName["DefaultBoxTemplate_ascx_cs.txt"].Replace("$$Name$$", this.GetType().Name));
            }

            // ensure that all box ascx files are unique
            Box[] allBoxes = this.ParentJavascriptPage.FindAllJsonControlsOfGenericType<Box>();
            if (allBoxes.Length > 1)
            {
                for (int i = 0; i < allBoxes.Length; i++)
                {
                    Box currentBox = allBoxes[i];
                    for (int ii = i + 1; ii <= allBoxes.Length - (i + 1); ii++)
                    {
                        Box nextBox = allBoxes[ii];
                        if (currentBox.VirtualBoxPath.Equals(nextBox.VirtualBoxPath))
                            throw new JsonInvalidOperationException(
                                string.Format("Multiple boxes found using the same ascx file: [jsonid: '{0}', aspid: '{1}'] and ['jsonid: '{2}', aspid: '{3}']", currentBox.JsonId, currentBox.ID, nextBox.JsonId, nextBox.ID)
                                );
                    }
                }
            }
        }

        public string VirtualBoxPath
        {
            get
            {
                return BoxServer.GetVirtualBoxPath(HttpContext.Current, BoxName);                
            }
            set
            {
                this.BoxName = BoxServer.GetBoxName(HttpContext.Current, value);
            }
        }

        public string BoxName
        {
            get;
            set;
        }

        bool populateOnPageLoad;
        /// <summary>
        /// If true, will cause this box to make an asynchronous request for its contents
        /// followed by an asynchronous request for the scripts required.  Setting this
        /// property will set the PopulateInline property to the opposite value.
        /// </summary>
        public bool PopulateOnPageLoad
        {
            get
            {
                return populateOnPageLoad;
            }
            set
            {
                populateOnPageLoad = value;
                populateInline = !value;
            }
        }

        bool populateInline;
        /// <summary>
        /// If true the content of the box will be populated inline with the page.
        /// Setting this property will set the PopulateOnPageLoad property to the
        /// opposite value.
        /// </summary>
        public bool PopulateInline
        {
            get
            {
                return populateInline;
            }
            set
            {
                populateInline = value;
                populateOnPageLoad = !value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            controlToRender.Attributes.Add("id", this.DomId);
            controlToRender.Attributes.Add("jsonid", this.JsonId);
            if (PopulateInline)
            {
                if (string.IsNullOrEmpty(this.VirtualBoxPath))
                    throw new JsonInvalidOperationException("The VirtualBoxPath property was not set or the BoxName specified could not be found.");

                controlToRender.Controls.Add(new LiteralControl(BoxServer.GetHtmlString(this.VirtualBoxPath)));
            }

            controlToRender.RenderControl(writer);

            if (PopulateOnPageLoad)
            {
                JsonFunction retrieveBoxContentScript = new JsonFunction();
                retrieveBoxContentScript.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
                retrieveBoxContentScript.FunctionBody = string.Format("DataBox.GetBoxContent('{0}', '{1}');", this.DomId, this.BoxName);
                retrieveBoxContentScript.RenderControl(writer);
            }           

        }
    }
}
