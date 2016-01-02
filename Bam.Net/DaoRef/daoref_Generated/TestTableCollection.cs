/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class TestTableCollection: DaoCollection<TestTableColumns, TestTable>
    { 
		public TestTableCollection(){}
		public TestTableCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TestTableCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestTableCollection(Query<TestTableColumns, TestTable> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestTableCollection(Database db, Query<TestTableColumns, TestTable> q, bool load) : base(db, q, load) { }
		public TestTableCollection(Query<TestTableColumns, TestTable> q, bool load) : base(q, load) { }
    }
}