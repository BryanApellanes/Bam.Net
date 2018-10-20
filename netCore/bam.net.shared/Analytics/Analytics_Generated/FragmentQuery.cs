/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class FragmentQuery: Query<FragmentColumns, Fragment>
    { 
		public FragmentQuery(){}
		public FragmentQuery(WhereDelegate<FragmentColumns> where, OrderBy<FragmentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public FragmentQuery(Func<FragmentColumns, QueryFilter<FragmentColumns>> where, OrderBy<FragmentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public FragmentQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static FragmentQuery Where(WhereDelegate<FragmentColumns> where)
        {
            return Where(where, null, null);
        }

        public static FragmentQuery Where(WhereDelegate<FragmentColumns> where, OrderBy<FragmentColumns> orderBy = null, Database db = null)
        {
            return new FragmentQuery(where, orderBy, db);
        }

		public FragmentCollection Execute()
		{
			return new FragmentCollection(this, true);
		}
    }
}