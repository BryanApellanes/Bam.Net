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
    public class DaoBaseItemCollection: DaoCollection<DaoBaseItemColumns, DaoBaseItem>
    { 
		public DaoBaseItemCollection(){}
		public DaoBaseItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DaoBaseItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DaoBaseItemCollection(Query<DaoBaseItemColumns, DaoBaseItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DaoBaseItemCollection(Database db, Query<DaoBaseItemColumns, DaoBaseItem> q, bool load) : base(db, q, load) { }
		public DaoBaseItemCollection(Query<DaoBaseItemColumns, DaoBaseItem> q, bool load) : base(q, load) { }
    }
}