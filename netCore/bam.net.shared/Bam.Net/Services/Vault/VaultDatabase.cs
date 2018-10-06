using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Encryption;

namespace Bam.Net.Services
{
    public class VaultDatabase
    {
        public VaultDatabase(Database db)
        {
            Database = db;
        }

        public static implicit operator Database(VaultDatabase db)
        {
            return db.Database;
        }

        public static implicit operator VaultDatabase(Database db)
        {
            return new VaultDatabase(db);
        }

        public Database Database { get; set; }

        static VaultDatabase _default;
        static object _defaultLock = new object();
        public static VaultDatabase System
        {
            get
            {
                return _defaultLock.DoubleCheckLock<VaultDatabase>(ref _default, () => Vault.SystemVaultDatabase);
            }
        }
    }
}
