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
    public class PromotionEffectsQuery: Query<PromotionEffectsColumns, PromotionEffects>
    { 
		public PromotionEffectsQuery(){}
		public PromotionEffectsQuery(WhereDelegate<PromotionEffectsColumns> where, OrderBy<PromotionEffectsColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PromotionEffectsQuery(Func<PromotionEffectsColumns, QueryFilter<PromotionEffectsColumns>> where, OrderBy<PromotionEffectsColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PromotionEffectsQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PromotionEffectsQuery Where(WhereDelegate<PromotionEffectsColumns> where)
        {
            return Where(where, null, null);
        }

        public static PromotionEffectsQuery Where(WhereDelegate<PromotionEffectsColumns> where, OrderBy<PromotionEffectsColumns> orderBy = null, Database db = null)
        {
            return new PromotionEffectsQuery(where, orderBy, db);
        }

		public PromotionEffectsCollection Execute()
		{
			return new PromotionEffectsCollection(this, true);
		}
    }
}