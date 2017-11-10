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
    public class CustomTimerCollection: DaoCollection<CustomTimerColumns, CustomTimer>
    { 
		public CustomTimerCollection(){}
		public CustomTimerCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CustomTimerCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CustomTimerCollection(Query<CustomTimerColumns, CustomTimer> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CustomTimerCollection(Database db, Query<CustomTimerColumns, CustomTimer> q, bool load) : base(db, q, load) { }
		public CustomTimerCollection(Query<CustomTimerColumns, CustomTimer> q, bool load) : base(q, load) { }
    }
}