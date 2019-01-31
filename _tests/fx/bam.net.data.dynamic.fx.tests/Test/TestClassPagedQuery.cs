/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Test.DataBanana.Dao
{
    public class TestClassPagedQuery: PagedQuery<TestClassColumns, TestClass>
    { 
		public TestClassPagedQuery(TestClassColumns orderByColumn, TestClassQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}