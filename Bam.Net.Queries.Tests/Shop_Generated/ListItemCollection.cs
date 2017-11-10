/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class ListItemCollection: DaoCollection<ListItemColumns, ListItem>
    { 
		public ListItemCollection(){}
		public ListItemCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ListItemCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ListItemCollection(Query<ListItemColumns, ListItem> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ListItemCollection(Database db, Query<ListItemColumns, ListItem> q, bool load) : base(db, q, load) { }
		public ListItemCollection(Query<ListItemColumns, ListItem> q, bool load) : base(q, load) { }
    }
}