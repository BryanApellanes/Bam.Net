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

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
	// schema = TestDaoSchema 
    public static class TestDaoSchemaContext
    {
		public static string ConnectionName
		{
			get
			{
				return "TestDaoSchema";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class DaughterDaoQueryContext
	{
			public DaughterDaoCollection Where(WhereDelegate<DaughterDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.DaughterDao.Where(where, db);
			}
		   
			public DaughterDaoCollection Where(WhereDelegate<DaughterDaoColumns> where, OrderBy<DaughterDaoColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.DaughterDao.Where(where, orderBy, db);
			}

			public DaughterDao OneWhere(WhereDelegate<DaughterDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.DaughterDao.OneWhere(where, db);
			}

			public static DaughterDao GetOneWhere(WhereDelegate<DaughterDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.DaughterDao.GetOneWhere(where, db);
			}
		
			public DaughterDao FirstOneWhere(WhereDelegate<DaughterDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.DaughterDao.FirstOneWhere(where, db);
			}

			public DaughterDaoCollection Top(int count, WhereDelegate<DaughterDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.DaughterDao.Top(count, where, db);
			}

			public DaughterDaoCollection Top(int count, WhereDelegate<DaughterDaoColumns> where, OrderBy<DaughterDaoColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.DaughterDao.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DaughterDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.DaughterDao.Count(where, db);
			}
	}

	static DaughterDaoQueryContext _daughterDaos;
	static object _daughterDaosLock = new object();
	public static DaughterDaoQueryContext DaughterDaos
	{
		get
		{
			return _daughterDaosLock.DoubleCheckLock<DaughterDaoQueryContext>(ref _daughterDaos, () => new DaughterDaoQueryContext());
		}
	}
	public class HouseDaoQueryContext
	{
			public HouseDaoCollection Where(WhereDelegate<HouseDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDao.Where(where, db);
			}
		   
			public HouseDaoCollection Where(WhereDelegate<HouseDaoColumns> where, OrderBy<HouseDaoColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDao.Where(where, orderBy, db);
			}

			public HouseDao OneWhere(WhereDelegate<HouseDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDao.OneWhere(where, db);
			}

			public static HouseDao GetOneWhere(WhereDelegate<HouseDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDao.GetOneWhere(where, db);
			}
		
			public HouseDao FirstOneWhere(WhereDelegate<HouseDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDao.FirstOneWhere(where, db);
			}

			public HouseDaoCollection Top(int count, WhereDelegate<HouseDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDao.Top(count, where, db);
			}

			public HouseDaoCollection Top(int count, WhereDelegate<HouseDaoColumns> where, OrderBy<HouseDaoColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDao.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HouseDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDao.Count(where, db);
			}
	}

	static HouseDaoQueryContext _houseDaos;
	static object _houseDaosLock = new object();
	public static HouseDaoQueryContext HouseDaos
	{
		get
		{
			return _houseDaosLock.DoubleCheckLock<HouseDaoQueryContext>(ref _houseDaos, () => new HouseDaoQueryContext());
		}
	}
	public class ParentDaoQueryContext
	{
			public ParentDaoCollection Where(WhereDelegate<ParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.Where(where, db);
			}
		   
			public ParentDaoCollection Where(WhereDelegate<ParentDaoColumns> where, OrderBy<ParentDaoColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.Where(where, orderBy, db);
			}

			public ParentDao OneWhere(WhereDelegate<ParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.OneWhere(where, db);
			}

			public static ParentDao GetOneWhere(WhereDelegate<ParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.GetOneWhere(where, db);
			}
		
			public ParentDao FirstOneWhere(WhereDelegate<ParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.FirstOneWhere(where, db);
			}

			public ParentDaoCollection Top(int count, WhereDelegate<ParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.Top(count, where, db);
			}

			public ParentDaoCollection Top(int count, WhereDelegate<ParentDaoColumns> where, OrderBy<ParentDaoColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.Count(where, db);
			}
	}

	static ParentDaoQueryContext _parentDaos;
	static object _parentDaosLock = new object();
	public static ParentDaoQueryContext ParentDaos
	{
		get
		{
			return _parentDaosLock.DoubleCheckLock<ParentDaoQueryContext>(ref _parentDaos, () => new ParentDaoQueryContext());
		}
	}
	public class SonDaoQueryContext
	{
			public SonDaoCollection Where(WhereDelegate<SonDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.SonDao.Where(where, db);
			}
		   
			public SonDaoCollection Where(WhereDelegate<SonDaoColumns> where, OrderBy<SonDaoColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.SonDao.Where(where, orderBy, db);
			}

			public SonDao OneWhere(WhereDelegate<SonDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.SonDao.OneWhere(where, db);
			}

			public static SonDao GetOneWhere(WhereDelegate<SonDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.SonDao.GetOneWhere(where, db);
			}
		
			public SonDao FirstOneWhere(WhereDelegate<SonDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.SonDao.FirstOneWhere(where, db);
			}

			public SonDaoCollection Top(int count, WhereDelegate<SonDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.SonDao.Top(count, where, db);
			}

			public SonDaoCollection Top(int count, WhereDelegate<SonDaoColumns> where, OrderBy<SonDaoColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.SonDao.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SonDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.SonDao.Count(where, db);
			}
	}

	static SonDaoQueryContext _sonDaos;
	static object _sonDaosLock = new object();
	public static SonDaoQueryContext SonDaos
	{
		get
		{
			return _sonDaosLock.DoubleCheckLock<SonDaoQueryContext>(ref _sonDaos, () => new SonDaoQueryContext());
		}
	}
	public class HouseDaoParentDaoQueryContext
	{
			public HouseDaoParentDaoCollection Where(WhereDelegate<HouseDaoParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.Where(where, db);
			}
		   
			public HouseDaoParentDaoCollection Where(WhereDelegate<HouseDaoParentDaoColumns> where, OrderBy<HouseDaoParentDaoColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.Where(where, orderBy, db);
			}

			public HouseDaoParentDao OneWhere(WhereDelegate<HouseDaoParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.OneWhere(where, db);
			}

			public static HouseDaoParentDao GetOneWhere(WhereDelegate<HouseDaoParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.GetOneWhere(where, db);
			}
		
			public HouseDaoParentDao FirstOneWhere(WhereDelegate<HouseDaoParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.FirstOneWhere(where, db);
			}

			public HouseDaoParentDaoCollection Top(int count, WhereDelegate<HouseDaoParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.Top(count, where, db);
			}

			public HouseDaoParentDaoCollection Top(int count, WhereDelegate<HouseDaoParentDaoColumns> where, OrderBy<HouseDaoParentDaoColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HouseDaoParentDaoColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.Count(where, db);
			}
	}

	static HouseDaoParentDaoQueryContext _houseDaoParentDaos;
	static object _houseDaoParentDaosLock = new object();
	public static HouseDaoParentDaoQueryContext HouseDaoParentDaos
	{
		get
		{
			return _houseDaoParentDaosLock.DoubleCheckLock<HouseDaoParentDaoQueryContext>(ref _houseDaoParentDaos, () => new HouseDaoParentDaoQueryContext());
		}
	}    }
}																								
