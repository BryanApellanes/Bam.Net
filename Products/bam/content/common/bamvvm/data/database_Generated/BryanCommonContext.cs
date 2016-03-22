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

namespace Bryan.Common.Data
{
	// schema = BryanCommon 
    public static class BryanCommonContext
    {
		public static string ConnectionName
		{
			get
			{
				return "BryanCommon";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class CommonItemQueryContext
	{
			public CommonItemCollection Where(WhereDelegate<CommonItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonItem.Where(where, db);
			}
		   
			public CommonItemCollection Where(WhereDelegate<CommonItemColumns> where, OrderBy<CommonItemColumns> orderBy = null, Database db = null)
			{
				return Bryan.Common.Data.CommonItem.Where(where, orderBy, db);
			}

			public CommonItem OneWhere(WhereDelegate<CommonItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonItem.OneWhere(where, db);
			}
		
			public CommonItem FirstOneWhere(WhereDelegate<CommonItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonItem.FirstOneWhere(where, db);
			}

			public CommonItemCollection Top(int count, WhereDelegate<CommonItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonItem.Top(count, where, db);
			}

			public CommonItemCollection Top(int count, WhereDelegate<CommonItemColumns> where, OrderBy<CommonItemColumns> orderBy, Database db = null)
			{
				return Bryan.Common.Data.CommonItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CommonItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonItem.Count(where, db);
			}
	}

	static CommonItemQueryContext _commonItems;
	static object _commonItemsLock = new object();
	public static CommonItemQueryContext CommonItems
	{
		get
		{
			return _commonItemsLock.DoubleCheckLock<CommonItemQueryContext>(ref _commonItems, () => new CommonItemQueryContext());
		}
	}
	public class CommonSubItemQueryContext
	{
			public CommonSubItemCollection Where(WhereDelegate<CommonSubItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonSubItem.Where(where, db);
			}
		   
			public CommonSubItemCollection Where(WhereDelegate<CommonSubItemColumns> where, OrderBy<CommonSubItemColumns> orderBy = null, Database db = null)
			{
				return Bryan.Common.Data.CommonSubItem.Where(where, orderBy, db);
			}

			public CommonSubItem OneWhere(WhereDelegate<CommonSubItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonSubItem.OneWhere(where, db);
			}
		
			public CommonSubItem FirstOneWhere(WhereDelegate<CommonSubItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonSubItem.FirstOneWhere(where, db);
			}

			public CommonSubItemCollection Top(int count, WhereDelegate<CommonSubItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonSubItem.Top(count, where, db);
			}

			public CommonSubItemCollection Top(int count, WhereDelegate<CommonSubItemColumns> where, OrderBy<CommonSubItemColumns> orderBy, Database db = null)
			{
				return Bryan.Common.Data.CommonSubItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CommonSubItemColumns> where, Database db = null)
			{
				return Bryan.Common.Data.CommonSubItem.Count(where, db);
			}
	}

	static CommonSubItemQueryContext _commonSubItems;
	static object _commonSubItemsLock = new object();
	public static CommonSubItemQueryContext CommonSubItems
	{
		get
		{
			return _commonSubItemsLock.DoubleCheckLock<CommonSubItemQueryContext>(ref _commonSubItems, () => new CommonSubItemQueryContext());
		}
	}    }
}																								
