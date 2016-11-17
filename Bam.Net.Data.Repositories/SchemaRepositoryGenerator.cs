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
        /// <summary>
        /// Instantiate an instance of SchemaRepositoryGenerator that
        /// can be used to generate a schema specific repository for the
        /// specified typeAssembly for types in the specified 
        /// sourceNamespace
        /// </summary>
        /// <param name="typeAssembly"></param>
        /// <param name="sourceNamespace"></param>
        /// <param name="logger"></param>
        public SchemaRepositoryGenerator(Assembly typeAssembly, string sourceNamespace, ILogger logger = null) 
            : base(typeAssembly, sourceNamespace, logger)
        {
            SourceNamespace = sourceNamespace;
            BaseRepositoryType = "DaoRepository";
        }

        /// <summary>
        /// The namespace to 
        /// </summary>
        public string SourceNamespace { get; set; }
        public string BaseRepositoryType { get; set; }
        public string SchemaRepositoryNamespace
        {
            get
            {
                return $"{DaoNamespace}.Repository";
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
            SchemaRepositoryModel schemaModel = new SchemaRepositoryModel
            {
                BaseRepositoryType = BaseRepositoryType,
                DaoNamespace = DaoNamespace,
                SourceNamespace = SourceNamespace,
                SchemaRepositoryNamespace = SchemaRepositoryNamespace,
                SchemaName = schemaName,
                Types = Types.Select(t => SchemaTypeModel.FromType(t, DaoNamespace)).ToArray()
            };
            string code = schemaModel.Render();
            code.SafeWriteToFile(Path.Combine(writeSourceTo, $"{schemaName}Repository.cs"));
        }
    }
}
