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
using Bam.Net;
using Bam.Net.Presentation.Html;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Presentation.Html
{
    public class MethodFormResult: ActionResult
    {
        public MethodFormResult(string className, string methodName)
        {
            this.ClassName = className; 
            this.MethodName = methodName;
        }

        public MethodFormResult(string className, string methodName, object defaults)
            : this(className, methodName)
        {
            this.Defaults = defaults;
        }

        public MethodFormResult(string className, string methodName, object defaults, ParameterLayouts layout)
            : this(className, methodName, defaults)
        {
            this.Layout = layout;
        }

        public object Defaults { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public ParameterLayouts Layout { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            StringBuilder output = new StringBuilder();

            if (string.IsNullOrEmpty(ClassName))
            {
                Tag message = new Tag("div", new { Class = "error" }).Text("ClassName not specified");
                output = new StringBuilder(message.ToHtmlString());
            }
            else
            {
                Incubator incubator = ServiceProxySystem.Incubator;
                Type type;
                incubator.Get(ClassName, out type);
                if (type == null)
                {
                    Tag message = new Tag("div", new { Class = "error" }).Text("The specified className ({0}) was not registered in the ServiceProxySystem"._Format(ClassName));
                    output = new StringBuilder(message.ToHtmlString());
                }
                else
                {
                    InputFormBuilder formBuilder = new InputFormBuilder(type);
                    formBuilder.Layout = Layout;
                    Dictionary<string, object> defaults = new Dictionary<string, object>();
                    if (Defaults != null)
                    {
                        defaults = Defaults.PropertiesToDictionary();
                    }

                    TagBuilder tag = formBuilder.MethodForm(MethodName, defaults);
                    output = new StringBuilder(tag.ToMvcHtml().ToString());
                }
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.Write(output.ToString());
        }
    }
}
