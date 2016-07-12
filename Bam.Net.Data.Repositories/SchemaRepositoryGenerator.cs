using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A code and assembly generator used to generate schema
    /// specific dao repositories
    /// </summary>
    public class SchemaRepositoryGenerator: TypeDaoGenerator
    { 
        public SchemaRepositoryGenerator(Assembly typeAssembly, string sourceNamespace, ILogger logger = null):base(typeAssembly, sourceNamespace, logger)
        {
            SourceNamespace = sourceNamespace;
            DestinationNamespace = $"{sourceNamespace}._Dao_";
        }
        public string SourceNamespace { get; set; }
        
        public string DestinationNamespace
        {
            get
            {
                return Namespace;
            }
            set
            {
                Namespace = value;
            }
        }
        public override void GenerateSource(string writeSourceTo)
        {
            base.GenerateSource(writeSourceTo);
            GenerateRepositorySource(writeSourceTo);
        }
        public void GenerateRepositorySource(string writeSourceTo, string schemaName = null)
        {
            schemaName = schemaName ?? SchemaName;
            SchemaName = schemaName;
            base.GenerateSource(writeSourceTo);            
            SchemaRepositoryModel schemaModel = new SchemaRepositoryModel { SourceNamespace = SourceNamespace, DestinationNamespace = DestinationNamespace, SchemaName = schemaName, Types = Types };
            string code = schemaModel.Render();
            code.SafeWriteToFile(Path.Combine(writeSourceTo, $"{schemaName}Repository.cs"));
        }
    }
}
