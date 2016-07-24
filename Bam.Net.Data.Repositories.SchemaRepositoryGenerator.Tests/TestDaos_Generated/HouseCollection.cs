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
    public class HouseCollection: DaoCollection<HouseColumns, House>
    { 
		public HouseCollection(){}
		public HouseCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public HouseCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HouseCollection(Query<HouseColumns, House> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HouseCollection(Database db, Query<HouseColumns, House> q, bool load) : base(db, q, load) { }
		public HouseCollection(Query<HouseColumns, House> q, bool load) : base(q, load) { }
    }
}