/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests
{
    public class TernaryObjectCollection: DaoCollection<TernaryObjectColumns, TernaryObject>
    { 
		public TernaryObjectCollection(){}
		public TernaryObjectCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TernaryObjectCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TernaryObjectCollection(Query<TernaryObjectColumns, TernaryObject> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TernaryObjectCollection(Database db, Query<TernaryObjectColumns, TernaryObject> q, bool load) : base(db, q, load) { }
		public TernaryObjectCollection(Query<TernaryObjectColumns, TernaryObject> q, bool load) : base(q, load) { }
    }
}