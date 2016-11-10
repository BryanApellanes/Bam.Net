/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class PriceQuery: Query<PriceColumns, Price>
    { 
		public PriceQuery(){}
		public PriceQuery(WhereDelegate<PriceColumns> where, OrderBy<PriceColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PriceQuery(Func<PriceColumns, QueryFilter<PriceColumns>> where, OrderBy<PriceColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PriceQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PriceQuery Where(WhereDelegate<PriceColumns> where)
        {
            return Where(where, null, null);
        }

        public static PriceQuery Where(WhereDelegate<PriceColumns> where, OrderBy<PriceColumns> orderBy = null, Database db = null)
        {
            return new PriceQuery(where, orderBy, db);
        }

		public PriceCollection Execute()
		{
			return new PriceCollection(this, true);
		}
    }
}