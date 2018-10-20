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
    public class FeatureQuery: Query<FeatureColumns, Feature>
    { 
		public FeatureQuery(){}
		public FeatureQuery(WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public FeatureQuery(Func<FeatureColumns, QueryFilter<FeatureColumns>> where, OrderBy<FeatureColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public FeatureQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static FeatureQuery Where(WhereDelegate<FeatureColumns> where)
        {
            return Where(where, null, null);
        }

        public static FeatureQuery Where(WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy = null, Database db = null)
        {
            return new FeatureQuery(where, orderBy, db);
        }

		public FeatureCollection Execute()
		{
			return new FeatureCollection(this, true);
		}
    }
}