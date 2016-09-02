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
    public class PromotionEffectQuery: Query<PromotionEffectColumns, PromotionEffect>
    { 
		public PromotionEffectQuery(){}
		public PromotionEffectQuery(WhereDelegate<PromotionEffectColumns> where, OrderBy<PromotionEffectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PromotionEffectQuery(Func<PromotionEffectColumns, QueryFilter<PromotionEffectColumns>> where, OrderBy<PromotionEffectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PromotionEffectQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PromotionEffectQuery Where(WhereDelegate<PromotionEffectColumns> where)
        {
            return Where(where, null, null);
        }

        public static PromotionEffectQuery Where(WhereDelegate<PromotionEffectColumns> where, OrderBy<PromotionEffectColumns> orderBy = null, Database db = null)
        {
            return new PromotionEffectQuery(where, orderBy, db);
        }

		public PromotionEffectCollection Execute()
		{
			return new PromotionEffectCollection(this, true);
		}
    }
}