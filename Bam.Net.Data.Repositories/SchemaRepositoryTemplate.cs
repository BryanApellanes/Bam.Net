using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Razor;
using System.Reflection;
using Bam.Net.Data.Schema;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public abstract class SchemaRepositoryTemplate: RazorTemplate<SchemaRepositoryModel>
    {
        public void WriteAddType(Type type)
        {
            Write(Render<Type>("SchemaRepositoryAddType.tmpl", new { Model = type }));
        }

        public void WriteMethods(Type type)
        {
            Write(Render<Type>("SchemaRepositoryMethods.tmpl", new { Model = type }));
        }

        private string Render<T>(string templateName, object options)
        {
            List<Assembly> referenceAssemblies = new List<Assembly>{
                    typeof(DaoGenerator).Assembly,
                    typeof(ServiceProxyController).Assembly,
                    typeof(Args).Assembly,
                    typeof(DaoRepository).Assembly};

            RazorParser<RazorTemplate<T>> parser = new RazorParser<RazorTemplate<T>>();
            string result = parser.ExecuteResource(templateName, "Bam.Net.Data.Repositories.Templates.", typeof(SchemaRepositoryTemplate).Assembly, options, referenceAssemblies.ToArray()).Trim();
            return result;
        }
    }
}
