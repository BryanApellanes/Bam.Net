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
    public class PromotionCodeQuery: Query<PromotionCodeColumns, PromotionCode>
    { 
		public PromotionCodeQuery(){}
		public PromotionCodeQuery(WhereDelegate<PromotionCodeColumns> where, OrderBy<PromotionCodeColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PromotionCodeQuery(Func<PromotionCodeColumns, QueryFilter<PromotionCodeColumns>> where, OrderBy<PromotionCodeColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PromotionCodeQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PromotionCodeQuery Where(WhereDelegate<PromotionCodeColumns> where)
        {
            return Where(where, null, null);
        }

        public static PromotionCodeQuery Where(WhereDelegate<PromotionCodeColumns> where, OrderBy<PromotionCodeColumns> orderBy = null, Database db = null)
        {
            return new PromotionCodeQuery(where, orderBy, db);
        }

		public PromotionCodeCollection Execute()
		{
			return new PromotionCodeCollection(this, true);
		}
    }
}