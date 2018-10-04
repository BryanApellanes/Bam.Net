/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class MethodCounterCollection: DaoCollection<MethodCounterColumns, MethodCounter>
    { 
		public MethodCounterCollection(){}
		public MethodCounterCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public MethodCounterCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MethodCounterCollection(Query<MethodCounterColumns, MethodCounter> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MethodCounterCollection(Database db, Query<MethodCounterColumns, MethodCounter> q, bool load) : base(db, q, load) { }
		public MethodCounterCollection(Query<MethodCounterColumns, MethodCounter> q, bool load) : base(q, load) { }
    }
}