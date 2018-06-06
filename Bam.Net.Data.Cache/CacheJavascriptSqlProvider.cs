/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Cache;
using Bam.Net.Configuration;

namespace Bam.Net.Javascript.Sql
{
    public class CacheJavscriptSqlProvider : JavaScriptSqlProvider
    {
        public CacheJavscriptSqlProvider()
        {
        }

        public string CacheUserId { get; set; }
        public string CachePassword { get; set; }
        public string CacheServerName { get; set; }
        public string CacheDatabaseName { get; set; }

        protected override void Initialize()
        {
            CacheDatabase database = new CacheDatabase();
            CacheCredentials creds = new CacheCredentials { UserName = CacheUserId, Password = CachePassword };
            CacheConnectionStringResolver conn = new CacheConnectionStringResolver(CacheServerName, CacheDatabaseName, creds);
            database.ConnectionStringResolver = conn;
            Database = database;
        }

        #region IHasRequiredProperties Members

        public override string[] RequiredProperties
        {
            get
            {
                return new string[] { "CacheServerName", "CacheDatabaseName" };
            }
        }

        #endregion
    }
}
