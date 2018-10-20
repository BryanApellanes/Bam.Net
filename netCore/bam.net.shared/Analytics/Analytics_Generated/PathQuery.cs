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
    public class PathQuery: Query<PathColumns, Path>
    { 
		public PathQuery(){}
		public PathQuery(WhereDelegate<PathColumns> where, OrderBy<PathColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PathQuery(Func<PathColumns, QueryFilter<PathColumns>> where, OrderBy<PathColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PathQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PathQuery Where(WhereDelegate<PathColumns> where)
        {
            return Where(where, null, null);
        }

        public static PathQuery Where(WhereDelegate<PathColumns> where, OrderBy<PathColumns> orderBy = null, Database db = null)
        {
            return new PathQuery(where, orderBy, db);
        }

		public PathCollection Execute()
		{
			return new PathCollection(this, true);
		}
    }
}