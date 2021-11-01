using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class TestFkTableCollection: DaoCollection<TestFkTableColumns, TestFkTable>
    { 
		public TestFkTableCollection(){}
		public TestFkTableCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TestFkTableCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestFkTableCollection(Query<TestFkTableColumns, TestFkTable> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestFkTableCollection(Database db, Query<TestFkTableColumns, TestFkTable> q, bool load) : base(db, q, load) { }
		public TestFkTableCollection(Query<TestFkTableColumns, TestFkTable> q, bool load) : base(q, load) { }
    }
}