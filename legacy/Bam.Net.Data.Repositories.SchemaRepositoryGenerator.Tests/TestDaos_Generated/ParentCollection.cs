/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
{
    public class ParentCollection: DaoCollection<ParentColumns, Parent>
    { 
		public ParentCollection(){}
		public ParentCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ParentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ParentCollection(Query<ParentColumns, Parent> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ParentCollection(Database db, Query<ParentColumns, Parent> q, bool load) : base(db, q, load) { }
		public ParentCollection(Query<ParentColumns, Parent> q, bool load) : base(q, load) { }
    }
}