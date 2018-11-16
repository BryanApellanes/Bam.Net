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
    public class TestClassCollection: DaoCollection<TestClassColumns, TestClass>
    { 
		public TestClassCollection(){}
		public TestClassCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TestClassCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestClassCollection(Query<TestClassColumns, TestClass> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestClassCollection(Database db, Query<TestClassColumns, TestClass> q, bool load) : base(db, q, load) { }
		public TestClassCollection(Query<TestClassColumns, TestClass> q, bool load) : base(q, load) { }
    }
}