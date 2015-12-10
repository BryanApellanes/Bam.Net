/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Instructions
{
    public class StepCollection: DaoCollection<StepColumns, Step>
    { 
		public StepCollection(){}
		public StepCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public StepCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public StepCollection(Query<StepColumns, Step> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public StepCollection(Database db, Query<StepColumns, Step> q, bool load) : base(db, q, load) { }
		public StepCollection(Query<StepColumns, Step> q, bool load) : base(q, load) { }
    }
}