/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class RightQuery: Query<RightColumns, Right>
    { 
		public RightQuery(){}
		public RightQuery(WhereDelegate<RightColumns> where, OrderBy<RightColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public RightQuery(Func<RightColumns, QueryFilter<RightColumns>> where, OrderBy<RightColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public RightQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static RightQuery Where(WhereDelegate<RightColumns> where)
        {
            return Where(where, null, null);
        }

        public static RightQuery Where(WhereDelegate<RightColumns> where, OrderBy<RightColumns> orderBy = null, Database db = null)
        {
            return new RightQuery(where, orderBy, db);
        }

		public RightCollection Execute()
		{
			return new RightCollection(this, true);
		}
    }
}