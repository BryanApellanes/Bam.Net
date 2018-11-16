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

namespace Bam.Net.Server.Tests.Dao
{
	// schema = _b78ecae83f4076d455c3057554d9fb44737b24b0_Dao 
    public static class _b78ecae83f4076d455c3057554d9fb44737b24b0_DaoContext
    {
		public static string ConnectionName
		{
			get
			{
				return "_b78ecae83f4076d455c3057554d9fb44737b24b0_Dao";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class TestStudentQueryContext
	{
			public TestStudentCollection Where(WhereDelegate<TestStudentColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestStudent.Where(where, db);
			}
		   
			public TestStudentCollection Where(WhereDelegate<TestStudentColumns> where, OrderBy<TestStudentColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestStudent.Where(where, orderBy, db);
			}

			public TestStudent OneWhere(WhereDelegate<TestStudentColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestStudent.OneWhere(where, db);
			}

			public static TestStudent GetOneWhere(WhereDelegate<TestStudentColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestStudent.GetOneWhere(where, db);
			}
		
			public TestStudent FirstOneWhere(WhereDelegate<TestStudentColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestStudent.FirstOneWhere(where, db);
			}

			public TestStudentCollection Top(int count, WhereDelegate<TestStudentColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestStudent.Top(count, where, db);
			}

			public TestStudentCollection Top(int count, WhereDelegate<TestStudentColumns> where, OrderBy<TestStudentColumns> orderBy, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestStudent.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestStudentColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestStudent.Count(where, db);
			}
	}

	static TestStudentQueryContext _testStudents;
	static object _testStudentsLock = new object();
	public static TestStudentQueryContext TestStudents
	{
		get
		{
			return _testStudentsLock.DoubleCheckLock<TestStudentQueryContext>(ref _testStudents, () => new TestStudentQueryContext());
		}
	}
	public class TestClassQueryContext
	{
			public TestClassCollection Where(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestClass.Where(where, db);
			}
		   
			public TestClassCollection Where(WhereDelegate<TestClassColumns> where, OrderBy<TestClassColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestClass.Where(where, orderBy, db);
			}

			public TestClass OneWhere(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestClass.OneWhere(where, db);
			}

			public static TestClass GetOneWhere(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestClass.GetOneWhere(where, db);
			}
		
			public TestClass FirstOneWhere(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestClass.FirstOneWhere(where, db);
			}

			public TestClassCollection Top(int count, WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestClass.Top(count, where, db);
			}

			public TestClassCollection Top(int count, WhereDelegate<TestClassColumns> where, OrderBy<TestClassColumns> orderBy, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestClass.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestClassColumns> where, Database db = null)
			{
				return Bam.Net.Server.Tests.Dao.TestClass.Count(where, db);
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
