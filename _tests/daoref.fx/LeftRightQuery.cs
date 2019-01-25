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
    public class LeftRightQuery: Query<LeftRightColumns, LeftRight>
    { 
		public LeftRightQuery(){}
		public LeftRightQuery(WhereDelegate<LeftRightColumns> where, OrderBy<LeftRightColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LeftRightQuery(Func<LeftRightColumns, QueryFilter<LeftRightColumns>> where, OrderBy<LeftRightColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LeftRightQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static LeftRightQuery Where(WhereDelegate<LeftRightColumns> where)
        {
            return Where(where, null, null);
        }

        public static LeftRightQuery Where(WhereDelegate<LeftRightColumns> where, OrderBy<LeftRightColumns> orderBy = null, Database db = null)
        {
            return new LeftRightQuery(where, orderBy, db);
        }

		public LeftRightCollection Execute()
		{
			return new LeftRightCollection(this, true);
		}
    }
}