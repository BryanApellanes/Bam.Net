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
    public class GroupQuery: Query<GroupColumns, Group>
    { 
		public GroupQuery(){}
		public GroupQuery(WhereDelegate<GroupColumns> where, OrderBy<GroupColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public GroupQuery(Func<GroupColumns, QueryFilter<GroupColumns>> where, OrderBy<GroupColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public GroupQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static GroupQuery Where(WhereDelegate<GroupColumns> where)
        {
            return Where(where, null, null);
        }

        public static GroupQuery Where(WhereDelegate<GroupColumns> where, OrderBy<GroupColumns> orderBy = null, Database db = null)
        {
            return new GroupQuery(where, orderBy, db);
        }

		public GroupCollection Execute()
		{
			return new GroupCollection(this, true);
		}
    }
}