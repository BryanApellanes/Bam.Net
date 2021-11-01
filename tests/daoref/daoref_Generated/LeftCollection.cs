using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class LeftCollection: DaoCollection<LeftColumns, Left>
    { 
		public LeftCollection(){}
		public LeftCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LeftCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LeftCollection(Query<LeftColumns, Left> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LeftCollection(Database db, Query<LeftColumns, Left> q, bool load) : base(db, q, load) { }
		public LeftCollection(Query<LeftColumns, Left> q, bool load) : base(q, load) { }
    }
}