/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ConfigSettingCollection: DaoCollection<ConfigSettingColumns, ConfigSetting>
    { 
		public ConfigSettingCollection(){}
		public ConfigSettingCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ConfigSettingCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ConfigSettingCollection(Query<ConfigSettingColumns, ConfigSetting> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ConfigSettingCollection(Database db, Query<ConfigSettingColumns, ConfigSetting> q, bool load) : base(db, q, load) { }
		public ConfigSettingCollection(Query<ConfigSettingColumns, ConfigSetting> q, bool load) : base(q, load) { }
    }
}