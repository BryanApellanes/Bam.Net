/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bam.Net.Incubation;
using System.Reflection;
using Bam.Net.Presentation.Html;

namespace Bam.Net.Dust
{
    public class DustTemplateResult: ActionResult
    {
        public DustTemplateResult(dynamic value)
        {
            this.Data = value;
            this.Legend = value.GetType().Name;
        }

        public DustTemplateResult(string legend, dynamic value)
        {
            this.Data = value;
            this.Legend = legend;
        }
        
        public string Legend { get; set; }
        public dynamic Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;

            response.Write(HtmlHelperExtensions.FieldsetFor(null, Data, Legend));
            
        }
    }
}
