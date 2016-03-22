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
    public class LeftOfManyItemCollection: DaoCollection<LeftOfManyItemColumns, LeftOfManyItem>
    { 
		public LeftOfManyItemCollection(){}
		public LeftOfManyItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LeftOfManyItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LeftOfManyItemCollection(Query<LeftOfManyItemColumns, LeftOfManyItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LeftOfManyItemCollection(Database db, Query<LeftOfManyItemColumns, LeftOfManyItem> q, bool load) : base(db, q, load) { }
		public LeftOfManyItemCollection(Query<LeftOfManyItemColumns, LeftOfManyItem> q, bool load) : base(q, load) { }
    }
}