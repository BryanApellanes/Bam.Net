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

namespace Bam.Net.Encryption
{
    public class VaultDatabaseInitializer: SQLiteDatabaseInitializer
    {
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
            DatabaseInitializationResult result = base.Initialize("Encryption");
            if (result.Success)
            {
                result.Initializer = this;
                SQLiteRegistrar.Register(result.Database.ServiceProvider);
            }
            else
            {
                throw result.Exception;
            }

            return result;
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
