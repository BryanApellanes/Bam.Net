/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class SettingQuery: Query<SettingColumns, Setting>
    { 
		public SettingQuery(){}
		public SettingQuery(WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SettingQuery(Func<SettingColumns, QueryFilter<SettingColumns>> where, OrderBy<SettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SettingQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SettingQuery Where(WhereDelegate<SettingColumns> where)
        {
            return Where(where, null, null);
        }

        public static SettingQuery Where(WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy = null, Database db = null)
        {
            return new SettingQuery(where, orderBy, db);
        }

		public SettingCollection Execute()
		{
			return new SettingCollection(this, true);
		}
    }
}