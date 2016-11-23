/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ConfigurationApplicationQuery: Query<ConfigurationApplicationColumns, ConfigurationApplication>
    { 
		public ConfigurationApplicationQuery(){}
		public ConfigurationApplicationQuery(WhereDelegate<ConfigurationApplicationColumns> where, OrderBy<ConfigurationApplicationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ConfigurationApplicationQuery(Func<ConfigurationApplicationColumns, QueryFilter<ConfigurationApplicationColumns>> where, OrderBy<ConfigurationApplicationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ConfigurationApplicationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ConfigurationApplicationQuery Where(WhereDelegate<ConfigurationApplicationColumns> where)
        {
            return Where(where, null, null);
        }

        public static ConfigurationApplicationQuery Where(WhereDelegate<ConfigurationApplicationColumns> where, OrderBy<ConfigurationApplicationColumns> orderBy = null, Database db = null)
        {
            return new ConfigurationApplicationQuery(where, orderBy, db);
        }

		public ConfigurationApplicationCollection Execute()
		{
			return new ConfigurationApplicationCollection(this, true);
		}
    }
}