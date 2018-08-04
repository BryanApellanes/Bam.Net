using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    public class DataSettingsDatabaseProvider : SQLiteDatabaseProvider
    {
        public DataSettingsDatabaseProvider(DefaultDatabaseProvider settings, ILogger logger) : base(settings.GetSysDatabaseDirectory().FullName, logger)
        { }

    }
}
