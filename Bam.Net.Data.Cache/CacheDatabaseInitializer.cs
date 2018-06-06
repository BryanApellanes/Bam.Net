/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace Bam.Net.Data.Cache
{
    public class CacheDatabaseInitializer : DefaultDatabaseInitializer
    {
        public CacheDatabaseInitializer()
        {
        }

        public CacheDatabaseInitializer(params string[] ignoreConnectionNames)
        {
            this.Ignore(ignoreConnectionNames);
        }

        public CacheDatabaseInitializer(params Type[] ignoreConnectionsForTypes)
        {
            this.Ignore(ignoreConnectionsForTypes);
        }

        public override Database GetDatabase(ConnectionStringSettings conn, DbProviderFactory factory)
        {
            Database db = base.GetDatabase(conn, factory);
            CacheRegistrar.Register(db.ServiceProvider);
            return db;
        }
    }
}
