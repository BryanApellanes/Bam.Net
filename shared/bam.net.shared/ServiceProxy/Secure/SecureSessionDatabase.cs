using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// Database used to store secure session information.  Acts 
    /// as a wrapper to whatever database is specified to the constructor
    /// or a SQLiteDatabase
    /// </summary>
    public class SecureSessionDatabase: Database
    {            
        public SecureSessionDatabase() : this(DefaultConfigurationApplicationNameProvider.Instance)
        { }

        public SecureSessionDatabase(IApplicationNameProvider applicationNameProvider) : this(DefaultDataDirectoryProvider.Current.GetAppDatabase(applicationNameProvider, Dao.ConnectionName(typeof(SecureSession))))
        { }

        public SecureSessionDatabase(Database actual)
        {
            actual.TryEnsureSchema(typeof(SecureSession));
            this.CopyProperties(actual);
            Actual = actual;
        }

        public Database Actual { get; set; }
    }
}
