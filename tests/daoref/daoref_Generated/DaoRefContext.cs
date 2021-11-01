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

namespace Bam.Net.DaoRef
{
	// schema = DaoRef
    public static class DaoRefContext
    {
		public static string ConnectionName
		{
			get
			{
				return "DaoRef";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class LeftQueryContext
	{
			public LeftCollection Where(WhereDelegate<LeftColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Left.Where(where, db);
			}
		   
			public LeftCollection Where(WhereDelegate<LeftColumns> where, OrderBy<LeftColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.DaoRef.Left.Where(where, orderBy, db);
			}

			public Left OneWhere(WhereDelegate<LeftColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Left.OneWhere(where, db);
			}

			public static Left GetOneWhere(WhereDelegate<LeftColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Left.GetOneWhere(where, db);
			}
		
			public Left FirstOneWhere(WhereDelegate<LeftColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Left.FirstOneWhere(where, db);
			}

			public LeftCollection Top(int count, WhereDelegate<LeftColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Left.Top(count, where, db);
			}

			public LeftCollection Top(int count, WhereDelegate<LeftColumns> where, OrderBy<LeftColumns> orderBy, Database db = null)
			{
				return Bam.Net.DaoRef.Left.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LeftColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Left.Count(where, db);
			}
	}

	static LeftQueryContext _lefts;
	static object _leftsLock = new object();
	public static LeftQueryContext Lefts
	{
		get
		{
			return _leftsLock.DoubleCheckLock<LeftQueryContext>(ref _lefts, () => new LeftQueryContext());
		}
	}
	public class RightQueryContext
	{
			public RightCollection Where(WhereDelegate<RightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Right.Where(where, db);
			}
		   
			public RightCollection Where(WhereDelegate<RightColumns> where, OrderBy<RightColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.DaoRef.Right.Where(where, orderBy, db);
			}

			public Right OneWhere(WhereDelegate<RightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Right.OneWhere(where, db);
			}

			public static Right GetOneWhere(WhereDelegate<RightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Right.GetOneWhere(where, db);
			}
		
			public Right FirstOneWhere(WhereDelegate<RightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Right.FirstOneWhere(where, db);
			}

			public RightCollection Top(int count, WhereDelegate<RightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Right.Top(count, where, db);
			}

			public RightCollection Top(int count, WhereDelegate<RightColumns> where, OrderBy<RightColumns> orderBy, Database db = null)
			{
				return Bam.Net.DaoRef.Right.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.Right.Count(where, db);
			}
	}

	static RightQueryContext _rights;
	static object _rightsLock = new object();
	public static RightQueryContext Rights
	{
		get
		{
			return _rightsLock.DoubleCheckLock<RightQueryContext>(ref _rights, () => new RightQueryContext());
		}
	}
	public class TestTableQueryContext
	{
			public TestTableCollection Where(WhereDelegate<TestTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestTable.Where(where, db);
			}
		   
			public TestTableCollection Where(WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.DaoRef.TestTable.Where(where, orderBy, db);
			}

			public TestTable OneWhere(WhereDelegate<TestTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestTable.OneWhere(where, db);
			}

			public static TestTable GetOneWhere(WhereDelegate<TestTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestTable.GetOneWhere(where, db);
			}
		
			public TestTable FirstOneWhere(WhereDelegate<TestTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestTable.FirstOneWhere(where, db);
			}

			public TestTableCollection Top(int count, WhereDelegate<TestTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestTable.Top(count, where, db);
			}

			public TestTableCollection Top(int count, WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy, Database db = null)
			{
				return Bam.Net.DaoRef.TestTable.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestTable.Count(where, db);
			}
	}

	static TestTableQueryContext _testTables;
	static object _testTablesLock = new object();
	public static TestTableQueryContext TestTables
	{
		get
		{
			return _testTablesLock.DoubleCheckLock<TestTableQueryContext>(ref _testTables, () => new TestTableQueryContext());
		}
	}
	public class TestFkTableQueryContext
	{
			public TestFkTableCollection Where(WhereDelegate<TestFkTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestFkTable.Where(where, db);
			}
		   
			public TestFkTableCollection Where(WhereDelegate<TestFkTableColumns> where, OrderBy<TestFkTableColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.DaoRef.TestFkTable.Where(where, orderBy, db);
			}

			public TestFkTable OneWhere(WhereDelegate<TestFkTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestFkTable.OneWhere(where, db);
			}

			public static TestFkTable GetOneWhere(WhereDelegate<TestFkTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestFkTable.GetOneWhere(where, db);
			}
		
			public TestFkTable FirstOneWhere(WhereDelegate<TestFkTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestFkTable.FirstOneWhere(where, db);
			}

			public TestFkTableCollection Top(int count, WhereDelegate<TestFkTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestFkTable.Top(count, where, db);
			}

			public TestFkTableCollection Top(int count, WhereDelegate<TestFkTableColumns> where, OrderBy<TestFkTableColumns> orderBy, Database db = null)
			{
				return Bam.Net.DaoRef.TestFkTable.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TestFkTableColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.TestFkTable.Count(where, db);
			}
	}

	static TestFkTableQueryContext _testFkTables;
	static object _testFkTablesLock = new object();
	public static TestFkTableQueryContext TestFkTables
	{
		get
		{
			return _testFkTablesLock.DoubleCheckLock<TestFkTableQueryContext>(ref _testFkTables, () => new TestFkTableQueryContext());
		}
	}
	public class DaoReferenceObjectQueryContext
	{
			public DaoReferenceObjectCollection Where(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObject.Where(where, db);
			}
		   
			public DaoReferenceObjectCollection Where(WhereDelegate<DaoReferenceObjectColumns> where, OrderBy<DaoReferenceObjectColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObject.Where(where, orderBy, db);
			}

			public DaoReferenceObject OneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObject.OneWhere(where, db);
			}

			public static DaoReferenceObject GetOneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObject.GetOneWhere(where, db);
			}
		
			public DaoReferenceObject FirstOneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObject.FirstOneWhere(where, db);
			}

