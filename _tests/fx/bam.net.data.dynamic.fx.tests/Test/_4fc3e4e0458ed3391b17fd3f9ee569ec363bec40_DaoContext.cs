/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Test.DataBanana.Dao
{
	// schema = _4fc3e4e0458ed3391b17fd3f9ee569ec363bec40_Dao 
    public static class _4fc3e4e0458ed3391b17fd3f9ee569ec363bec40_DaoContext
    {
		public static string ConnectionName
		{
			get
			{
				return "_4fc3e4e0458ed3391b17fd3f9ee569ec363bec40_Dao";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class TestClassQueryContext
	{
			public TestClassCollection Where(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Test.DataBanana.Dao.TestClass.Where(where, db);
			}
		   
			public TestClassCollection Where(WhereDelegate<TestClassColumns> where, OrderBy<TestClassColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Test.DataBanana.Dao.TestClass.Where(where, orderBy, db);
			}

			public TestClass OneWhere(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Test.DataBanana.Dao.TestClass.OneWhere(where, db);
			}

			public static TestClass GetOneWhere(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Test.DataBanana.Dao.TestClass.GetOneWhere(where, db);
			}
		
			public TestClass FirstOneWhere(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Test.DataBanana.Dao.TestClass.FirstOneWhere(where, db);
			}

			public TestClassCollection Top(int count, WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Test.DataBanana.Dao.TestClass.Top(count, where, db);
			}

			public TestClassCollection Top(int count, WhereDelegate<TestClassColumns> where, OrderBy<TestClassColumns> orderBy, Database db = null)
			{
				return Bam.Net.Test.DataBanana.Dao.TestClass.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Test.DataBanana.Dao.TestClass.Count(where, db);
			}
	}

	static TestClassQueryContext _testClasses;
	static object _testClassesLock = new object();
	public static TestClassQueryContext TestClasses
	{
		get
		{
			return _testClassesLock.DoubleCheckLock<TestClassQueryContext>(ref _testClasses, () => new TestClassQueryContext());
		}
	}    }
}																								
