/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests
{
    public class MainObjectQuery: Query<MainObjectColumns, MainObject>
    { 
		public MainObjectQuery(){}
		public MainObjectQuery(WhereDelegate<MainObjectColumns> where, OrderBy<MainObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public MainObjectQuery(Func<MainObjectColumns, QueryFilter<MainObjectColumns>> where, OrderBy<MainObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public MainObjectQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static MainObjectQuery Where(WhereDelegate<MainObjectColumns> where)
        {
            return Where(where, null, null);
        }

        public static MainObjectQuery Where(WhereDelegate<MainObjectColumns> where, OrderBy<MainObjectColumns> orderBy = null, Database db = null)
        {
            return new MainObjectQuery(where, orderBy, db);
        }

		public MainObjectCollection Execute()
		{
			return new MainObjectCollection(this, true);
		}
    }
}