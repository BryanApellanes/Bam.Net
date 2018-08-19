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

namespace Bam.Net.Logging.Data
{
	// schema = DaoLogger2
	// connection Name = DaoLogger2
	[Serializable]
	[Bam.Net.Data.Table("CategoryName", "DaoLogger2")]
	public partial class CategoryName: Bam.Net.Data.Dao
	{
		public CategoryName():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CategoryName(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CategoryName(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CategoryName(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator CategoryName(DataRow data)
		{
			return new CategoryName(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("Event_CategoryNameId", new EventCollection(Database.GetQuery<EventColumns, Event>((c) => c.CategoryNameId == GetULongValue("Id")), this, "CategoryNameId"));				
			}						
		}

	// property:Id, columnName:Id	
	[Bam.Net.Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="19")]
	public ulong? Id
	{
		get
		{
			return GetULongValue("Id");
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

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				

	[Bam.Net.Exclude]	
	public EventCollection EventsByCategoryNameId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Event_CategoryNameId"))
			{
				SetChildren();
			}

			var c = (EventCollection)this.ChildCollections["Event_CategoryNameId"];
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
		[Bam.Net.Exclude] 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider(this);
			}
			else
			{
				var colFilter = new CategoryNameColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the CategoryName table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CategoryNameCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<CategoryName>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<CategoryName>();
			var results = new CategoryNameCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<CategoryName>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CategoryNameColumns columns = new CategoryNameColumns();
				var orderBy = Bam.Net.Data.Order.By<CategoryNameColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<CategoryName>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<CategoryNameColumns> where, Action<IEnumerable<CategoryName>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CategoryNameColumns columns = new CategoryNameColumns();
				var orderBy = Bam.Net.Data.Order.By<CategoryNameColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (CategoryNameColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<CategoryName>> batchProcessor, Bam.Net.Data.OrderBy<CategoryNameColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<CategoryNameColumns> where, Action<IEnumerable<CategoryName>> batchProcessor, Bam.Net.Data.OrderBy<CategoryNameColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CategoryNameColumns columns = new CategoryNameColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (CategoryNameColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static CategoryName GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static CategoryName GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static CategoryName GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static CategoryName GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static CategoryName GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static CategoryName GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static CategoryNameCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static CategoryNameCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<CategoryNameColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CategoryNameColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CategoryNameCollection Where(Func<CategoryNameColumns, QueryFilter<CategoryNameColumns>> where, OrderBy<CategoryNameColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<CategoryName>();
			return new CategoryNameCollection(database.GetQuery<CategoryNameColumns, CategoryName>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CategoryNameCollection Where(WhereDelegate<CategoryNameColumns> where, Database database = null)
		{		
			database = database ?? Db.For<CategoryName>();
			var results = new CategoryNameCollection(database, database.GetQuery<CategoryNameColumns, CategoryName>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CategoryNameCollection Where(WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<CategoryName>();
			var results = new CategoryNameCollection(database, database.GetQuery<CategoryNameColumns, CategoryName>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;CategoryNameColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CategoryNameCollection Where(QiQuery where, Database database = null)
		{
			var results = new CategoryNameCollection(database, Select<CategoryNameColumns>.From<CategoryName>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static CategoryName GetOneWhere(QueryFilter where, Database database = null)
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
		public static CategoryName OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<CategoryNameColumns> whereDelegate = (c) => where;
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
		public static CategoryName GetOneWhere(WhereDelegate<CategoryNameColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				CategoryNameColumns c = new CategoryNameColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single CategoryName instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CategoryName OneWhere(WhereDelegate<CategoryNameColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CategoryNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CategoryName OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CategoryName FirstOneWhere(WhereDelegate<CategoryNameColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CategoryName FirstOneWhere(WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CategoryName FirstOneWhere(QueryFilter where, OrderBy<CategoryNameColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<CategoryNameColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CategoryNameCollection Top(int count, WhereDelegate<CategoryNameColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static CategoryNameCollection Top(int count, WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy, Database database = null)
		{
			CategoryNameColumns c = new CategoryNameColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db); 
			query.Top<CategoryName>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CategoryNameColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CategoryNameCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static CategoryNameCollection Top(int count, QueryFilter where, Database database)
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
		public static CategoryNameCollection Top(int count, QueryFilter where, OrderBy<CategoryNameColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db);
			query.Top<CategoryName>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<CategoryNameColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CategoryNameCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static CategoryNameCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db);
			query.Top<CategoryName>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<CategoryNameCollection>(0);
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
		public static CategoryNameCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db);
			query.Top<CategoryName>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<CategoryNameCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of CategoryNames
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<CategoryName>();
            QuerySet query = GetQuerySet(db);
            query.Count<CategoryName>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<CategoryNameColumns> where, Database database = null)
		{
			CategoryNameColumns c = new CategoryNameColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<CategoryName>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<CategoryName>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static CategoryName CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<CategoryName>();			
			var dao = new CategoryName();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static CategoryName OneOrThrow(CategoryNameCollection c)
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
