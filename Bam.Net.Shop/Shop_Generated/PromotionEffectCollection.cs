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
    public class PromotionEffectCollection: DaoCollection<PromotionEffectColumns, PromotionEffect>
    { 
		public PromotionEffectCollection(){}
		public PromotionEffectCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PromotionEffectCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PromotionEffectCollection(Query<PromotionEffectColumns, PromotionEffect> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PromotionEffectCollection(Database db, Query<PromotionEffectColumns, PromotionEffect> q, bool load) : base(db, q, load) { }
		public PromotionEffectCollection(Query<PromotionEffectColumns, PromotionEffect> q, bool load) : base(q, load) { }
    }
}