/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Html;
using System.Web.Mvc;
using System.IO;

namespace Bam.Net.Server.Renderers
{
    /// <summary>
    /// The renderer used to render a template for 
    /// a given object.  In other words writes
    /// the default template for an object.
    /// </summary>
    public class CommonTemplateRenderer: Renderer
    {
		public const string ViewFolderName = "views";
        public CommonTemplateRenderer(ContentResponder content)
            : base("text/html", ".dust")
        {
            this.ContentResponder = content;
        }

        public ContentResponder ContentResponder
        {
            get;
            set;
        }

        object _renderLock = new object();
        public override void Render(object toRender)
        {
            base.Render(toRender);
            DirectoryInfo dustDir = GetViewRoot();
            if (!dustDir.Exists)
            {
                dustDir.Create();
            }

            lock (_renderLock)
            {
                string fileName = "{0}.dust"._Format(toRender.GetType().Name);
                string dustFilePath = Path.Combine(dustDir.FullName, fileName);
                if (!File.Exists(dustFilePath))
                {
                    using (FileStream fs = File.Create(dustFilePath, (int)OutputStream.Length))
                    {
                        OutputStream.Seek(0, SeekOrigin.Begin);
                        OutputStream.CopyTo(fs);
                    }
                }
            }
        }

        protected virtual DirectoryInfo GetViewRoot()
        {
            DirectoryInfo dustDir = new DirectoryInfo(Path.Combine(ContentResponder.Root, "common", ViewFolderName));
            return dustDir;
        }

        /// <summary>
        /// Writes a FieldSet for the specified object toRender.
        /// </summary>
        /// <param name="toRender"></param>
        /// <param name="output"></param>
        public override void Render(object toRender, Stream output)
        {
            string fieldSet = FieldSetFor(toRender.GetType()).ToString().XmlToHumanReadable();            
            byte[] data = Encoding.UTF8.GetBytes(fieldSet);
            output.Write(data, 0, data.Length);
        }

        protected internal static MvcHtmlString FieldSetFor(string json, string legendText = null, object wrapperAttrs = null , bool setValues = false)
        {
            return HtmlHelperExtensions.FieldsetFor(null, json, legendText, wrapperAttrs, setValues);
        }

        protected internal static MvcHtmlString FieldSetFor(dynamic obj, string legendText, object wrapperAttrs = null, bool setValues = false)
        {
            return HtmlHelperExtensions.FieldsetFor(null, obj, legendText, wrapperAttrs, setValues);
        }

        public static MvcHtmlString FieldSetFor(Type type, object defaults = null, string legendText = null, object wrapperAttrs = null)
        {
            return HtmlHelperExtensions.FieldSetFor(null, type, defaults, legendText, wrapperAttrs);
        }

        protected internal static MvcHtmlString InputsFor(Type type, object defaults = null, object wrapperAttrs = null)
        {
            return HtmlHelperExtensions.InputsFor(null, type, defaults, wrapperAttrs);
        }

    }
}
