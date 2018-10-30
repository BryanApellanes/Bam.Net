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

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
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


	public class DaughterQueryContext
	{
			public DaughterCollection Where(WhereDelegate<DaughterColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.Where(where, db);
			}
		   
			public DaughterCollection Where(WhereDelegate<DaughterColumns> where, OrderBy<DaughterColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.Where(where, orderBy, db);
			}

			public Daughter OneWhere(WhereDelegate<DaughterColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.OneWhere(where, db);
			}

			public static Daughter GetOneWhere(WhereDelegate<DaughterColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.GetOneWhere(where, db);
			}
		
			public Daughter FirstOneWhere(WhereDelegate<DaughterColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.FirstOneWhere(where, db);
			}

			public DaughterCollection Top(int count, WhereDelegate<DaughterColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.Top(count, where, db);
			}

			public DaughterCollection Top(int count, WhereDelegate<DaughterColumns> where, OrderBy<DaughterColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DaughterColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.Count(where, db);
			}
	}

	static DaughterQueryContext _daughters;
	static object _daughtersLock = new object();
	public static DaughterQueryContext Daughters
	{
		get
		{
			return _daughtersLock.DoubleCheckLock<DaughterQueryContext>(ref _daughters, () => new DaughterQueryContext());
		}
	}
	public class HouseQueryContext
	{
			public HouseCollection Where(WhereDelegate<HouseColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.Where(where, db);
			}
		   
			public HouseCollection Where(WhereDelegate<HouseColumns> where, OrderBy<HouseColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.Where(where, orderBy, db);
			}

			public House OneWhere(WhereDelegate<HouseColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.OneWhere(where, db);
			}

			public static House GetOneWhere(WhereDelegate<HouseColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.GetOneWhere(where, db);
			}
		
			public House FirstOneWhere(WhereDelegate<HouseColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.FirstOneWhere(where, db);
			}

			public HouseCollection Top(int count, WhereDelegate<HouseColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.Top(count, where, db);
			}

			public HouseCollection Top(int count, WhereDelegate<HouseColumns> where, OrderBy<HouseColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HouseColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.Count(where, db);
			}
	}

	static HouseQueryContext _houses;
	static object _housesLock = new object();
	public static HouseQueryContext Houses
	{
		get
		{
			return _housesLock.DoubleCheckLock<HouseQueryContext>(ref _houses, () => new HouseQueryContext());
		}
	}
	public class ParentQueryContext
	{
			public ParentCollection Where(WhereDelegate<ParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.Where(where, db);
			}
		   
			public ParentCollection Where(WhereDelegate<ParentColumns> where, OrderBy<ParentColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.Where(where, orderBy, db);
			}

			public Parent OneWhere(WhereDelegate<ParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.OneWhere(where, db);
			}

			public static Parent GetOneWhere(WhereDelegate<ParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.GetOneWhere(where, db);
			}
		
			public Parent FirstOneWhere(WhereDelegate<ParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.FirstOneWhere(where, db);
			}

			public ParentCollection Top(int count, WhereDelegate<ParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.Top(count, where, db);
			}

			public ParentCollection Top(int count, WhereDelegate<ParentColumns> where, OrderBy<ParentColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.Count(where, db);
			}
	}

	static ParentQueryContext _parents;
	static object _parentsLock = new object();
	public static ParentQueryContext Parents
	{
		get
		{
			return _parentsLock.DoubleCheckLock<ParentQueryContext>(ref _parents, () => new ParentQueryContext());
		}
	}
	public class SonQueryContext
	{
			public SonCollection Where(WhereDelegate<SonColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.Where(where, db);
			}
		   
			public SonCollection Where(WhereDelegate<SonColumns> where, OrderBy<SonColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.Where(where, orderBy, db);
			}

			public Son OneWhere(WhereDelegate<SonColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.OneWhere(where, db);
			}

			public static Son GetOneWhere(WhereDelegate<SonColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.GetOneWhere(where, db);
			}
		
			public Son FirstOneWhere(WhereDelegate<SonColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.FirstOneWhere(where, db);
			}

			public SonCollection Top(int count, WhereDelegate<SonColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.Top(count, where, db);
			}

			public SonCollection Top(int count, WhereDelegate<SonColumns> where, OrderBy<SonColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SonColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.Count(where, db);
			}
	}

	static SonQueryContext _sons;
	static object _sonsLock = new object();
	public static SonQueryContext Sons
	{
		get
		{
			return _sonsLock.DoubleCheckLock<SonQueryContext>(ref _sons, () => new SonQueryContext());
		}
	}
	public class HouseParentQueryContext
	{
			public HouseParentCollection Where(WhereDelegate<HouseParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.HouseParent.Where(where, db);
			}
		   
			public HouseParentCollection Where(WhereDelegate<HouseParentColumns> where, OrderBy<HouseParentColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.HouseParent.Where(where, orderBy, db);
			}

			public HouseParent OneWhere(WhereDelegate<HouseParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.HouseParent.OneWhere(where, db);
			}

			public static HouseParent GetOneWhere(WhereDelegate<HouseParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.HouseParent.GetOneWhere(where, db);
			}
		
			public HouseParent FirstOneWhere(WhereDelegate<HouseParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.HouseParent.FirstOneWhere(where, db);
			}

			public HouseParentCollection Top(int count, WhereDelegate<HouseParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.HouseParent.Top(count, where, db);
			}

			public HouseParentCollection Top(int count, WhereDelegate<HouseParentColumns> where, OrderBy<HouseParentColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.HouseParent.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HouseParentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.HouseParent.Count(where, db);
			}
	}

	static HouseParentQueryContext _houseParents;
	static object _houseParentsLock = new object();
	public static HouseParentQueryContext HouseParents
	{
		get
		{
			return _houseParentsLock.DoubleCheckLock<HouseParentQueryContext>(ref _houseParents, () => new HouseParentQueryContext());
		}
	}    }
}																								
