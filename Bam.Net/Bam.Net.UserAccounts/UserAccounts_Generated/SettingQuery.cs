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
    public class SettingQuery: Query<SettingColumns, Setting>
    { 
		public SettingQuery(){}
		public SettingQuery(WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SettingQuery(Func<SettingColumns, QueryFilter<SettingColumns>> where, OrderBy<SettingColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SettingQuery(Delegate where, Database db = null) : base(where, db) { }

		public SettingCollection Execute()
		{
			return new SettingCollection(this, true);
		}
    }
}