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

namespace Bam.Net.Services.Data
{
	// schema = KeyValueStore 
    public static class KeyValueStoreContext
    {
		public static string ConnectionName
		{
			get
			{
				return "KeyValueStore";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class KeyValuePairQueryContext
	{
			public KeyValuePairCollection Where(WhereDelegate<KeyValuePairColumns> where, Database db = null)
			{
				return Bam.Net.Services.Data.KeyValuePair.Where(where, db);
			}
		   
			public KeyValuePairCollection Where(WhereDelegate<KeyValuePairColumns> where, OrderBy<KeyValuePairColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.Data.KeyValuePair.Where(where, orderBy, db);
			}

			public KeyValuePair OneWhere(WhereDelegate<KeyValuePairColumns> where, Database db = null)
			{
				return Bam.Net.Services.Data.KeyValuePair.OneWhere(where, db);
			}

			public static KeyValuePair GetOneWhere(WhereDelegate<KeyValuePairColumns> where, Database db = null)
			{
				return Bam.Net.Services.Data.KeyValuePair.GetOneWhere(where, db);
			}
		
			public KeyValuePair FirstOneWhere(WhereDelegate<KeyValuePairColumns> where, Database db = null)
			{
				return Bam.Net.Services.Data.KeyValuePair.FirstOneWhere(where, db);
			}

			public KeyValuePairCollection Top(int count, WhereDelegate<KeyValuePairColumns> where, Database db = null)
			{
				return Bam.Net.Services.Data.KeyValuePair.Top(count, where, db);
			}

			public KeyValuePairCollection Top(int count, WhereDelegate<KeyValuePairColumns> where, OrderBy<KeyValuePairColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.Data.KeyValuePair.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<KeyValuePairColumns> where, Database db = null)
			{
				return Bam.Net.Services.Data.KeyValuePair.Count(where, db);
			}
	}

	static KeyValuePairQueryContext _keyValuePairs;
	static object _keyValuePairsLock = new object();
	public static KeyValuePairQueryContext KeyValuePairs
	{
		get
		{
			return _keyValuePairsLock.DoubleCheckLock<KeyValuePairQueryContext>(ref _keyValuePairs, () => new KeyValuePairQueryContext());
		}
	}    }
}																								
