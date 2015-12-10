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
using System.Reflection;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace Bam.Net.Yaml
{
    public class YamlResult: ActionResult
    {
        public YamlResult(object data)
        {
            this.Data = data;
        }

        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            WriteYaml(Data, context.HttpContext.Response.OutputStream);
        }

        public void WriteYaml(Stream output)
        {
            WriteYaml(Data, output);
        }

        public void WriteYaml(object data, Stream output)
        {
            StreamWriter sw = new System.IO.StreamWriter(output);
            sw.Write(data.ToYaml());
        }
    }
}
