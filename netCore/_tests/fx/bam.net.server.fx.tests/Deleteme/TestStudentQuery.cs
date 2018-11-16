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
    public class TestStudentQuery: Query<TestStudentColumns, TestStudent>
    { 
		public TestStudentQuery(){}
		public TestStudentQuery(WhereDelegate<TestStudentColumns> where, OrderBy<TestStudentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TestStudentQuery(Func<TestStudentColumns, QueryFilter<TestStudentColumns>> where, OrderBy<TestStudentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TestStudentQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TestStudentQuery Where(WhereDelegate<TestStudentColumns> where)
        {
            return Where(where, null, null);
        }

        public static TestStudentQuery Where(WhereDelegate<TestStudentColumns> where, OrderBy<TestStudentColumns> orderBy = null, Database db = null)
        {
            return new TestStudentQuery(where, orderBy, db);
        }

		public TestStudentCollection Execute()
		{
			return new TestStudentCollection(this, true);
		}
    }
}