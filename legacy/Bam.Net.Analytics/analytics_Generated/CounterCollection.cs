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
    public class CounterCollection: DaoCollection<CounterColumns, Counter>
    { 
		public CounterCollection(){}
		public CounterCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CounterCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CounterCollection(Query<CounterColumns, Counter> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CounterCollection(Database db, Query<CounterColumns, Counter> q, bool load) : base(db, q, load) { }
		public CounterCollection(Query<CounterColumns, Counter> q, bool load) : base(q, load) { }
    }
}