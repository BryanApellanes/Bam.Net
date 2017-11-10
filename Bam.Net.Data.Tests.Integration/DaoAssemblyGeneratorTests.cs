/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Incubation;
using Bam.Net.Data.MsSql;
using Bam.Net.Data.Schema;
using Bam.Net.Data.Dynamic;
using Bam.Net.Testing.Integration;

namespace Bam.Net.Data.Tests.Integration
{
    public class DaoAssemblyGeneratorTests: CommandLineTestInterface
    {
        [IntegrationTest]
        public void DaoAssemblyGenerator()
        {
            MsSqlCredentials creds = new MsSqlCredentials { UserName = "mssqluser", Password = "mssqlP455w0rd" };
            MsSqlDatabase db = new MsSqlDatabase(new MsSqlConnectionStringResolver("chumsql2", "Db_Sillydatabase", creds));
            MsSqlSmoSchemaExtractor extractor = new MsSqlSmoSchemaExtractor(db);
            DaoAssemblyGenerator generator = new DaoAssemblyGenerator(extractor, "C:\\Bam\\Data\\Test");
            
            Out(generator.PropertiesToString(), ConsoleColor.Cyan);
        }
    }
}
