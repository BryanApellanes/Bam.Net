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
    public class DaoSubItemCollection: DaoCollection<DaoSubItemColumns, DaoSubItem>
    { 
		public DaoSubItemCollection(){}
		public DaoSubItemCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DaoSubItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DaoSubItemCollection(Query<DaoSubItemColumns, DaoSubItem> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DaoSubItemCollection(Database db, Query<DaoSubItemColumns, DaoSubItem> q, bool load) : base(db, q, load) { }
		public DaoSubItemCollection(Query<DaoSubItemColumns, DaoSubItem> q, bool load) : base(q, load) { }
    }
}