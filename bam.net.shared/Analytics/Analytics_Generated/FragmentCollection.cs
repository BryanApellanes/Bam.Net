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
    public class FragmentCollection: DaoCollection<FragmentColumns, Fragment>
    { 
		public FragmentCollection(){}
		public FragmentCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public FragmentCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public FragmentCollection(Query<FragmentColumns, Fragment> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public FragmentCollection(Database db, Query<FragmentColumns, Fragment> q, bool load) : base(db, q, load) { }
		public FragmentCollection(Query<FragmentColumns, Fragment> q, bool load) : base(q, load) { }
    }
}