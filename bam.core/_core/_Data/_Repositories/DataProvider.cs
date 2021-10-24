using System;
using System.IO;
using Bam.Net.Configuration;
using Bam.Net.Data.SQLite;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.Data.Repositories
{
    public partial class DataProvider
    {
        public virtual IRepository GetSysRepository()
        {
            return new DaoRepository();
        }
    }
}
