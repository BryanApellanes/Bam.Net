/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class SettingCollection: DaoCollection<SettingColumns, Setting>
    { 
		public SettingCollection(){}
		public SettingCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SettingCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SettingCollection(Query<SettingColumns, Setting> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SettingCollection(Database db, Query<SettingColumns, Setting> q, bool load) : base(db, q, load) { }
		public SettingCollection(Query<SettingColumns, Setting> q, bool load) : base(q, load) { }
    }
}