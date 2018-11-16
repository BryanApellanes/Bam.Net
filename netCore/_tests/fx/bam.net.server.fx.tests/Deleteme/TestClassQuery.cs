/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Server.Tests.Dao
{
    public class TestClassQuery: Query<TestClassColumns, TestClass>
    { 
		public TestClassQuery(){}
		public TestClassQuery(WhereDelegate<TestClassColumns> where, OrderBy<TestClassColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TestClassQuery(Func<TestClassColumns, QueryFilter<TestClassColumns>> where, OrderBy<TestClassColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TestClassQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TestClassQuery Where(WhereDelegate<TestClassColumns> where)
        {
            return Where(where, null, null);
        }

        public static TestClassQuery Where(WhereDelegate<TestClassColumns> where, OrderBy<TestClassColumns> orderBy = null, Database db = null)
        {
            return new TestClassQuery(where, orderBy, db);
        }

		public TestClassCollection Execute()
		{
			return new TestClassCollection(this, true);
		}
    }
}