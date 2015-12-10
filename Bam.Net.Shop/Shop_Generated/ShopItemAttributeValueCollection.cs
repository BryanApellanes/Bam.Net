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
    public class ShopItemAttributeValueCollection: DaoCollection<ShopItemAttributeValueColumns, ShopItemAttributeValue>
    { 
		public ShopItemAttributeValueCollection(){}
		public ShopItemAttributeValueCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShopItemAttributeValueCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShopItemAttributeValueCollection(Query<ShopItemAttributeValueColumns, ShopItemAttributeValue> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShopItemAttributeValueCollection(Database db, Query<ShopItemAttributeValueColumns, ShopItemAttributeValue> q, bool load) : base(db, q, load) { }
		public ShopItemAttributeValueCollection(Query<ShopItemAttributeValueColumns, ShopItemAttributeValue> q, bool load) : base(q, load) { }
    }
}