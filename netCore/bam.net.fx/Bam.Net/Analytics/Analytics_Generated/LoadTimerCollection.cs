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
    public class LoadTimerCollection: DaoCollection<LoadTimerColumns, LoadTimer>
    { 
		public LoadTimerCollection(){}
		public LoadTimerCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LoadTimerCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LoadTimerCollection(Query<LoadTimerColumns, LoadTimer> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LoadTimerCollection(Database db, Query<LoadTimerColumns, LoadTimer> q, bool load) : base(db, q, load) { }
		public LoadTimerCollection(Query<LoadTimerColumns, LoadTimer> q, bool load) : base(q, load) { }
    }
}