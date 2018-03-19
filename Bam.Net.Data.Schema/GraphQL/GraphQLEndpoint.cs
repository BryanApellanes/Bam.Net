using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Razor;

namespace Bam.Net.Data.Schema.GraphQL
{
    public class GraphQLEndpoint
    {
        public string EndpointName { get; set; }
        public GraphQLArgument[] Arguments { get; set; }
        public string ReturnType { get; set; }
        
        public string Render()
        {
            return Render<GraphQLEndpoint>("GraphQLEndpoint.tmpl");
        }

        protected string Render<T>(string templateName, string ns = "")
        {
            Type type = this.GetType();
            string namespacePath = string.Format("{0}.Templates.", type.Namespace);
            RazorParser<DaoRazorTemplate<T>> razorParser = new RazorParser<DaoRazorTemplate<T>>();
            razorParser.GetDefaultAssembliesToReference = DaoGenerator.GetReferenceAssemblies;
            return razorParser.ExecuteResource(templateName, namespacePath, type.Assembly, new { Model = this, Namespace = ns });
        }
    }
}
