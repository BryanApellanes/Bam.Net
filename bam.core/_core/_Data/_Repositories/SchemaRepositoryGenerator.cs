using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public partial class SchemaRepositoryGenerator
    {
        public virtual void GenerateRepositorySource(string writeSourceTo, string schemaName = null)
        {
            Args.ThrowIfNull(TemplateRenderer, "TemplateRenderer");

            schemaName = schemaName ?? SchemaName;
            SchemaName = schemaName;
            base.GenerateSource(writeSourceTo);
            SchemaRepositoryModel schemaModel = new SchemaRepositoryModel
            {
                BaseRepositoryType = BaseRepositoryType,
                BaseNamespace = SourceNamespace,
                SchemaRepositoryNamespace = SchemaRepositoryNamespace,
                SchemaName = schemaName,
                Types = Types.Select(GetSchemaTypeModel).ToArray()
            };

            string filePath = Path.Combine(writeSourceTo, $"{schemaName}Repository.cs");
            if (File.Exists(filePath))
            {
                File.Move(filePath, filePath.GetNextFileName());
            }
            TemplateRenderer.Render("SchemaRepository", schemaModel, new FileStream(filePath, FileMode.Create));
        }
    }
}
