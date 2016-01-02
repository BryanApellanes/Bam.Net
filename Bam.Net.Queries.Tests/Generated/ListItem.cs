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
	[Bam.Net.Data.Table("ListItem", "Shop")]
	public partial class ListItem: Dao
	{
		public ListItem():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public ListItem(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator ListItem(DataRow data)
		{
			return new ListItem(data);
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



	// start ListId -> ListId
	[Bam.Net.Data.ForeignKey(
        Table="ListItem",
		Name="ListId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="List",
		Suffix="1")]
	public long? ListId
	{
		get
		{
			return GetLongValue("ListId");
		}
		set
		{
			SetValue("ListId", value);
		}
	}

	List _listOfListId;
	public List ListOfListId
	{
		get
		{
			if(_listOfListId == null)
			{
				_listOfListId = Bam.Net.Data.Tests.List.OneWhere(f => f.Id == this.ListId);
			}
			return _listOfListId;
		}
	}
	
	// start ItemId -> ItemId
	[Bam.Net.Data.ForeignKey(
        Table="ListItem",
		Name="ItemId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
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
			var colFilter = new ListItemColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the ListItem table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ListItemCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ListItem>();
			Database db = database == null ? Db.For<ListItem>(): database;
			var results = new ListItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ListItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ListItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ListItemCollection Where(Func<ListItemColumns, QueryFilter<ListItemColumns>> where, OrderBy<ListItemColumns> orderBy = null)
		{
			return new ListItemCollection(new Query<ListItemColumns, ListItem>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ListItemCollection Where(WhereDelegate<ListItemColumns> where, Database db = null)
		{
			var results = new ListItemCollection(db, new Query<ListItemColumns, ListItem>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ListItemCollection Where(WhereDelegate<ListItemColumns> where, OrderBy<ListItemColumns> orderBy = null, Database db = null)
		{
			var results = new ListItemCollection(db, new Query<ListItemColumns, ListItem>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ListItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ListItemCollection Where(QiQuery where, Database db = null)
		{
			var results = new ListItemCollection(db, Select<ListItemColumns>.From<ListItem>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ListItem instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ListItem OneWhere(WhereDelegate<ListItemColumns> where, Database db = null)
		{
			var results = new ListItemCollection(db, Select<ListItemColumns>.From<ListItem>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ListItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ListItem OneWhere(QiQuery where, Database db = null)
		{
			var results = new ListItemCollection(db, Select<ListItemColumns>.From<ListItem>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ListItem OneOrThrow(ListItemCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a ListItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ListItem FirstOneWhere(WhereDelegate<ListItemColumns> where, Database db = null)
		{
			var results = new ListItemCollection(db, Select<ListItemColumns>.From<ListItem>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a ListItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ListItemCollection Top(int count, WhereDelegate<ListItemColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a ListItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ListItemCollection Top(int count, WhereDelegate<ListItemColumns> where, OrderBy<ListItemColumns> orderBy, Database database = null)
		{
			ListItemColumns c = new ListItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<ListItem>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<ListItem>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ListItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ListItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ListItemColumns> where, Database database = null)
		{
			ListItemColumns c = new ListItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<ListItem>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ListItem>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
