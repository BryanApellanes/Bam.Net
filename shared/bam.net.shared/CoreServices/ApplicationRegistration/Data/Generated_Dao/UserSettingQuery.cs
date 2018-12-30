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
    public class UserSettingQuery: Query<UserSettingColumns, UserSetting>
    { 
		public UserSettingQuery(){}
		public UserSettingQuery(WhereDelegate<UserSettingColumns> where, OrderBy<UserSettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserSettingQuery(Func<UserSettingColumns, QueryFilter<UserSettingColumns>> where, OrderBy<UserSettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserSettingQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static UserSettingQuery Where(WhereDelegate<UserSettingColumns> where)
        {
            return Where(where, null, null);
        }

        public static UserSettingQuery Where(WhereDelegate<UserSettingColumns> where, OrderBy<UserSettingColumns> orderBy = null, Database db = null)
        {
            return new UserSettingQuery(where, orderBy, db);
        }

		public UserSettingCollection Execute()
		{
			return new UserSettingCollection(this, true);
		}
    }
}