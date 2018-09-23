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
using System.Xml.Serialization;

namespace Bam.Net.ServiceProxy
{
    public class XmlResult: ActionResult
    {
        public XmlResult(object data)
        {
            this.Data = data;
        }

        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            WriteXml(Data, context.HttpContext.Response.OutputStream);
        }

        public void WriteXml(Stream output)
        {
            WriteXml(Data, output);
        }

        public void WriteXml(object data, Stream output)
        {
            XmlSerializer ser = new XmlSerializer(data.GetType());
            ser.Serialize(output, data);            
        }
    }
}
