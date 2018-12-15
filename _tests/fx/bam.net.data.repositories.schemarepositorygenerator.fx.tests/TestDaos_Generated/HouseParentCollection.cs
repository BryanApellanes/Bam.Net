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
    public class HouseParentCollection: DaoCollection<HouseParentColumns, HouseParent>
    { 
		public HouseParentCollection(){}
		public HouseParentCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public HouseParentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HouseParentCollection(Query<HouseParentColumns, HouseParent> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HouseParentCollection(Database db, Query<HouseParentColumns, HouseParent> q, bool load) : base(db, q, load) { }
		public HouseParentCollection(Query<HouseParentColumns, HouseParent> q, bool load) : base(q, load) { }
    }
}