			public DaoReferenceObjectCollection Top(int count, WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObject.Top(count, where, db);
			}

			public DaoReferenceObjectCollection Top(int count, WhereDelegate<DaoReferenceObjectColumns> where, OrderBy<DaoReferenceObjectColumns> orderBy, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObject.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObject.Count(where, db);
			}
	}

	static DaoReferenceObjectQueryContext _daoReferenceObjects;
	static object _daoReferenceObjectsLock = new object();
	public static DaoReferenceObjectQueryContext DaoReferenceObjects
	{
		get
		{
			return _daoReferenceObjectsLock.DoubleCheckLock<DaoReferenceObjectQueryContext>(ref _daoReferenceObjects, () => new DaoReferenceObjectQueryContext());
		}
	}
	public class DaoReferenceObjectWithForeignKeyQueryContext
	{
			public DaoReferenceObjectWithForeignKeyCollection Where(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObjectWithForeignKey.Where(where, db);
			}
		   
			public DaoReferenceObjectWithForeignKeyCollection Where(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, OrderBy<DaoReferenceObjectWithForeignKeyColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObjectWithForeignKey.Where(where, orderBy, db);
			}

			public DaoReferenceObjectWithForeignKey OneWhere(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObjectWithForeignKey.OneWhere(where, db);
			}

			public static DaoReferenceObjectWithForeignKey GetOneWhere(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObjectWithForeignKey.GetOneWhere(where, db);
			}
		
			public DaoReferenceObjectWithForeignKey FirstOneWhere(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObjectWithForeignKey.FirstOneWhere(where, db);
			}

			public DaoReferenceObjectWithForeignKeyCollection Top(int count, WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObjectWithForeignKey.Top(count, where, db);
			}

			public DaoReferenceObjectWithForeignKeyCollection Top(int count, WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, OrderBy<DaoReferenceObjectWithForeignKeyColumns> orderBy, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObjectWithForeignKey.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.DaoReferenceObjectWithForeignKey.Count(where, db);
			}
	}

	static DaoReferenceObjectWithForeignKeyQueryContext _daoReferenceObjectWithForeignKeys;
	static object _daoReferenceObjectWithForeignKeysLock = new object();
	public static DaoReferenceObjectWithForeignKeyQueryContext DaoReferenceObjectWithForeignKeys
	{
		get
		{
			return _daoReferenceObjectWithForeignKeysLock.DoubleCheckLock<DaoReferenceObjectWithForeignKeyQueryContext>(ref _daoReferenceObjectWithForeignKeys, () => new DaoReferenceObjectWithForeignKeyQueryContext());
		}
	}
	public class LeftRightQueryContext
	{
			public LeftRightCollection Where(WhereDelegate<LeftRightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.LeftRight.Where(where, db);
			}
		   
			public LeftRightCollection Where(WhereDelegate<LeftRightColumns> where, OrderBy<LeftRightColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.DaoRef.LeftRight.Where(where, orderBy, db);
			}

			public LeftRight OneWhere(WhereDelegate<LeftRightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.LeftRight.OneWhere(where, db);
			}

			public static LeftRight GetOneWhere(WhereDelegate<LeftRightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.LeftRight.GetOneWhere(where, db);
			}
		
			public LeftRight FirstOneWhere(WhereDelegate<LeftRightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.LeftRight.FirstOneWhere(where, db);
			}

			public LeftRightCollection Top(int count, WhereDelegate<LeftRightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.LeftRight.Top(count, where, db);
			}

			public LeftRightCollection Top(int count, WhereDelegate<LeftRightColumns> where, OrderBy<LeftRightColumns> orderBy, Database db = null)
			{
				return Bam.Net.DaoRef.LeftRight.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LeftRightColumns> where, Database db = null)
			{
				return Bam.Net.DaoRef.LeftRight.Count(where, db);
			}
	}

	static LeftRightQueryContext _leftRights;
	static object _leftRightsLock = new object();
	public static LeftRightQueryContext LeftRights
	{
		get
		{
			return _leftRightsLock.DoubleCheckLock<LeftRightQueryContext>(ref _leftRights, () => new LeftRightQueryContext());
		}
	}
    }
}																								
