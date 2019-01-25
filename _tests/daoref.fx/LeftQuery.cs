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
    public class LeftQuery: Query<LeftColumns, Left>
    { 
		public LeftQuery(){}
		public LeftQuery(WhereDelegate<LeftColumns> where, OrderBy<LeftColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LeftQuery(Func<LeftColumns, QueryFilter<LeftColumns>> where, OrderBy<LeftColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LeftQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static LeftQuery Where(WhereDelegate<LeftColumns> where)
        {
            return Where(where, null, null);
        }

        public static LeftQuery Where(WhereDelegate<LeftColumns> where, OrderBy<LeftColumns> orderBy = null, Database db = null)
        {
            return new LeftQuery(where, orderBy, db);
        }

		public LeftCollection Execute()
		{
			return new LeftCollection(this, true);
		}
    }
}