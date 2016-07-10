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
    public class SchemaRepositoryGenerator: Loggable
    {
        // Manually create Specific repo with query context to guide
        // template creation

        // Generate Dao code using TypeDaoGenerator.GenerateSource
        // Generate Specific repo code into same folder
        // compile into assembly
        TypeDaoGenerator _typeDaoGenerator;        
        public SchemaRepositoryGenerator(Assembly typeAssembly, string sourceNamespace, ILogger logger = null)
        {
            _typeDaoGenerator = new TypeDaoGenerator(typeAssembly, sourceNamespace, logger);
            SourceNamespace = sourceNamespace;
            DestinationNamespace = $"{sourceNamespace}._Dao_";
        }
        public Type[] Types { get { return _typeDaoGenerator.Types; } }
        public string SchemaName
        {
            get { return _typeDaoGenerator.SchemaName; }
            set { _typeDaoGenerator.SchemaName = value; }
        }
        public string SourceNamespace { get; set; }
        
        public string DestinationNamespace
        {
            get
            {
                return _typeDaoGenerator.Namespace;
            }
            set
            {
                _typeDaoGenerator.Namespace = value;
            }
        }
        public void GenerateSource(string writeSourceTo, string schemaName = null)
        {
            schemaName = schemaName ?? SchemaName;
            SchemaName = schemaName;
            _typeDaoGenerator.GenerateSource(writeSourceTo);            
            SchemaRepositoryModel schemaModel = new SchemaRepositoryModel { SourceNamespace = SourceNamespace, DestinationNamespace = DestinationNamespace, SchemaName = schemaName, Types = Types };
            string code = schemaModel.Render();
            code.SafeWriteToFile(Path.Combine(writeSourceTo, $"{schemaName}Repository.cs"));
        }
    }
}
