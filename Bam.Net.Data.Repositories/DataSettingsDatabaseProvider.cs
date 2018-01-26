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
        public DataSettingsDatabaseProvider(string root, ILogger logger = null) : base(root, logger)
        { }

        public DataSettingsDatabaseProvider(DataSettings settings, ILogger logger) : this(settings.GetSysDatabaseDirectory().FullName, logger)
        { }

    }
}
