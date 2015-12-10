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
	[Bam.Net.Data.Table("CartItem", "Shop")]
	public partial class CartItem: Dao
	{
		public CartItem():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public CartItem(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator CartItem(DataRow data)
		{
			return new CartItem(data);
		}

		private void SetChildren()
		{
						
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

	// property:Quantity, columnName:Quantity	
	[Bam.Net.Data.Column(Name="Quantity", DbDataType="Int", MaxLength="4", AllowNull=false)]
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
				_cartOfCartId = Bam.Net.Data.Tests.Cart.OneWhere(f => f.Id == this.CartId);
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
				_itemOfItemId = Bam.Net.Data.Tests.Item.OneWhere(f => f.Id == this.ItemId);
			}
			return _itemOfItemId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new CartItemColumns();
			return (colFilter.Id == IdValue);
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
			Database db = database == null ? Db.For<CartItem>(): database;
			var results = new CartItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CartItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CartItemCollection Where(Func<CartItemColumns, QueryFilter<CartItemColumns>> where, OrderBy<CartItemColumns> orderBy = null)
		{
			return new CartItemCollection(new Query<CartItemColumns, CartItem>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CartItemCollection Where(WhereDelegate<CartItemColumns> where, Database db = null)
		{
			var results = new CartItemCollection(db, new Query<CartItemColumns, CartItem>(where, db), true);
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
		/// <param name="db"></param>
		public static CartItemCollection Where(WhereDelegate<CartItemColumns> where, OrderBy<CartItemColumns> orderBy = null, Database db = null)
		{
			var results = new CartItemCollection(db, new Query<CartItemColumns, CartItem>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CartItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CartItemCollection Where(QiQuery where, Database db = null)
		{
			var results = new CartItemCollection(db, Select<CartItemColumns>.From<CartItem>().Where(where, db));
			return results;
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
		/// <param name="db"></param>
		public static CartItem OneWhere(WhereDelegate<CartItemColumns> where, Database db = null)
		{
			var results = new CartItemCollection(db, Select<CartItemColumns>.From<CartItem>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CartItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CartItem OneWhere(QiQuery where, Database db = null)
		{
			var results = new CartItemCollection(db, Select<CartItemColumns>.From<CartItem>().Where(where, db));
			return OneOrThrow(results);
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

		/// <summary>
		/// Execute a query and return the first result
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CartItem FirstOneWhere(WhereDelegate<CartItemColumns> where, Database db = null)
		{
			var results = new CartItemCollection(db, Select<CartItemColumns>.From<CartItem>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CartItemCollection Top(int count, WhereDelegate<CartItemColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CartItemCollection Top(int count, WhereDelegate<CartItemColumns> where, OrderBy<CartItemColumns> orderBy, Database database = null)
		{
			CartItemColumns c = new CartItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<CartItem>(): database;
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

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CartItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CartItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CartItemColumns> where, Database database = null)
		{
			CartItemColumns c = new CartItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<CartItem>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<CartItem>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
