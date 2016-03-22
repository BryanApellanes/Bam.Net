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
    public class CommonSubItemCollection: DaoCollection<CommonSubItemColumns, CommonSubItem>
    { 
		public CommonSubItemCollection(){}
		public CommonSubItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CommonSubItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CommonSubItemCollection(Query<CommonSubItemColumns, CommonSubItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CommonSubItemCollection(Database db, Query<CommonSubItemColumns, CommonSubItem> q, bool load) : base(db, q, load) { }
		public CommonSubItemCollection(Query<CommonSubItemColumns, CommonSubItem> q, bool load) : base(q, load) { }
    }
}