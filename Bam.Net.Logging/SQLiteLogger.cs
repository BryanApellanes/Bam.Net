using Bam.Net.Configuration;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    public class SQLiteLogger : DaoLogger
    {
        public SQLiteLogger() : this(new SQLiteDatabase(RuntimeSettings.AppDataFolder, $"{DefaultConfiguration.GetAppSetting("ApplicationName", StaticApplicationNameProvider.DefaultApplicationName)}_Logs"))
        { }

        public SQLiteLogger(SQLiteDatabase database):base(database)
        {
        }
    }
}
