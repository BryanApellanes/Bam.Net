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
    public class SonCollection: DaoCollection<SonColumns, Son>
    { 
		public SonCollection(){}
		public SonCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SonCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SonCollection(Query<SonColumns, Son> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SonCollection(Database db, Query<SonColumns, Son> q, bool load) : base(db, q, load) { }
		public SonCollection(Query<SonColumns, Son> q, bool load) : base(q, load) { }
    }
}