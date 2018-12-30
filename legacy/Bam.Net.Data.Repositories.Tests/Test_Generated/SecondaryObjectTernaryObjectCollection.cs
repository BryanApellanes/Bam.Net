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
    public class SecondaryObjectTernaryObjectCollection: DaoCollection<SecondaryObjectTernaryObjectColumns, SecondaryObjectTernaryObject>
    { 
		public SecondaryObjectTernaryObjectCollection(){}
		public SecondaryObjectTernaryObjectCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SecondaryObjectTernaryObjectCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SecondaryObjectTernaryObjectCollection(Query<SecondaryObjectTernaryObjectColumns, SecondaryObjectTernaryObject> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SecondaryObjectTernaryObjectCollection(Database db, Query<SecondaryObjectTernaryObjectColumns, SecondaryObjectTernaryObject> q, bool load) : base(db, q, load) { }
		public SecondaryObjectTernaryObjectCollection(Query<SecondaryObjectTernaryObjectColumns, SecondaryObjectTernaryObject> q, bool load) : base(q, load) { }
    }
}