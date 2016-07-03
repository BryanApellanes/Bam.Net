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

namespace Bam.Net.Shop
{
	// schema = Shop
	// connection Name = Shop
	[Serializable]
	[Bam.Net.Data.Table("Shop", "Shop")]
	public partial class Shop: Dao
	{
		public Shop():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Shop(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Shop(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Shop(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Shop(DataRow data)
		{
			return new Shop(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("ShopShopItem_ShopId", new ShopShopItemCollection(Database.GetQuery<ShopShopItemColumns, ShopShopItem>((c) => c.ShopId == GetLongValue("Id")), this, "ShopId"));	
            this.ChildCollections.Add("ShopPromotion_ShopId", new ShopPromotionCollection(Database.GetQuery<ShopPromotionColumns, ShopPromotion>((c) => c.ShopId == GetLongValue("Id")), this, "ShopId"));				
            this.ChildCollections.Add("Shop_ShopShopItem_ShopItem",  new XrefDaoCollection<ShopShopItem, ShopItem>(this, false));
				
            this.ChildCollections.Add("Shop_ShopPromotion_Promotion",  new XrefDaoCollection<ShopPromotion, Promotion>(this, false));
							
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
	public ShopShopItemCollection ShopShopItemsByShopId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ShopShopItem_ShopId"))
			{
				SetChildren();
			}

			var c = (ShopShopItemCollection)this.ChildCollections["ShopShopItem_ShopId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public ShopPromotionCollection ShopPromotionsByShopId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ShopPromotion_ShopId"))
			{
				SetChildren();
			}

			var c = (ShopPromotionCollection)this.ChildCollections["ShopPromotion_ShopId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<ShopShopItem, ShopItem> ShopItems
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Shop_ShopShopItem_ShopItem"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ShopShopItem, ShopItem>)this.ChildCollections["Shop_ShopShopItem_ShopItem"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<ShopPromotion, Promotion> Promotions
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Shop_ShopPromotion_Promotion"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ShopPromotion, Promotion>)this.ChildCollections["Shop_ShopPromotion_Promotion"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
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
				var colFilter = new ShopColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Shop table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ShopCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Shop>();
			Database db = database ?? Db.For<Shop>();
			var results = new ShopCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<ShopCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ShopColumns columns = new ShopColumns();
				var orderBy = Order.By<ShopColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<ShopCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<ShopColumns> where, Func<ShopCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ShopColumns columns = new ShopColumns();
				var orderBy = Order.By<ShopColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ShopColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Shop GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Shop GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Shop GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Shop GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static ShopCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static ShopCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ShopColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ShopColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ShopCollection Where(Func<ShopColumns, QueryFilter<ShopColumns>> where, OrderBy<ShopColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Shop>();
			return new ShopCollection(database.GetQuery<ShopColumns, Shop>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ShopCollection Where(WhereDelegate<ShopColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Shop>();
			var results = new ShopCollection(database, database.GetQuery<ShopColumns, Shop>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static ShopCollection Where(WhereDelegate<ShopColumns> where, OrderBy<ShopColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Shop>();
			var results = new ShopCollection(database, database.GetQuery<ShopColumns, Shop>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ShopColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ShopCollection Where(QiQuery where, Database database = null)
		{
			var results = new ShopCollection(database, Select<ShopColumns>.From<Shop>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Shop GetOneWhere(QueryFilter where, Database database = null)
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
		public static Shop OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ShopColumns> whereDelegate = (c) => where;
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
		public static Shop GetOneWhere(WhereDelegate<ShopColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ShopColumns c = new ShopColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Shop instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Shop OneWhere(WhereDelegate<ShopColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ShopColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Shop OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Shop FirstOneWhere(WhereDelegate<ShopColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ShopColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Shop FirstOneWhere(WhereDelegate<ShopColumns> where, OrderBy<ShopColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ShopColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Shop FirstOneWhere(QueryFilter where, OrderBy<ShopColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ShopColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ShopColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static ShopCollection Top(int count, WhereDelegate<ShopColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ShopColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static ShopCollection Top(int count, WhereDelegate<ShopColumns> where, OrderBy<ShopColumns> orderBy, Database database = null)
		{
			ShopColumns c = new ShopColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Shop>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Shop>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ShopColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ShopCollection>(0);
			results.Database = db;
			return results;
		}

		public static ShopCollection Top(int count, QueryFilter where, Database database)
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
		public static ShopCollection Top(int count, QueryFilter where, OrderBy<ShopColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Shop>();
			QuerySet query = GetQuerySet(db);
			query.Top<Shop>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ShopColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ShopCollection>(0);
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
		public static ShopCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Shop>();
			QuerySet query = GetQuerySet(db);
			query.Top<Shop>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ShopCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ShopColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ShopColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ShopColumns> where, Database database = null)
		{
			ShopColumns c = new ShopColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Shop>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Shop>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Shop CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Shop>();			
			var dao = new Shop();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Shop OneOrThrow(ShopCollection c)
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
