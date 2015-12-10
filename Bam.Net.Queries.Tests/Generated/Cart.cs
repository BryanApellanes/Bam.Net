/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Data.Tests
{
	// schema = Shop
	// connection Name = Shop
	[Bam.Net.Data.Table("Cart", "Shop")]
	public partial class Cart: Dao
	{
		public Cart():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Cart(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Cart(DataRow data)
		{
			return new Cart(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("CartItem_CartId", new CartItemCollection(new Query<CartItemColumns, CartItem>((c) => c.CartId == this.Id), this, "CartId"));							
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="8")]
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



				

	[Exclude]	
	public CartItemCollection CartItemsByCartId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("CartItem_CartId"))
			{
				SetChildren();
			}

			var c = (CartItemCollection)this.ChildCollections["CartItem_CartId"];
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
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new CartColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Cart table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CartCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Cart>();
			Database db = database == null ? Db.For<Cart>(): database;
			var results = new CartCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CartColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CartColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CartCollection Where(Func<CartColumns, QueryFilter<CartColumns>> where, OrderBy<CartColumns> orderBy = null)
		{
			return new CartCollection(new Query<CartColumns, Cart>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CartCollection Where(WhereDelegate<CartColumns> where, Database db = null)
		{
			var results = new CartCollection(db, new Query<CartColumns, Cart>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CartCollection Where(WhereDelegate<CartColumns> where, OrderBy<CartColumns> orderBy = null, Database db = null)
		{
			var results = new CartCollection(db, new Query<CartColumns, Cart>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CartColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CartCollection Where(QiQuery where, Database db = null)
		{
			var results = new CartCollection(db, Select<CartColumns>.From<Cart>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Cart instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Cart OneWhere(WhereDelegate<CartColumns> where, Database db = null)
		{
			var results = new CartCollection(db, Select<CartColumns>.From<Cart>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CartColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Cart OneWhere(QiQuery where, Database db = null)
		{
			var results = new CartCollection(db, Select<CartColumns>.From<Cart>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Cart OneOrThrow(CartCollection c)
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

		/// <summary>
		/// Execute a query and return the first result
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Cart FirstOneWhere(WhereDelegate<CartColumns> where, Database db = null)
		{
			var results = new CartCollection(db, Select<CartColumns>.From<Cart>().Where(where, db));
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
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a CartColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CartCollection Top(int count, WhereDelegate<CartColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		/// <summary>
		/// Execute a query and return the specified count
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a CartColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CartCollection Top(int count, WhereDelegate<CartColumns> where, OrderBy<CartColumns> orderBy, Database database = null)
		{
			CartColumns c = new CartColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Cart>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Cart>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CartColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CartCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CartColumns> where, Database database = null)
		{
			CartColumns c = new CartColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Cart>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Cart>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
