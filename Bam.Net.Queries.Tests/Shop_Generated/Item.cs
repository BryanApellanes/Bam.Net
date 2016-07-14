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
	[Bam.Net.Data.Table("Item", "Shop")]
	public partial class Item: Dao
	{
		public Item():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Item(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Item(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Item(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Item(DataRow data)
		{
			return new Item(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("CartItem_ItemId", new CartItemCollection(Database.GetQuery<CartItemColumns, CartItem>((c) => c.ItemId == GetLongValue("Id")), this, "ItemId"));	
            this.ChildCollections.Add("ListItem_ItemId", new ListItemCollection(Database.GetQuery<ListItemColumns, ListItem>((c) => c.ItemId == GetLongValue("Id")), this, "ItemId"));							
            this.ChildCollections.Add("Item_ListItem_List",  new XrefDaoCollection<ListItem, List>(this, false));
				
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
	public CartItemCollection CartItemsByItemId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

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
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

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
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Item_ListItem_List"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ListItem, List>)this.ChildCollections["Item_ListItem_List"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }		/// <summary>
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
				var colFilter = new ItemColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
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
			Database db = database ?? Db.For<Item>();
			var results = new ItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Action<IEnumerable<Item>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ItemColumns columns = new ItemColumns();
				var orderBy = Order.By<ItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
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

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Item>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<ItemColumns> where, Action<IEnumerable<Item>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ItemColumns columns = new ItemColumns();
				var orderBy = Order.By<ItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ItemColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Item GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Item GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Item GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Item GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static ItemCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static ItemCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ItemColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ItemCollection Where(Func<ItemColumns, QueryFilter<ItemColumns>> where, OrderBy<ItemColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Item>();
			return new ItemCollection(database.GetQuery<ItemColumns, Item>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ItemCollection Where(WhereDelegate<ItemColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Item>();
			var results = new ItemCollection(database, database.GetQuery<ItemColumns, Item>(where), true);
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
		/// <param name="database"></param>
		public static ItemCollection Where(WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Item>();
			var results = new ItemCollection(database, database.GetQuery<ItemColumns, Item>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ItemColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ItemCollection Where(QiQuery where, Database database = null)
		{
			var results = new ItemCollection(database, Select<ItemColumns>.From<Item>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Item GetOneWhere(QueryFilter where, Database database = null)
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
		public static Item OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ItemColumns> whereDelegate = (c) => where;
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
		public static Item GetOneWhere(WhereDelegate<ItemColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ItemColumns c = new ItemColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
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
		/// <param name="database"></param>
		public static Item OneWhere(WhereDelegate<ItemColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Item OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Item FirstOneWhere(WhereDelegate<ItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Item FirstOneWhere(WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Item FirstOneWhere(QueryFilter where, OrderBy<ItemColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ItemColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static ItemCollection Top(int count, WhereDelegate<ItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static ItemCollection Top(int count, WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy, Database database = null)
		{
			ItemColumns c = new ItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Item>();
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

		public static ItemCollection Top(int count, QueryFilter where, Database database)
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
		public static ItemCollection Top(int count, QueryFilter where, OrderBy<ItemColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Item>();
			QuerySet query = GetQuerySet(db);
			query.Top<Item>(count);
			query.Where(where);

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
		public static ItemCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Item>();
			QuerySet query = GetQuerySet(db);
			query.Top<Item>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Items
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Item>();
            QuerySet query = GetQuerySet(db);
            query.Count<Item>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
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

			Database db = database ?? Db.For<Item>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Item>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Item CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Item>();			
			var dao = new Item();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
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

	}
}																								
