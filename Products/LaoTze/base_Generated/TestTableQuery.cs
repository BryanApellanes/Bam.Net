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
    public class TestTableQuery: Query<TestTableColumns, TestTable>
    { 
		public TestTableQuery(){}
		public TestTableQuery(WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TestTableQuery(Func<TestTableColumns, QueryFilter<TestTableColumns>> where, OrderBy<TestTableColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TestTableQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TestTableQuery Where(WhereDelegate<TestTableColumns> where)
        {
            return Where(where, null, null);
        }

        public static TestTableQuery Where(WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy = null, Database db = null)
        {
            return new TestTableQuery(where, orderBy, db);
        }

		public TestTableCollection Execute()
		{
			return new TestTableCollection(this, true);
		}
    }
}