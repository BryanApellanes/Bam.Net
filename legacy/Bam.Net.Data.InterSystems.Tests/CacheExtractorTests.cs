using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Data.Cache;
using Bam.Net.Data.Dynamic;
using Bam.Net.Data.Schema;
using Bam.Net.Incubation;
using Bam.Net.Testing.Integration;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;
using Bam.Net.Data.Cache.Tests;

namespace Bam.Net.Cache.Tests
{
    [Serializable]
    public class CacheExtractorTests: CommandLineTestInterface
    {
        static ServiceRegistry _serviceRegistry;
        
        static CacheExtractorTests()
        {
            CacheConnectionStrings _connectionStrings = "C:\\src\\config\\CacheCreds.json".FromJsonFile<CacheConnectionStrings>();
            CacheSchemaExtractorConfig config = new CacheSchemaExtractorConfig { TableNameFilter = "%EnsLib.Workflow%", ConnectionString = _connectionStrings.WA };
            _serviceRegistry = new ServiceRegistry();
            _serviceRegistry.Include(
                Asking.For<ISchemaExtractor>().Returns<CacheSchemaExtractor>()
                .For<CacheConnectionStrings>().Use(_connectionStrings)
                .For<CacheSchemaExtractorConfig>().Use(config)
                .For<CacheSchemaExtractor>().Use<CacheSchemaExtractor>()
            );            
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanGetTableNames()
        {
            CacheConnectionStrings connectionStrings = _serviceRegistry.Get<CacheConnectionStrings>();
            CacheSchemaExtractorConfig config = new CacheSchemaExtractorConfig { TableNameFilter = "%EnsLib.Workflow%", ConnectionString = connectionStrings.WA };
            CacheDatabase db = new CacheDatabase(connectionStrings.WA);
            CacheSchemaExtractor schemaExtractor = new CacheSchemaExtractor(config);
            string[] tableNames = schemaExtractor.GetTableNames();
            Expect.IsNotNull(tableNames, "tableNames was null");
            Expect.IsTrue(tableNames.Length > 0);
        }

        [ConsoleAction]
        public void CanExtractCacheSchema()
        {
            CacheSchemaExtractor schemaExtractor = _serviceRegistry.Get<CacheSchemaExtractor>();
            DaoGenerator generator = new DaoGenerator("Premera.Ehri.EnsLib.Data");
            
            generator.Generate(schemaExtractor.Extract(), "C:\\bam\\daoWorkspace\\src");
        }

    }
}
