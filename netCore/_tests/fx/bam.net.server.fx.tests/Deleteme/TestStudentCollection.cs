/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Server.Tests.Dao
{
    public class TestStudentCollection: DaoCollection<TestStudentColumns, TestStudent>
    { 
		public TestStudentCollection(){}
		public TestStudentCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TestStudentCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestStudentCollection(Query<TestStudentColumns, TestStudent> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestStudentCollection(Database db, Query<TestStudentColumns, TestStudent> q, bool load) : base(db, q, load) { }
		public TestStudentCollection(Query<TestStudentColumns, TestStudent> q, bool load) : base(q, load) { }
    }
}