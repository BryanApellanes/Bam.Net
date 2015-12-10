/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Razor;
using System.Reflection;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Schema
{
    public abstract class SchemaTemplate : DaoRazorTemplate<SchemaDefinition>
    {

        public void WriteContextMethods(Table table, string ns)
        {
            RazorParser<TableTemplate> razorParser = new RazorParser<TableTemplate>();
            razorParser.GetDefaultAssembliesToReference = () =>
            {
                Assembly[] assembliesToReference = new Assembly[]{typeof(SchemaTemplate).Assembly, 
					typeof(DaoGenerator).Assembly,
					typeof(ServiceProxySystem).Assembly, 
					typeof(Providers).Assembly};
                return assembliesToReference;
            };
            Write(razorParser.ExecuteResource("ContextMethods.tmpl", "Bam.Net.Data.Schema.Templates.", typeof(SchemaTemplate).Assembly, new { Model = table, Namespace = ns }));
        }
    }
}
