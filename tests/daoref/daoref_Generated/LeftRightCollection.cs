using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class LeftRightCollection: DaoCollection<LeftRightColumns, LeftRight>
    { 
		public LeftRightCollection(){}
		public LeftRightCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LeftRightCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LeftRightCollection(Query<LeftRightColumns, LeftRight> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LeftRightCollection(Database db, Query<LeftRightColumns, LeftRight> q, bool load) : base(db, q, load) { }
		public LeftRightCollection(Query<LeftRightColumns, LeftRight> q, bool load) : base(q, load) { }
    }
}