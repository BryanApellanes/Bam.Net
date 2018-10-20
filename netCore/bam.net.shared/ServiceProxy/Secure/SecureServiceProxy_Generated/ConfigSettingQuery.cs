/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ConfigSettingQuery: Query<ConfigSettingColumns, ConfigSetting>
    { 
		public ConfigSettingQuery(){}
		public ConfigSettingQuery(WhereDelegate<ConfigSettingColumns> where, OrderBy<ConfigSettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ConfigSettingQuery(Func<ConfigSettingColumns, QueryFilter<ConfigSettingColumns>> where, OrderBy<ConfigSettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ConfigSettingQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ConfigSettingQuery Where(WhereDelegate<ConfigSettingColumns> where)
        {
            return Where(where, null, null);
        }

        public static ConfigSettingQuery Where(WhereDelegate<ConfigSettingColumns> where, OrderBy<ConfigSettingColumns> orderBy = null, Database db = null)
        {
            return new ConfigSettingQuery(where, orderBy, db);
        }

		public ConfigSettingCollection Execute()
		{
			return new ConfigSettingCollection(this, true);
		}
    }
}