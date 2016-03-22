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

namespace Bam.Net.Data
{
	// schema = DaoTestData 
    public static class DaoTestDataContext
    {
		public static string ConnectionName
		{
			get
			{
				return "DaoTestData";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class DaoBaseItemQueryContext
	{
			public DaoBaseItemCollection Where(WhereDelegate<DaoBaseItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoBaseItem.Where(where, db);
			}
		   
			public DaoBaseItemCollection Where(WhereDelegate<DaoBaseItemColumns> where, OrderBy<DaoBaseItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.DaoBaseItem.Where(where, orderBy, db);
			}

			public DaoBaseItem OneWhere(WhereDelegate<DaoBaseItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoBaseItem.OneWhere(where, db);
			}
		
			public DaoBaseItem FirstOneWhere(WhereDelegate<DaoBaseItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoBaseItem.FirstOneWhere(where, db);
			}

			public DaoBaseItemCollection Top(int count, WhereDelegate<DaoBaseItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoBaseItem.Top(count, where, db);
			}

			public DaoBaseItemCollection Top(int count, WhereDelegate<DaoBaseItemColumns> where, OrderBy<DaoBaseItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.DaoBaseItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DaoBaseItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoBaseItem.Count(where, db);
			}
	}

	static DaoBaseItemQueryContext _daoBaseItems;
	static object _daoBaseItemsLock = new object();
	public static DaoBaseItemQueryContext DaoBaseItems
	{
		get
		{
			return _daoBaseItemsLock.DoubleCheckLock<DaoBaseItemQueryContext>(ref _daoBaseItems, () => new DaoBaseItemQueryContext());
		}
	}
	public class DaoSubItemQueryContext
	{
			public DaoSubItemCollection Where(WhereDelegate<DaoSubItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoSubItem.Where(where, db);
			}
		   
			public DaoSubItemCollection Where(WhereDelegate<DaoSubItemColumns> where, OrderBy<DaoSubItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.DaoSubItem.Where(where, orderBy, db);
			}

			public DaoSubItem OneWhere(WhereDelegate<DaoSubItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoSubItem.OneWhere(where, db);
			}
		
			public DaoSubItem FirstOneWhere(WhereDelegate<DaoSubItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoSubItem.FirstOneWhere(where, db);
			}

			public DaoSubItemCollection Top(int count, WhereDelegate<DaoSubItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoSubItem.Top(count, where, db);
			}

			public DaoSubItemCollection Top(int count, WhereDelegate<DaoSubItemColumns> where, OrderBy<DaoSubItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.DaoSubItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DaoSubItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.DaoSubItem.Count(where, db);
			}
	}

	static DaoSubItemQueryContext _daoSubItems;
	static object _daoSubItemsLock = new object();
	public static DaoSubItemQueryContext DaoSubItems
	{
		get
		{
			return _daoSubItemsLock.DoubleCheckLock<DaoSubItemQueryContext>(ref _daoSubItems, () => new DaoSubItemQueryContext());
		}
	}
	public class LeftOfManyItemQueryContext
	{
			public LeftOfManyItemCollection Where(WhereDelegate<LeftOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItem.Where(where, db);
			}
		   
			public LeftOfManyItemCollection Where(WhereDelegate<LeftOfManyItemColumns> where, OrderBy<LeftOfManyItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItem.Where(where, orderBy, db);
			}

			public LeftOfManyItem OneWhere(WhereDelegate<LeftOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItem.OneWhere(where, db);
			}
		
			public LeftOfManyItem FirstOneWhere(WhereDelegate<LeftOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItem.FirstOneWhere(where, db);
			}

			public LeftOfManyItemCollection Top(int count, WhereDelegate<LeftOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItem.Top(count, where, db);
			}

			public LeftOfManyItemCollection Top(int count, WhereDelegate<LeftOfManyItemColumns> where, OrderBy<LeftOfManyItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LeftOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItem.Count(where, db);
			}
	}

	static LeftOfManyItemQueryContext _leftOfManyItems;
	static object _leftOfManyItemsLock = new object();
	public static LeftOfManyItemQueryContext LeftOfManyItems
	{
		get
		{
			return _leftOfManyItemsLock.DoubleCheckLock<LeftOfManyItemQueryContext>(ref _leftOfManyItems, () => new LeftOfManyItemQueryContext());
		}
	}
	public class RightOfManyItemQueryContext
	{
			public RightOfManyItemCollection Where(WhereDelegate<RightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.RightOfManyItem.Where(where, db);
			}
		   
			public RightOfManyItemCollection Where(WhereDelegate<RightOfManyItemColumns> where, OrderBy<RightOfManyItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.RightOfManyItem.Where(where, orderBy, db);
			}

			public RightOfManyItem OneWhere(WhereDelegate<RightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.RightOfManyItem.OneWhere(where, db);
			}
		
			public RightOfManyItem FirstOneWhere(WhereDelegate<RightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.RightOfManyItem.FirstOneWhere(where, db);
			}

			public RightOfManyItemCollection Top(int count, WhereDelegate<RightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.RightOfManyItem.Top(count, where, db);
			}

			public RightOfManyItemCollection Top(int count, WhereDelegate<RightOfManyItemColumns> where, OrderBy<RightOfManyItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.RightOfManyItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.RightOfManyItem.Count(where, db);
			}
	}

	static RightOfManyItemQueryContext _rightOfManyItems;
	static object _rightOfManyItemsLock = new object();
	public static RightOfManyItemQueryContext RightOfManyItems
	{
		get
		{
			return _rightOfManyItemsLock.DoubleCheckLock<RightOfManyItemQueryContext>(ref _rightOfManyItems, () => new RightOfManyItemQueryContext());
		}
	}
	public class LeftOfManyItemRightOfManyItemQueryContext
	{
			public LeftOfManyItemRightOfManyItemCollection Where(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItemRightOfManyItem.Where(where, db);
			}
		   
			public LeftOfManyItemRightOfManyItemCollection Where(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItemRightOfManyItem.Where(where, orderBy, db);
			}

			public LeftOfManyItemRightOfManyItem OneWhere(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItemRightOfManyItem.OneWhere(where, db);
			}
		
			public LeftOfManyItemRightOfManyItem FirstOneWhere(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItemRightOfManyItem.FirstOneWhere(where, db);
			}

			public LeftOfManyItemRightOfManyItemCollection Top(int count, WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItemRightOfManyItem.Top(count, where, db);
			}

			public LeftOfManyItemRightOfManyItemCollection Top(int count, WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, OrderBy<LeftOfManyItemRightOfManyItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItemRightOfManyItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LeftOfManyItemRightOfManyItemColumns> where, Database db = null)
			{
				return Bam.Net.Data.LeftOfManyItemRightOfManyItem.Count(where, db);
			}
	}

	static LeftOfManyItemRightOfManyItemQueryContext _leftOfManyItemRightOfManyItems;
	static object _leftOfManyItemRightOfManyItemsLock = new object();
	public static LeftOfManyItemRightOfManyItemQueryContext LeftOfManyItemRightOfManyItems
	{
		get
		{
			return _leftOfManyItemRightOfManyItemsLock.DoubleCheckLock<LeftOfManyItemRightOfManyItemQueryContext>(ref _leftOfManyItemRightOfManyItems, () => new LeftOfManyItemRightOfManyItemQueryContext());
		}
	}    }
}																								
