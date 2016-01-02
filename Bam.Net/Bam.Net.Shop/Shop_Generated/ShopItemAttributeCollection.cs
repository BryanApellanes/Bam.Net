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
    public class ShopItemAttributeCollection: DaoCollection<ShopItemAttributeColumns, ShopItemAttribute>
    { 
		public ShopItemAttributeCollection(){}
		public ShopItemAttributeCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ShopItemAttributeCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ShopItemAttributeCollection(Query<ShopItemAttributeColumns, ShopItemAttribute> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ShopItemAttributeCollection(Database db, Query<ShopItemAttributeColumns, ShopItemAttribute> q, bool load) : base(db, q, load) { }
		public ShopItemAttributeCollection(Query<ShopItemAttributeColumns, ShopItemAttribute> q, bool load) : base(q, load) { }
    }
}