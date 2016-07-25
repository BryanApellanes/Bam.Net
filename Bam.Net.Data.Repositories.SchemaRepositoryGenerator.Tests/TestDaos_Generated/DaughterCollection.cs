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
    public class DaughterCollection: DaoCollection<DaughterColumns, Daughter>
    { 
		public DaughterCollection(){}
		public DaughterCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DaughterCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DaughterCollection(Query<DaughterColumns, Daughter> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DaughterCollection(Database db, Query<DaughterColumns, Daughter> q, bool load) : base(db, q, load) { }
		public DaughterCollection(Query<DaughterColumns, Daughter> q, bool load) : base(q, load) { }
    }
}