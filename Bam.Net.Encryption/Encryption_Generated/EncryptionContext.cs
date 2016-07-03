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

namespace Bam.Net.Encryption
{
	// schema = Encryption 
    public static class EncryptionContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Encryption";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class VaultQueryContext
	{
			public VaultCollection Where(WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.Vault.Where(where, db);
			}
		   
			public VaultCollection Where(WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Encryption.Vault.Where(where, orderBy, db);
			}

			public Vault OneWhere(WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.Vault.OneWhere(where, db);
			}

			public static Vault GetOneWhere(WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.Vault.GetOneWhere(where, db);
			}
		
			public Vault FirstOneWhere(WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.Vault.FirstOneWhere(where, db);
			}

			public VaultCollection Top(int count, WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.Vault.Top(count, where, db);
			}

			public VaultCollection Top(int count, WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy, Database db = null)
			{
				return Bam.Net.Encryption.Vault.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<VaultColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.Vault.Count(where, db);
			}
	}

	static VaultQueryContext _vaults;
	static object _vaultsLock = new object();
	public static VaultQueryContext Vaults
	{
		get
		{
			return _vaultsLock.DoubleCheckLock<VaultQueryContext>(ref _vaults, () => new VaultQueryContext());
		}
	}
	public class VaultItemQueryContext
	{
			public VaultItemCollection Where(WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultItem.Where(where, db);
			}
		   
			public VaultItemCollection Where(WhereDelegate<VaultItemColumns> where, OrderBy<VaultItemColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Encryption.VaultItem.Where(where, orderBy, db);
			}

			public VaultItem OneWhere(WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultItem.OneWhere(where, db);
			}

			public static VaultItem GetOneWhere(WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultItem.GetOneWhere(where, db);
			}
		
			public VaultItem FirstOneWhere(WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultItem.FirstOneWhere(where, db);
			}

			public VaultItemCollection Top(int count, WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultItem.Top(count, where, db);
			}

			public VaultItemCollection Top(int count, WhereDelegate<VaultItemColumns> where, OrderBy<VaultItemColumns> orderBy, Database db = null)
			{
				return Bam.Net.Encryption.VaultItem.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<VaultItemColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultItem.Count(where, db);
			}
	}

	static VaultItemQueryContext _vaultItems;
	static object _vaultItemsLock = new object();
	public static VaultItemQueryContext VaultItems
	{
		get
		{
			return _vaultItemsLock.DoubleCheckLock<VaultItemQueryContext>(ref _vaultItems, () => new VaultItemQueryContext());
		}
	}
	public class VaultKeyQueryContext
	{
			public VaultKeyCollection Where(WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultKey.Where(where, db);
			}
		   
			public VaultKeyCollection Where(WhereDelegate<VaultKeyColumns> where, OrderBy<VaultKeyColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Encryption.VaultKey.Where(where, orderBy, db);
			}

			public VaultKey OneWhere(WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultKey.OneWhere(where, db);
			}

			public static VaultKey GetOneWhere(WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultKey.GetOneWhere(where, db);
			}
		
			public VaultKey FirstOneWhere(WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultKey.FirstOneWhere(where, db);
			}

			public VaultKeyCollection Top(int count, WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultKey.Top(count, where, db);
			}

			public VaultKeyCollection Top(int count, WhereDelegate<VaultKeyColumns> where, OrderBy<VaultKeyColumns> orderBy, Database db = null)
			{
				return Bam.Net.Encryption.VaultKey.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<VaultKeyColumns> where, Database db = null)
			{
				return Bam.Net.Encryption.VaultKey.Count(where, db);
			}
	}

	static VaultKeyQueryContext _vaultKeys;
	static object _vaultKeysLock = new object();
	public static VaultKeyQueryContext VaultKeys
	{
		get
		{
			return _vaultKeysLock.DoubleCheckLock<VaultKeyQueryContext>(ref _vaultKeys, () => new VaultKeyQueryContext());
		}
	}    }
}																								
