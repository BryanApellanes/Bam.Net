/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ConfigurationSettingQuery: Query<ConfigurationSettingColumns, ConfigurationSetting>
    { 
		public ConfigurationSettingQuery(){}
		public ConfigurationSettingQuery(WhereDelegate<ConfigurationSettingColumns> where, OrderBy<ConfigurationSettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ConfigurationSettingQuery(Func<ConfigurationSettingColumns, QueryFilter<ConfigurationSettingColumns>> where, OrderBy<ConfigurationSettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ConfigurationSettingQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ConfigurationSettingQuery Where(WhereDelegate<ConfigurationSettingColumns> where)
        {
            return Where(where, null, null);
        }

        public static ConfigurationSettingQuery Where(WhereDelegate<ConfigurationSettingColumns> where, OrderBy<ConfigurationSettingColumns> orderBy = null, Database db = null)
        {
            return new ConfigurationSettingQuery(where, orderBy, db);
        }

		public ConfigurationSettingCollection Execute()
		{
			return new ConfigurationSettingCollection(this, true);
		}
    }
}