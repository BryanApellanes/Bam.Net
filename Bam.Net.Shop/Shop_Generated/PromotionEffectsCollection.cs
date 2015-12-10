/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class PromotionEffectsCollection: DaoCollection<PromotionEffectsColumns, PromotionEffects>
    { 
		public PromotionEffectsCollection(){}
		public PromotionEffectsCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PromotionEffectsCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PromotionEffectsCollection(Query<PromotionEffectsColumns, PromotionEffects> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PromotionEffectsCollection(Database db, Query<PromotionEffectsColumns, PromotionEffects> q, bool load) : base(db, q, load) { }
		public PromotionEffectsCollection(Query<PromotionEffectsColumns, PromotionEffects> q, bool load) : base(q, load) { }
    }
}