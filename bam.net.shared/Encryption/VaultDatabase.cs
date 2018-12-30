using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public class VaultDatabase
    {
        public VaultDatabase(Database db)
        {
            Database = db;
            SchemaStatus = Database.TryEnsureSchema(typeof(Vault));
        }

        public VaultDatabase(FileInfo sqliteVaultDatabase) : this(SQLiteDatabase.FromFile(sqliteVaultDatabase))
        { }

        public static VaultDatabase FromFile(string sqliteVaultDatabasePath)
        {
            return new VaultDatabase(new FileInfo(sqliteVaultDatabasePath));
        }

        public static VaultDatabase FromFile(FileInfo sqliteVaultDatabase)
        {
            return new VaultDatabase(sqliteVaultDatabase);
        }

        public EnsureSchemaStatus SchemaStatus { get; private set; }
        public Database Database { get; set; }

        public static implicit operator Database(VaultDatabase vaultDatabase)
        {
            return vaultDatabase.Database;
        }

        public static implicit operator VaultDatabase(Database database)
        {
            return new VaultDatabase(database);
        }

        public string[] ListVaultNames()
        {
            VaultCollection vaults = Vault.LoadAll(Database);
            return vaults.Select(v => v.Name).ToArray();
        }

        public Vault GetVault(string vaultName)
        {
            return Vault.FirstOneWhere(v => v.Name == vaultName, Database);
        }
    }
}
