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
    public class UserSettingCollection: DaoCollection<UserSettingColumns, UserSetting>
    { 
		public UserSettingCollection(){}
		public UserSettingCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UserSettingCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserSettingCollection(Query<UserSettingColumns, UserSetting> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserSettingCollection(Database db, Query<UserSettingColumns, UserSetting> q, bool load) : base(db, q, load) { }
		public UserSettingCollection(Query<UserSettingColumns, UserSetting> q, bool load) : base(q, load) { }
    }
}