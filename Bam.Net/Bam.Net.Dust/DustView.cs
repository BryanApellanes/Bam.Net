/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using Bam.Net;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Dust
{
    public class DustView: IView
    {
        public DustView(string templateName)
        {
            this.TemplateName = templateName;
        }

        public DustView(string templateName, string masterName)
            : this(templateName)
        {
            this.MasterName = masterName;
        }

        public string MasterName { get; private set; }
        public string TemplateName { get; private set; }

        #region IView Members

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            if (Dust.TemplateIsRegistered(this.TemplateName))
            {
                object model = viewContext.ViewData.Model;
                if (model == null)
                {
                    model = new object();
                }
                MvcHtmlString result = Dust.RenderMvcHtmlString(TemplateName, model);
                writer.Write(result);
            }
        }

        #endregion
    }
}
