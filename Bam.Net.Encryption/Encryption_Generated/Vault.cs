/*
	This file was generated and should not be modified directly
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Encryption
{
	// schema = Encryption
	// connection Name = Encryption
	[Serializable]
	[Bam.Net.Data.Table("Vault", "Encryption")]
	public partial class Vault: Dao
	{
		public Vault():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Vault(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Vault(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Vault(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Vault(DataRow data)
		{
			return new Vault(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("VaultItem_VaultId", new VaultItemCollection(Database.GetQuery<VaultItemColumns, VaultItem>((c) => c.VaultId == GetLongValue("Id")), this, "VaultId"));	
            this.ChildCollections.Add("VaultKey_VaultId", new VaultKeyCollection(Database.GetQuery<VaultKeyColumns, VaultKey>((c) => c.VaultId == GetLongValue("Id")), this, "VaultId"));							
		}

	// property:Id, columnName:Id	
	[Bam.Net.Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="19")]
	public long? Id
	{
		get
		{
			return GetLongValue("Id");
		}
		set
		{
			SetValue("Id", value);
		}
	}

	// property:Uuid, columnName:Uuid	
	[Bam.Net.Data.Column(Name="Uuid", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Uuid
	{
		get
		{
			return GetStringValue("Uuid");
		}
		set
		{
			SetValue("Uuid", value);
		}
	}

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}



				

	[Exclude]	
	public VaultItemCollection VaultItemsByVaultId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("VaultItem_VaultId"))
			{
				SetChildren();
			}

			var c = (VaultItemCollection)this.ChildCollections["VaultItem_VaultId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public VaultKeyCollection VaultKeysByVaultId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("VaultKey_VaultId"))
			{
				SetChildren();
			}

			var c = (VaultKeyCollection)this.ChildCollections["VaultKey_VaultId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance's.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new VaultColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Vault table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static VaultCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Vault>();
			Database db = database ?? Db.For<Vault>();
			var results = new VaultCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<VaultCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				VaultColumns columns = new VaultColumns();
				var orderBy = Order.By<VaultColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<VaultCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<VaultColumns> where, Func<VaultCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				VaultColumns columns = new VaultColumns();
				var orderBy = Order.By<VaultColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (VaultColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Vault GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Vault GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Vault GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Vault GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static VaultCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static VaultCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<VaultColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a VaultColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultCollection Where(Func<VaultColumns, QueryFilter<VaultColumns>> where, OrderBy<VaultColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Vault>();
			return new VaultCollection(database.GetQuery<VaultColumns, Vault>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static VaultCollection Where(WhereDelegate<VaultColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Vault>();
			var results = new VaultCollection(database, database.GetQuery<VaultColumns, Vault>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static VaultCollection Where(WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Vault>();
			var results = new VaultCollection(database, database.GetQuery<VaultColumns, Vault>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;VaultColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static VaultCollection Where(QiQuery where, Database database = null)
		{
			var results = new VaultCollection(database, Select<VaultColumns>.From<Vault>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Vault GetOneWhere(QueryFilter where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				result = CreateFromFilter(where, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Vault OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<VaultColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Vault GetOneWhere(WhereDelegate<VaultColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				VaultColumns c = new VaultColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Vault instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Vault OneWhere(WhereDelegate<VaultColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<VaultColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Vault OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Vault FirstOneWhere(WhereDelegate<VaultColumns> where, Database database = null)
		{
			var results = Top(1, where, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}
		
		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Vault FirstOneWhere(WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy, Database database = null)
		{
			var results = Top(1, where, orderBy, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Shortcut for Top(1, where, orderBy, database)
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Vault FirstOneWhere(QueryFilter where, OrderBy<VaultColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<VaultColumns> whereDelegate = (c) => where;
			var results = Top(1, whereDelegate, orderBy, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static VaultCollection Top(int count, WhereDelegate<VaultColumns> where, Database database = null)
		{
			return Top(count, where, null, database);
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static VaultCollection Top(int count, WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy, Database database = null)
		{
			VaultColumns c = new VaultColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Vault>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Vault>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<VaultColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<VaultCollection>(0);
			results.Database = db;
			return results;
		}

		public static VaultCollection Top(int count, QueryFilter where, Database database)
		{
			return Top(count, where, null, database);
		}
		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A QueryFilter used to filter the 
		/// results
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static VaultCollection Top(int count, QueryFilter where, OrderBy<VaultColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Vault>();
			QuerySet query = GetQuerySet(db);
			query.Top<Vault>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<VaultColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<VaultCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A QueryFilter used to filter the 
		/// results
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static VaultCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Vault>();
			QuerySet query = GetQuerySet(db);
			query.Top<Vault>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<VaultCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a VaultColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between VaultColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<VaultColumns> where, Database database = null)
		{
			VaultColumns c = new VaultColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Vault>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Vault>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Vault CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Vault>();			
			var dao = new Vault();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Vault OneOrThrow(VaultCollection c)
		{
			if(c.Count == 1)
			{
				return c[0];
			}
			else if(c.Count > 1)
			{
				throw new MultipleEntriesFoundException();
			}

			return null;
		}

	}
}																								
