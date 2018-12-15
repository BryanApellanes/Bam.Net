using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using System.Reflection;
using System.Collections;
using System.Data;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// Represents a backup of a Dao database schema and the data therein
    /// </summary>
    public partial class DaoBackup
    {
        public DaoBackup(Assembly daoAssembly, Database databaseToBackup)
            : this(daoAssembly, databaseToBackup, new ObjectRepository(DefaultDataDirectoryProvider.Current.AppDataDirectory))
        { }
    }
}
