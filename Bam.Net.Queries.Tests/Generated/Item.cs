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
	[Bam.Net.Data.Table("Item", "Shop")]
	public partial class Item: Dao
	{
		public Item():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Item(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Item(DataRow data)
		{
			return new Item(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("CartItem_ItemId", new CartItemCollection(new Query<CartItemColumns, CartItem>((c) => c.ItemId == this.Id), this, "ItemId"));	
            this.ChildCollections.Add("ListItem_ItemId", new ListItemCollection(new Query<ListItemColumns, ListItem>((c) => c.ItemId == this.Id), this, "ItemId"));							
            this.ChildCollections.Add("Item_ListItem_List",  new XrefDaoCollection<ListItem, List>(this, false));
				
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
	public CartItemCollection CartItemsByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("CartItem_ItemId"))
			{
				SetChildren();
			}

			var c = (CartItemCollection)this.ChildCollections["CartItem_ItemId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public ListItemCollection ListItemsByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ListItem_ItemId"))
			{
				SetChildren();
			}

			var c = (ListItemCollection)this.ChildCollections["ListItem_ItemId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<ListItem, List> Lists
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Item_ListItem_List"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ListItem, List>)this.ChildCollections["Item_ListItem_List"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ItemColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Item table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ItemCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Item>();
			Database db = database == null ? Db.For<Item>(): database;
			var results = new ItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ItemCollection Where(Func<ItemColumns, QueryFilter<ItemColumns>> where, OrderBy<ItemColumns> orderBy = null)
		{
			return new ItemCollection(new Query<ItemColumns, Item>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ItemCollection Where(WhereDelegate<ItemColumns> where, Database db = null)
		{
			var results = new ItemCollection(db, new Query<ItemColumns, Item>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ItemCollection Where(WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy = null, Database db = null)
		{
			var results = new ItemCollection(db, new Query<ItemColumns, Item>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ItemCollection Where(QiQuery where, Database db = null)
		{
			var results = new ItemCollection(db, Select<ItemColumns>.From<Item>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Item instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Item OneWhere(WhereDelegate<ItemColumns> where, Database db = null)
		{
			var results = new ItemCollection(db, Select<ItemColumns>.From<Item>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Item OneWhere(QiQuery where, Database db = null)
		{
			var results = new ItemCollection(db, Select<ItemColumns>.From<Item>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Item OneOrThrow(ItemCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Item FirstOneWhere(WhereDelegate<ItemColumns> where, Database db = null)
		{
			var results = new ItemCollection(db, Select<ItemColumns>.From<Item>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ItemCollection Top(int count, WhereDelegate<ItemColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ItemCollection Top(int count, WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy, Database database = null)
		{
			ItemColumns c = new ItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Item>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Item>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ItemColumns> where, Database database = null)
		{
			ItemColumns c = new ItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Item>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Item>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
