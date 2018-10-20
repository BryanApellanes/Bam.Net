/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ConfigurationSettingCollection: DaoCollection<ConfigurationSettingColumns, ConfigurationSetting>
    { 
		public ConfigurationSettingCollection(){}
		public ConfigurationSettingCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ConfigurationSettingCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ConfigurationSettingCollection(Query<ConfigurationSettingColumns, ConfigurationSetting> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ConfigurationSettingCollection(Database db, Query<ConfigurationSettingColumns, ConfigurationSetting> q, bool load) : base(db, q, load) { }
		public ConfigurationSettingCollection(Query<ConfigurationSettingColumns, ConfigurationSetting> q, bool load) : base(q, load) { }
    }
}