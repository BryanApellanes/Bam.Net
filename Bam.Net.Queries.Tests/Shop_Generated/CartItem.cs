/*
	This file was generated and should not be modified directly
*/
// Model is Table
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Data.Tests
{
	// schema = Shop
	// connection Name = Shop
	[Serializable]
	[Bam.Net.Data.Table("CartItem", "Shop")]
	public partial class CartItem: Bam.Net.Data.Dao
	{
		public CartItem():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CartItem(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CartItem(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CartItem(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator CartItem(DataRow data)
		{
			return new CartItem(data);
		}

		private void SetChildren()
		{
						
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

	// property:Cuid, columnName:Cuid	
	[Bam.Net.Data.Column(Name="Cuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Cuid
	{
		get
		{
			return GetStringValue("Cuid");
		}
		set
		{
			SetValue("Cuid", value);
		}
	}

	// property:Quantity, columnName:Quantity	
	[Bam.Net.Data.Column(Name="Quantity", DbDataType="Int", MaxLength="10", AllowNull=false)]
	public int? Quantity
	{
		get
		{
			return GetIntValue("Quantity");
		}
		set
		{
			SetValue("Quantity", value);
		}
	}



	// start CartId -> CartId
	[Bam.Net.Data.ForeignKey(
        Table="CartItem",
		Name="CartId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Cart",
		Suffix="1")]
	public long? CartId
	{
		get
		{
			return GetLongValue("CartId");
		}
		set
		{
			SetValue("CartId", value);
		}
	}

	Cart _cartOfCartId;
	public Cart CartOfCartId
	{
		get
		{
			if(_cartOfCartId == null)
			{
				_cartOfCartId = Bam.Net.Data.Tests.Cart.OneWhere(c => c.KeyColumn == this.CartId, this.Database);
			}
			return _cartOfCartId;
		}
	}
	
	// start ItemId -> ItemId
	[Bam.Net.Data.ForeignKey(
        Table="CartItem",
		Name="ItemId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Item",
		Suffix="2")]
	public long? ItemId
	{
		get
		{
			return GetLongValue("ItemId");
		}
		set
		{
			SetValue("ItemId", value);
		}
	}

	Item _itemOfItemId;
	public Item ItemOfItemId
	{
		get
		{
			if(_itemOfItemId == null)
			{
				_itemOfItemId = Bam.Net.Data.Tests.Item.OneWhere(c => c.KeyColumn == this.ItemId, this.Database);
			}
			return _itemOfItemId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance's.
		/// </summary>
		[Bam.Net.Exclude] 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new CartItemColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the CartItem table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CartItemCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<CartItem>();
			Database db = database ?? Db.For<CartItem>();
			var results = new CartItemCollection(db, sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<CartItem>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				CartItemColumns columns = new CartItemColumns();
				var orderBy = Bam.Net.Data.Order.By<CartItemColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<CartItem>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<CartItemColumns> where, Action<IEnumerable<CartItem>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				CartItemColumns columns = new CartItemColumns();
				var orderBy = Bam.Net.Data.Order.By<CartItemColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (CartItemColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<CartItem>> batchProcessor, Bam.Net.Data.OrderBy<CartItemColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<CartItemColumns> where, Action<IEnumerable<CartItem>> batchProcessor, Bam.Net.Data.OrderBy<CartItemColumns> orderBy, Database database = null)
		{
			await Task.Run(async ()=>
			{
				CartItemColumns columns = new CartItemColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (CartItemColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static CartItem GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static CartItem GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static CartItem GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static CartItem GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static CartItemCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static CartItemCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<CartItemColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CartItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CartItemCollection Where(Func<CartItemColumns, QueryFilter<CartItemColumns>> where, OrderBy<CartItemColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<CartItem>();
			return new CartItemCollection(database.GetQuery<CartItemColumns, CartItem>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CartItemCollection Where(WhereDelegate<CartItemColumns> where, Database database = null)
		{		
			database = database ?? Db.For<CartItem>();
			var results = new CartItemCollection(database, database.GetQuery<CartItemColumns, CartItem>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CartItemCollection Where(WhereDelegate<CartItemColumns> where, OrderBy<CartItemColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<CartItem>();
			var results = new CartItemCollection(database, database.GetQuery<CartItemColumns, CartItem>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;CartItemColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CartItemCollection Where(QiQuery where, Database database = null)
		{
			var results = new CartItemCollection(database, Select<CartItemColumns>.From<CartItem>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static CartItem GetOneWhere(QueryFilter where, Database database = null)
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
		[Bam.Net.Exclude]
		public static CartItem OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<CartItemColumns> whereDelegate = (c) => where;
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
		[Bam.Net.Exclude]
		public static CartItem GetOneWhere(WhereDelegate<CartItemColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				CartItemColumns c = new CartItemColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single CartItem instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CartItem OneWhere(WhereDelegate<CartItemColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CartItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CartItem OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CartItem FirstOneWhere(WhereDelegate<CartItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CartItem FirstOneWhere(WhereDelegate<CartItemColumns> where, OrderBy<CartItemColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CartItem FirstOneWhere(QueryFilter where, OrderBy<CartItemColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<CartItemColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CartItemCollection Top(int count, WhereDelegate<CartItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static CartItemCollection Top(int count, WhereDelegate<CartItemColumns> where, OrderBy<CartItemColumns> orderBy, Database database = null)
		{
			CartItemColumns c = new CartItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<CartItem>();
			QuerySet query = GetQuerySet(db); 
			query.Top<CartItem>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CartItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CartItemCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static CartItemCollection Top(int count, QueryFilter where, Database database)
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
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static CartItemCollection Top(int count, QueryFilter where, OrderBy<CartItemColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<CartItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<CartItem>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<CartItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CartItemCollection>(0);
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
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static CartItemCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<CartItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<CartItem>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<CartItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of CartItems
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<CartItem>();
            QuerySet query = GetQuerySet(db);
            query.Count<CartItem>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<CartItemColumns> where, Database database = null)
		{
			CartItemColumns c = new CartItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<CartItem>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<CartItem>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<CartItem>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<CartItem>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static CartItem CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<CartItem>();			
			var dao = new CartItem();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static CartItem OneOrThrow(CartItemCollection c)
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
