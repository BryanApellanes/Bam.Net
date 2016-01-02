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
    public class PromotionConditionCollection: DaoCollection<PromotionConditionColumns, PromotionCondition>
    { 
		public PromotionConditionCollection(){}
		public PromotionConditionCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PromotionConditionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PromotionConditionCollection(Query<PromotionConditionColumns, PromotionCondition> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PromotionConditionCollection(Database db, Query<PromotionConditionColumns, PromotionCondition> q, bool load) : base(db, q, load) { }
		public PromotionConditionCollection(Query<PromotionConditionColumns, PromotionCondition> q, bool load) : base(q, load) { }
    }
}