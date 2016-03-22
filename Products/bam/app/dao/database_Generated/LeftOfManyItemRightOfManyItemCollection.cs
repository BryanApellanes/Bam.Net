/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class LeftOfManyItemRightOfManyItemCollection: DaoCollection<LeftOfManyItemRightOfManyItemColumns, LeftOfManyItemRightOfManyItem>
    { 
		public LeftOfManyItemRightOfManyItemCollection(){}
		public LeftOfManyItemRightOfManyItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LeftOfManyItemRightOfManyItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LeftOfManyItemRightOfManyItemCollection(Query<LeftOfManyItemRightOfManyItemColumns, LeftOfManyItemRightOfManyItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LeftOfManyItemRightOfManyItemCollection(Database db, Query<LeftOfManyItemRightOfManyItemColumns, LeftOfManyItemRightOfManyItem> q, bool load) : base(db, q, load) { }
		public LeftOfManyItemRightOfManyItemCollection(Query<LeftOfManyItemRightOfManyItemColumns, LeftOfManyItemRightOfManyItem> q, bool load) : base(q, load) { }
    }
}