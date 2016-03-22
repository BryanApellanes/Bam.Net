/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class TestFkTableQuery: Query<TestFkTableColumns, TestFkTable>
    { 
		public TestFkTableQuery(){}
		public TestFkTableQuery(WhereDelegate<TestFkTableColumns> where, OrderBy<TestFkTableColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TestFkTableQuery(Func<TestFkTableColumns, QueryFilter<TestFkTableColumns>> where, OrderBy<TestFkTableColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TestFkTableQuery(Delegate where, Database db = null) : base(where, db) { }

		public TestFkTableCollection Execute()
		{
			return new TestFkTableCollection(this, true);
		}
    }
}