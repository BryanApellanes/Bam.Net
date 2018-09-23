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
    public class LoadCounterCollection: DaoCollection<LoadCounterColumns, LoadCounter>
    { 
		public LoadCounterCollection(){}
		public LoadCounterCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LoadCounterCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LoadCounterCollection(Query<LoadCounterColumns, LoadCounter> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LoadCounterCollection(Database db, Query<LoadCounterColumns, LoadCounter> q, bool load) : base(db, q, load) { }
		public LoadCounterCollection(Query<LoadCounterColumns, LoadCounter> q, bool load) : base(q, load) { }
    }
}