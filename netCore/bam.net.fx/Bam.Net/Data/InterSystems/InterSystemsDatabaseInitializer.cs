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

namespace Bam.Net.Data.Intersystems
{
    public class InterSystemsDatabaseInitializer : DefaultDatabaseInitializer
    {
        public InterSystemsDatabaseInitializer()
        {
        }

        public InterSystemsDatabaseInitializer(params string[] ignoreConnectionNames)
        {
            this.Ignore(ignoreConnectionNames);
        }

        public InterSystemsDatabaseInitializer(params Type[] ignoreConnectionsForTypes)
        {
            this.Ignore(ignoreConnectionsForTypes);
        }

        public override Database GetDatabase(ConnectionStringSettings conn, DbProviderFactory factory)
        {
            Database db = base.GetDatabase(conn, factory);
            InterSystemsRegistrar.Register(db.ServiceProvider);
            return db;
        }
    }
}
