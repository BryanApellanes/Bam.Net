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
    public class TestStudentPagedQuery: PagedQuery<TestStudentColumns, TestStudent>
    { 
		public TestStudentPagedQuery(TestStudentColumns orderByColumn, TestStudentQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}