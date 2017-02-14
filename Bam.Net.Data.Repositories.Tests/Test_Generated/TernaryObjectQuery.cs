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
    public class TernaryObjectQuery: Query<TernaryObjectColumns, TernaryObject>
    { 
		public TernaryObjectQuery(){}
		public TernaryObjectQuery(WhereDelegate<TernaryObjectColumns> where, OrderBy<TernaryObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TernaryObjectQuery(Func<TernaryObjectColumns, QueryFilter<TernaryObjectColumns>> where, OrderBy<TernaryObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TernaryObjectQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TernaryObjectQuery Where(WhereDelegate<TernaryObjectColumns> where)
        {
            return Where(where, null, null);
        }

        public static TernaryObjectQuery Where(WhereDelegate<TernaryObjectColumns> where, OrderBy<TernaryObjectColumns> orderBy = null, Database db = null)
        {
            return new TernaryObjectQuery(where, orderBy, db);
        }

		public TernaryObjectCollection Execute()
		{
			return new TernaryObjectCollection(this, true);
		}
    }
}