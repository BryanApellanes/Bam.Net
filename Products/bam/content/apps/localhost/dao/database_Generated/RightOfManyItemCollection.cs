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
    public class RightOfManyItemCollection: DaoCollection<RightOfManyItemColumns, RightOfManyItem>
    { 
		public RightOfManyItemCollection(){}
		public RightOfManyItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RightOfManyItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RightOfManyItemCollection(Query<RightOfManyItemColumns, RightOfManyItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RightOfManyItemCollection(Database db, Query<RightOfManyItemColumns, RightOfManyItem> q, bool load) : base(db, q, load) { }
		public RightOfManyItemCollection(Query<RightOfManyItemColumns, RightOfManyItem> q, bool load) : base(q, load) { }
    }
}