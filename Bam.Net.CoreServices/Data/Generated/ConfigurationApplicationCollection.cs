/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ConfigurationApplicationCollection: DaoCollection<ConfigurationApplicationColumns, ConfigurationApplication>
    { 
		public ConfigurationApplicationCollection(){}
		public ConfigurationApplicationCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ConfigurationApplicationCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ConfigurationApplicationCollection(Query<ConfigurationApplicationColumns, ConfigurationApplication> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ConfigurationApplicationCollection(Database db, Query<ConfigurationApplicationColumns, ConfigurationApplication> q, bool load) : base(db, q, load) { }
		public ConfigurationApplicationCollection(Query<ConfigurationApplicationColumns, ConfigurationApplication> q, bool load) : base(q, load) { }
    }
}