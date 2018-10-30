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

namespace Bam.Net.Data.Repositories.Tests
{
	// schema = RepoTests 
    public static class RepoTestsContext
    {
		public static string ConnectionName
		{
			get
			{
				return "RepoTests";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class MainObjectQueryContext
	{
			public MainObjectCollection Where(WhereDelegate<MainObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.MainObject.Where(where, db);
			}
		   
			public MainObjectCollection Where(WhereDelegate<MainObjectColumns> where, OrderBy<MainObjectColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.MainObject.Where(where, orderBy, db);
			}

			public MainObject OneWhere(WhereDelegate<MainObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.MainObject.OneWhere(where, db);
			}

			public static MainObject GetOneWhere(WhereDelegate<MainObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.MainObject.GetOneWhere(where, db);
			}
		
			public MainObject FirstOneWhere(WhereDelegate<MainObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.MainObject.FirstOneWhere(where, db);
			}

			public MainObjectCollection Top(int count, WhereDelegate<MainObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.MainObject.Top(count, where, db);
			}

			public MainObjectCollection Top(int count, WhereDelegate<MainObjectColumns> where, OrderBy<MainObjectColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.MainObject.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MainObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.MainObject.Count(where, db);
			}
	}

	static MainObjectQueryContext _mainObjects;
	static object _mainObjectsLock = new object();
	public static MainObjectQueryContext MainObjects
	{
		get
		{
			return _mainObjectsLock.DoubleCheckLock<MainObjectQueryContext>(ref _mainObjects, () => new MainObjectQueryContext());
		}
	}
	public class SecondaryObjectQueryContext
	{
			public SecondaryObjectCollection Where(WhereDelegate<SecondaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObject.Where(where, db);
			}
		   
			public SecondaryObjectCollection Where(WhereDelegate<SecondaryObjectColumns> where, OrderBy<SecondaryObjectColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObject.Where(where, orderBy, db);
			}

			public SecondaryObject OneWhere(WhereDelegate<SecondaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObject.OneWhere(where, db);
			}

			public static SecondaryObject GetOneWhere(WhereDelegate<SecondaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObject.GetOneWhere(where, db);
			}
		
			public SecondaryObject FirstOneWhere(WhereDelegate<SecondaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObject.FirstOneWhere(where, db);
			}

			public SecondaryObjectCollection Top(int count, WhereDelegate<SecondaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObject.Top(count, where, db);
			}

			public SecondaryObjectCollection Top(int count, WhereDelegate<SecondaryObjectColumns> where, OrderBy<SecondaryObjectColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObject.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SecondaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObject.Count(where, db);
			}
	}

	static SecondaryObjectQueryContext _secondaryObjects;
	static object _secondaryObjectsLock = new object();
	public static SecondaryObjectQueryContext SecondaryObjects
	{
		get
		{
			return _secondaryObjectsLock.DoubleCheckLock<SecondaryObjectQueryContext>(ref _secondaryObjects, () => new SecondaryObjectQueryContext());
		}
	}
	public class TernaryObjectQueryContext
	{
			public TernaryObjectCollection Where(WhereDelegate<TernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.TernaryObject.Where(where, db);
			}
		   
			public TernaryObjectCollection Where(WhereDelegate<TernaryObjectColumns> where, OrderBy<TernaryObjectColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.TernaryObject.Where(where, orderBy, db);
			}

			public TernaryObject OneWhere(WhereDelegate<TernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.TernaryObject.OneWhere(where, db);
			}

			public static TernaryObject GetOneWhere(WhereDelegate<TernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.TernaryObject.GetOneWhere(where, db);
			}
		
			public TernaryObject FirstOneWhere(WhereDelegate<TernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.TernaryObject.FirstOneWhere(where, db);
			}

			public TernaryObjectCollection Top(int count, WhereDelegate<TernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.TernaryObject.Top(count, where, db);
			}

			public TernaryObjectCollection Top(int count, WhereDelegate<TernaryObjectColumns> where, OrderBy<TernaryObjectColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.TernaryObject.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.TernaryObject.Count(where, db);
			}
	}

	static TernaryObjectQueryContext _ternaryObjects;
	static object _ternaryObjectsLock = new object();
	public static TernaryObjectQueryContext TernaryObjects
	{
		get
		{
			return _ternaryObjectsLock.DoubleCheckLock<TernaryObjectQueryContext>(ref _ternaryObjects, () => new TernaryObjectQueryContext());
		}
	}
	public class SecondaryObjectTernaryObjectQueryContext
	{
			public SecondaryObjectTernaryObjectCollection Where(WhereDelegate<SecondaryObjectTernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObjectTernaryObject.Where(where, db);
			}
		   
			public SecondaryObjectTernaryObjectCollection Where(WhereDelegate<SecondaryObjectTernaryObjectColumns> where, OrderBy<SecondaryObjectTernaryObjectColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObjectTernaryObject.Where(where, orderBy, db);
			}

			public SecondaryObjectTernaryObject OneWhere(WhereDelegate<SecondaryObjectTernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObjectTernaryObject.OneWhere(where, db);
			}

			public static SecondaryObjectTernaryObject GetOneWhere(WhereDelegate<SecondaryObjectTernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObjectTernaryObject.GetOneWhere(where, db);
			}
		
			public SecondaryObjectTernaryObject FirstOneWhere(WhereDelegate<SecondaryObjectTernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObjectTernaryObject.FirstOneWhere(where, db);
			}

			public SecondaryObjectTernaryObjectCollection Top(int count, WhereDelegate<SecondaryObjectTernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObjectTernaryObject.Top(count, where, db);
			}

			public SecondaryObjectTernaryObjectCollection Top(int count, WhereDelegate<SecondaryObjectTernaryObjectColumns> where, OrderBy<SecondaryObjectTernaryObjectColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObjectTernaryObject.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SecondaryObjectTernaryObjectColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.SecondaryObjectTernaryObject.Count(where, db);
			}
	}

	static SecondaryObjectTernaryObjectQueryContext _secondaryObjectTernaryObjects;
	static object _secondaryObjectTernaryObjectsLock = new object();
	public static SecondaryObjectTernaryObjectQueryContext SecondaryObjectTernaryObjects
	{
		get
		{
			return _secondaryObjectTernaryObjectsLock.DoubleCheckLock<SecondaryObjectTernaryObjectQueryContext>(ref _secondaryObjectTernaryObjects, () => new SecondaryObjectTernaryObjectQueryContext());
		}
	}    }
}																								
