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
using System.Web.Script.Serialization;

namespace Bam.Net.ServiceProxy
{
    public class CSharpProxyResult : ActionResult
    {
        public CSharpProxyResult(string fileName, string nameSpace, params string[] classNames)
        {
            this.Namespace = nameSpace;
            this.ClassNames = classNames;
            this.FileName = fileName;
        }

        public string Namespace { get; set; }
        protected string ContractNamespace
        {
            get
            {
                return "{0}.Contracts.Generated"._Format(Namespace);
            }
        }

        public string FileName { get; set; }
        public string[] ClassNames { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            Uri uri = context.HttpContext.Request.Url;
            // TODO: check for X-Forwarded-For and set port to 80 if it is present
            string defaultBaseAddress = ServiceProxySystem.GetBaseAddress(uri);

            StringBuilder code = GenerateCSharpProxyCode(defaultBaseAddress);

            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName + ".cs");
            context.HttpContext.Response.AddHeader("Content-Type", "text/plain");

            context.HttpContext.Response.Write(code.ToString());
        }

        private StringBuilder GenerateCSharpProxyCode(string defaultBaseAddress)
        {
            return ServiceProxySystem.GenerateCSharpProxyCode(defaultBaseAddress, ClassNames, Namespace, ContractNamespace);
        }

    }
}
