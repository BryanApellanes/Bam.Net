using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

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

        public Database Database { get; set; }
    }
}
