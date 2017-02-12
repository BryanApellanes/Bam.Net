/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class ListQuery: Query<ListColumns, List>
    { 
		public ListQuery(){}
		public ListQuery(WhereDelegate<ListColumns> where, OrderBy<ListColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ListQuery(Func<ListColumns, QueryFilter<ListColumns>> where, OrderBy<ListColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ListQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ListQuery Where(WhereDelegate<ListColumns> where)
        {
            return Where(where, null, null);
        }

        public static ListQuery Where(WhereDelegate<ListColumns> where, OrderBy<ListColumns> orderBy = null, Database db = null)
        {
            return new ListQuery(where, orderBy, db);
        }

		public ListCollection Execute()
		{
			return new ListCollection(this, true);
		}
    }
}