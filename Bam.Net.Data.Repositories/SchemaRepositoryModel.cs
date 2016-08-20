using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public class SchemaRepositoryModel
    {
        public SchemaRepositoryModel()
        {
            BaseRepositoryType = "DaoRepository";
        }
        public string SchemaName { get; set; }
        public SchemaTypeModel[] Types { get; set; }
        public string SourceNamespace { get; set; }
        public string SchemaRepositoryNamespace { get; set; }
        public string BaseRepositoryType { get; set; }
        public string Render()
        {
            List<Assembly> referenceAssemblies = new List<Assembly>{
                    typeof(DaoGenerator).Assembly,
                    typeof(ServiceProxyController).Assembly,
                    typeof(Args).Assembly,
                    typeof(DaoRepository).Assembly};
            RazorParser<SchemaRepositoryTemplate> parser = new RazorParser<SchemaRepositoryTemplate>(RazorBaseTemplate.DefaultInspector);
            string output = parser.ExecuteResource("SchemaRepository.tmpl", "Bam.Net.Data.Repositories.Templates.", typeof(SchemaRepositoryModel).Assembly, new { Model = this }, referenceAssemblies.ToArray());
            return output;
        }
    }
}
