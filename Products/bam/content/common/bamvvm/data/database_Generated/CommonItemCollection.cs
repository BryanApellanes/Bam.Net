/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bryan.Common.Data
{
    public class CommonItemCollection: DaoCollection<CommonItemColumns, CommonItem>
    { 
		public CommonItemCollection(){}
		public CommonItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CommonItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CommonItemCollection(Query<CommonItemColumns, CommonItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CommonItemCollection(Database db, Query<CommonItemColumns, CommonItem> q, bool load) : base(db, q, load) { }
		public CommonItemCollection(Query<CommonItemColumns, CommonItem> q, bool load) : base(q, load) { }
    }
}