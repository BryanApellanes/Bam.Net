/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using System.Configuration;
using System.IO;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Encryption
{
    public class VaultDatabaseInitializer: SQLiteDatabaseInitializer
    {
        public const string DbFileName = "Vault";

        public VaultDatabaseInitializer() { }
        public VaultDatabaseInitializer(FileInfo vaultFile)
        {
            this.VaultFile = vaultFile;
        }

        public VaultDatabaseInitializer(string vaultFilePath)
            : this(new FileInfo(vaultFilePath))
        {
        }

        public FileInfo VaultFile
        {
            get;
            set;
        }

        public DatabaseInitializationResult Initialize()
        {
            try
            {
                DataPaths paths = DataPaths.Get(DefaultDataDirectoryProvider.Instance);
                string appDbDir = VaultFile == null ? paths.AppDatabase : VaultFile.Directory.FullName;
                string fileName = VaultFile == null ? DbFileName : VaultFile.Name;
                return new DatabaseInitializationResult(VaultDatabase.FromFile(Path.Combine(appDbDir, fileName))) { Success = true };
            }
            catch (Exception ex)
            {
                return new DatabaseInitializationResult { Exception = ex, Success = false };
            }
        }

        public override Database GetDatabase(ConnectionStringSettings conn, System.Data.Common.DbProviderFactory factory)
        {
            Database db = base.GetDatabase(conn, factory);
            TryEnsureSchema(db);
            return db;
        }

        public static bool TryEnsureSchema(Database db)
        {
            return TryEnsureSchema(db, out Exception ignore);
        }

        public static bool TryEnsureSchema(Database db, out Exception ex)
        {
            bool result = false;
            ex = null;
            try
            {
                SchemaWriter schemaWriter = db.ServiceProvider.Get<SchemaWriter>();
                schemaWriter.WriteSchemaScript(typeof(EncryptionContext));
                db.ExecuteSql(schemaWriter);
                result = true;
            }
            catch (Exception e)
            {
                ex = e;
                result = false;
            }

            return result;
        }

        public override ConnectionStringResolveResult ResolveConnectionString(string connectionName)
        {
            try
            {
                Args.ThrowIfNull(VaultFile, "VaultFile");

                ConnectionStringSettings s = new ConnectionStringSettings
                {
                    ProviderName = SQLiteRegistrar.SQLiteFactoryAssemblyQualifiedName(),
                    ConnectionString = string.Format("Data Source={0};Version=3;", VaultFile.FullName)
                };

                return new ConnectionStringResolveResult(s);
            }
            catch (Exception ex)
            {
                return new ConnectionStringResolveResult(null, ex);
            }
        }

    }
}
