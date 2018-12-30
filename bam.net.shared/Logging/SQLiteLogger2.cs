using Bam.Net.Configuration;
using Bam.Net.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    public class SQLiteLogger2: DaoLogger2
    {
        public SQLiteLogger2() : this(new SQLiteDatabase(RuntimeSettings.AppDataFolder, $"{DefaultConfigurationApplicationNameProvider.Instance.GetApplicationName()}_Logs"))
        { }

        public SQLiteLogger2(SQLiteDatabase database) : base(database)
        {
        }
    }
}
