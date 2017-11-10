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
	[Bam.Net.Data.Table("List", "Shop")]
	public partial class List: Bam.Net.Data.Dao
	{
		public List():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public List(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public List(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public List(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator List(DataRow data)
		{
			return new List(data);
		}

		private void SetChildren()
		{
			if(_database != null)
			{
				this.ChildCollections.Add("ListItem_ListId", new ListItemCollection(Database.GetQuery<ListItemColumns, ListItem>((c) => c.ListId == GetLongValue("Id")), this, "ListId"));				
			}			
            this.ChildCollections.Add("List_ListItem_Item",  new XrefDaoCollection<ListItem, Item>(this, false));
							
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



				

	[Bam.Net.Exclude]	
	public ListItemCollection ListItemsByListId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ListItem_ListId"))
			{
				SetChildren();
			}

			var c = (ListItemCollection)this.ChildCollections["ListItem_ListId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<ListItem, Item> Items
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("List_ListItem_Item"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ListItem, Item>)this.ChildCollections["List_ListItem_Item"];
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
		[Bam.Net.Exclude] 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new ListColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the List table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ListCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<List>();
			Database db = database ?? Db.For<List>();
			var results = new ListCollection(db, sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<List>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ListColumns columns = new ListColumns();
				var orderBy = Bam.Net.Data.Order.By<ListColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<List>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ListColumns> where, Action<IEnumerable<List>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ListColumns columns = new ListColumns();
				var orderBy = Bam.Net.Data.Order.By<ListColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ListColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<List>> batchProcessor, Bam.Net.Data.OrderBy<ListColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ListColumns> where, Action<IEnumerable<List>> batchProcessor, Bam.Net.Data.OrderBy<ListColumns> orderBy, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ListColumns columns = new ListColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ListColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static List GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static List GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static List GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static List GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ListCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ListCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ListColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ListColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ListCollection Where(Func<ListColumns, QueryFilter<ListColumns>> where, OrderBy<ListColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<List>();
			return new ListCollection(database.GetQuery<ListColumns, List>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ListCollection Where(WhereDelegate<ListColumns> where, Database database = null)
		{		
			database = database ?? Db.For<List>();
			var results = new ListCollection(database, database.GetQuery<ListColumns, List>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ListCollection Where(WhereDelegate<ListColumns> where, OrderBy<ListColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<List>();
			var results = new ListCollection(database, database.GetQuery<ListColumns, List>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ListColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ListCollection Where(QiQuery where, Database database = null)
		{
			var results = new ListCollection(database, Select<ListColumns>.From<List>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static List GetOneWhere(QueryFilter where, Database database = null)
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
		public static List OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ListColumns> whereDelegate = (c) => where;
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
		public static List GetOneWhere(WhereDelegate<ListColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ListColumns c = new ListColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single List instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static List OneWhere(WhereDelegate<ListColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ListColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static List OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static List FirstOneWhere(WhereDelegate<ListColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static List FirstOneWhere(WhereDelegate<ListColumns> where, OrderBy<ListColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static List FirstOneWhere(QueryFilter where, OrderBy<ListColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ListColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ListCollection Top(int count, WhereDelegate<ListColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ListCollection Top(int count, WhereDelegate<ListColumns> where, OrderBy<ListColumns> orderBy, Database database = null)
		{
			ListColumns c = new ListColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<List>();
			QuerySet query = GetQuerySet(db); 
			query.Top<List>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ListColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ListCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ListCollection Top(int count, QueryFilter where, Database database)
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
		public static ListCollection Top(int count, QueryFilter where, OrderBy<ListColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<List>();
			QuerySet query = GetQuerySet(db);
			query.Top<List>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ListColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ListCollection>(0);
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
		public static ListCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<List>();
			QuerySet query = GetQuerySet(db);
			query.Top<List>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ListCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Lists
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<List>();
            QuerySet query = GetQuerySet(db);
            query.Count<List>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ListColumns> where, Database database = null)
		{
			ListColumns c = new ListColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<List>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<List>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<List>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<List>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static List CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<List>();			
			var dao = new List();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static List OneOrThrow(ListCollection c)
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
