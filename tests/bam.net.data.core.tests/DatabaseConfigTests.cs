using System;
using System.Collections.Generic;
using Bam.Net.CommandLine;
using Bam.Net.Data.Dynamic;
using Bam.Net.Data.Npgsql;
using Bam.Net.Data.Repositories;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Tests
{
    public class DatabaseConfigTests: CommandLineTool
    {
        [ConsoleAction("dynamicQuery", "Run the dynamicQuery test")]
        [TestGroup("Data.Dynamic")]
        [UnitTest]
        public void GetDbFromConfig()
        {
            Sql.In(DatabaseConfig.GetFirstDatabase())
                .Query(
                    new
                    {
                        Table = "entity.taskcase",
                        Where = new
                        {
                            id = Guid.Parse("8561eb80-133a-4fa3-9803-906bd9a6e3f3")
                        }
                    }).Each(app =>
                            {
                                OutLine(Reflect.ToJson(app));
                            });
            
        }
        
        [ConsoleAction("loadDbConfigTest", "Run the loadDbConfigTest test")]
        [TestGroup("Data.Core")]
        [UnitTest]
        public void CanLoadDatabaseConfigs()
        {
            DatabaseConfig[] configs = DatabaseConfig.LoadConfigs();
            Expect.IsGreaterThan(configs.Length, 0);
            OutLine(configs.ToJson(true));
        }
        
        [ConsoleAction("dbTest", "Run the dbTest test")]
        [UnitTest]
        [TestGroup("Data.Core")]
        public void CanDeserializeDatabaseConfig()
        {
            DatabaseConfig config = new DatabaseConfig()
            {
                DatabaseType = RelationalDatabaseTypes.Npgsql,
                ConnectionName = "TheConnectionName",
                DatabaseName = "TheNameOfTheDatabase",
                Credentials = new NpgsqlCredentials()
                {
                    UserId = "userId",
                    Password = "not a real password"
                }
            };
            DatabaseConfig config2 = new DatabaseConfig()
            {
                DatabaseType = RelationalDatabaseTypes.Npgsql,
                ConnectionName = "TheConnectionName2",
                DatabaseName = "TheNameOfTheDatabase",
                Credentials = new NpgsqlCredentials()
                {
                    UserId = "userId2",
                    Password = "not a real password2"
                }
            };

            string json = config.ToJson(true);
            OutLine(json, ConsoleColor.Cyan);

            DatabaseConfig rehydrated = json.FromJson<DatabaseConfig>();

            OutLine(rehydrated.ToJson(true), ConsoleColor.Green);

            OutLine(rehydrated.ToYaml(), ConsoleColor.DarkGreen);

            List<DatabaseConfig> configs = new List<DatabaseConfig>()
            {
                config,
                config2
            };

            OutLine(configs.ToYaml(), ConsoleColor.Blue);
        }
    }
}