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
    public class PromotionQuery: Query<PromotionColumns, Promotion>
    { 
		public PromotionQuery(){}
		public PromotionQuery(WhereDelegate<PromotionColumns> where, OrderBy<PromotionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PromotionQuery(Func<PromotionColumns, QueryFilter<PromotionColumns>> where, OrderBy<PromotionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PromotionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PromotionQuery Where(WhereDelegate<PromotionColumns> where)
        {
            return Where(where, null, null);
        }

        public static PromotionQuery Where(WhereDelegate<PromotionColumns> where, OrderBy<PromotionColumns> orderBy = null, Database db = null)
        {
            return new PromotionQuery(where, orderBy, db);
        }

		public PromotionCollection Execute()
		{
			return new PromotionCollection(this, true);
		}
    }
}