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
	[Bam.Net.Data.Table("Price", "Shop")]
	public partial class Price: Dao
	{
		public Price():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Price(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Price(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Price(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Price(DataRow data)
		{
			return new Price(data);
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

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="Decimal", MaxLength="28", AllowNull=false)]
	public decimal? Value
	{
		get
		{
			return GetDecimalValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



	// start ShopItemId -> ShopItemId
	[Bam.Net.Data.ForeignKey(
        Table="Price",
		Name="ShopItemId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="ShopItem",
		Suffix="1")]
	public long? ShopItemId
	{
		get
		{
			return GetLongValue("ShopItemId");
		}
		set
		{
			SetValue("ShopItemId", value);
		}
	}

	ShopItem _shopItemOfShopItemId;
	public ShopItem ShopItemOfShopItemId
	{
		get
		{
			if(_shopItemOfShopItemId == null)
			{
				_shopItemOfShopItemId = Bam.Net.Shop.ShopItem.OneWhere(c => c.KeyColumn == this.ShopItemId, this.Database);
			}
			return _shopItemOfShopItemId;
		}
	}
	
	// start CurrencyId -> CurrencyId
	[Bam.Net.Data.ForeignKey(
        Table="Price",
		Name="CurrencyId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Currency",
		Suffix="2")]
	public long? CurrencyId
	{
		get
		{
			return GetLongValue("CurrencyId");
		}
		set
		{
			SetValue("CurrencyId", value);
		}
	}

	Currency _currencyOfCurrencyId;
	public Currency CurrencyOfCurrencyId
	{
		get
		{
			if(_currencyOfCurrencyId == null)
			{
				_currencyOfCurrencyId = Bam.Net.Shop.Currency.OneWhere(c => c.KeyColumn == this.CurrencyId, this.Database);
			}
			return _currencyOfCurrencyId;
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
				var colFilter = new PriceColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Price table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PriceCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Price>();
			Database db = database ?? Db.For<Price>();
			var results = new PriceCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<PriceCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				PriceColumns columns = new PriceColumns();
				var orderBy = Order.By<PriceColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<PriceCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<PriceColumns> where, Func<PriceCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				PriceColumns columns = new PriceColumns();
				var orderBy = Order.By<PriceColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (PriceColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Price GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Price GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Price GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Price GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static PriceCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static PriceCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<PriceColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PriceColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PriceCollection Where(Func<PriceColumns, QueryFilter<PriceColumns>> where, OrderBy<PriceColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Price>();
			return new PriceCollection(database.GetQuery<PriceColumns, Price>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PriceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PriceCollection Where(WhereDelegate<PriceColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Price>();
			var results = new PriceCollection(database, database.GetQuery<PriceColumns, Price>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PriceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static PriceCollection Where(WhereDelegate<PriceColumns> where, OrderBy<PriceColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Price>();
			var results = new PriceCollection(database, database.GetQuery<PriceColumns, Price>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;PriceColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static PriceCollection Where(QiQuery where, Database database = null)
		{
			var results = new PriceCollection(database, Select<PriceColumns>.From<Price>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Price GetOneWhere(QueryFilter where, Database database = null)
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
		public static Price OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<PriceColumns> whereDelegate = (c) => where;
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
		public static Price GetOneWhere(WhereDelegate<PriceColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				PriceColumns c = new PriceColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Price instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PriceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Price OneWhere(WhereDelegate<PriceColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PriceColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Price OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PriceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Price FirstOneWhere(WhereDelegate<PriceColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PriceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Price FirstOneWhere(WhereDelegate<PriceColumns> where, OrderBy<PriceColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PriceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Price FirstOneWhere(QueryFilter where, OrderBy<PriceColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<PriceColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a PriceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static PriceCollection Top(int count, WhereDelegate<PriceColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PriceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static PriceCollection Top(int count, WhereDelegate<PriceColumns> where, OrderBy<PriceColumns> orderBy, Database database = null)
		{
			PriceColumns c = new PriceColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Price>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Price>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PriceColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PriceCollection>(0);
			results.Database = db;
			return results;
		}

		public static PriceCollection Top(int count, QueryFilter where, Database database)
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
		public static PriceCollection Top(int count, QueryFilter where, OrderBy<PriceColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Price>();
			QuerySet query = GetQuerySet(db);
			query.Top<Price>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<PriceColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PriceCollection>(0);
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
		public static PriceCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Price>();
			QuerySet query = GetQuerySet(db);
			query.Top<Price>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<PriceCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PriceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PriceColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PriceColumns> where, Database database = null)
		{
			PriceColumns c = new PriceColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Price>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Price>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Price CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Price>();			
			var dao = new Price();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Price OneOrThrow(PriceCollection c)
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
