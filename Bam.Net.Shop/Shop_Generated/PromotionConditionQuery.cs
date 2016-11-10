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
    public class PromotionConditionQuery: Query<PromotionConditionColumns, PromotionCondition>
    { 
		public PromotionConditionQuery(){}
		public PromotionConditionQuery(WhereDelegate<PromotionConditionColumns> where, OrderBy<PromotionConditionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PromotionConditionQuery(Func<PromotionConditionColumns, QueryFilter<PromotionConditionColumns>> where, OrderBy<PromotionConditionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PromotionConditionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PromotionConditionQuery Where(WhereDelegate<PromotionConditionColumns> where)
        {
            return Where(where, null, null);
        }

        public static PromotionConditionQuery Where(WhereDelegate<PromotionConditionColumns> where, OrderBy<PromotionConditionColumns> orderBy = null, Database db = null)
        {
            return new PromotionConditionQuery(where, orderBy, db);
        }

		public PromotionConditionCollection Execute()
		{
			return new PromotionConditionCollection(this, true);
		}
    }
}