using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    /// <summary>
    /// Class used to report diagnostic information about a database.
    /// </summary>
    public class DatabaseInfo
    {
        public DatabaseInfo(Database database)
        {
            this.DatabaseType = database.GetType().FullName;
            this.ConnectionString = database.ConnectionString;
            this.ConnectionName = database.ConnectionName;
        }
        public string DatabaseType { get; private set; }
        public string ConnectionString { get; private set; }
        public string ConnectionName { get; private set; }

        public override int GetHashCode()
        {
            return string.Format("{0}{1}{2}", DatabaseType, ConnectionString, ConnectionName).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            DatabaseInfo dbInfo = obj as DatabaseInfo;
            if (dbInfo == null)
            {
                return false;
            }
            return dbInfo.GetHashCode() == this.GetHashCode();
        }
    }
}
