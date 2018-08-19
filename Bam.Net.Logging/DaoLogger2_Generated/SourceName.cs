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
	[Bam.Net.Data.Table("SourceName", "DaoLogger2")]
	public partial class SourceName: Bam.Net.Data.Dao
	{
		public SourceName():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SourceName(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SourceName(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SourceName(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator SourceName(DataRow data)
		{
			return new SourceName(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("Event_SourceNameId", new EventCollection(Database.GetQuery<EventColumns, Event>((c) => c.SourceNameId == GetULongValue("Id")), this, "SourceNameId"));				
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
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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
	public EventCollection EventsBySourceNameId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Event_SourceNameId"))
			{
				SetChildren();
			}

			var c = (EventCollection)this.ChildCollections["Event_SourceNameId"];
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
				var colFilter = new SourceNameColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the SourceName table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SourceNameCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<SourceName>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<SourceName>();
			var results = new SourceNameCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<SourceName>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				SourceNameColumns columns = new SourceNameColumns();
				var orderBy = Bam.Net.Data.Order.By<SourceNameColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<SourceName>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<SourceNameColumns> where, Action<IEnumerable<SourceName>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				SourceNameColumns columns = new SourceNameColumns();
				var orderBy = Bam.Net.Data.Order.By<SourceNameColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (SourceNameColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<SourceName>> batchProcessor, Bam.Net.Data.OrderBy<SourceNameColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<SourceNameColumns> where, Action<IEnumerable<SourceName>> batchProcessor, Bam.Net.Data.OrderBy<SourceNameColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				SourceNameColumns columns = new SourceNameColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (SourceNameColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static SourceName GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static SourceName GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static SourceName GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static SourceName GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static SourceName GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static SourceName GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static SourceNameCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static SourceNameCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<SourceNameColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SourceNameColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static SourceNameCollection Where(Func<SourceNameColumns, QueryFilter<SourceNameColumns>> where, OrderBy<SourceNameColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<SourceName>();
			return new SourceNameCollection(database.GetQuery<SourceNameColumns, SourceName>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static SourceNameCollection Where(WhereDelegate<SourceNameColumns> where, Database database = null)
		{		
			database = database ?? Db.For<SourceName>();
			var results = new SourceNameCollection(database, database.GetQuery<SourceNameColumns, SourceName>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SourceNameCollection Where(WhereDelegate<SourceNameColumns> where, OrderBy<SourceNameColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<SourceName>();
			var results = new SourceNameCollection(database, database.GetQuery<SourceNameColumns, SourceName>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;SourceNameColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SourceNameCollection Where(QiQuery where, Database database = null)
		{
			var results = new SourceNameCollection(database, Select<SourceNameColumns>.From<SourceName>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static SourceName GetOneWhere(QueryFilter where, Database database = null)
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
		public static SourceName OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<SourceNameColumns> whereDelegate = (c) => where;
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
		public static SourceName GetOneWhere(WhereDelegate<SourceNameColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				SourceNameColumns c = new SourceNameColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SourceName instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SourceName OneWhere(WhereDelegate<SourceNameColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SourceNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SourceName OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SourceName FirstOneWhere(WhereDelegate<SourceNameColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SourceName FirstOneWhere(WhereDelegate<SourceNameColumns> where, OrderBy<SourceNameColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SourceName FirstOneWhere(QueryFilter where, OrderBy<SourceNameColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<SourceNameColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SourceNameCollection Top(int count, WhereDelegate<SourceNameColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static SourceNameCollection Top(int count, WhereDelegate<SourceNameColumns> where, OrderBy<SourceNameColumns> orderBy, Database database = null)
		{
			SourceNameColumns c = new SourceNameColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<SourceName>();
			QuerySet query = GetQuerySet(db); 
			query.Top<SourceName>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SourceNameColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SourceNameCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static SourceNameCollection Top(int count, QueryFilter where, Database database)
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
		public static SourceNameCollection Top(int count, QueryFilter where, OrderBy<SourceNameColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<SourceName>();
			QuerySet query = GetQuerySet(db);
			query.Top<SourceName>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<SourceNameColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SourceNameCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static SourceNameCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<SourceName>();
			QuerySet query = GetQuerySet(db);
			query.Top<SourceName>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<SourceNameCollection>(0);
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
		public static SourceNameCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<SourceName>();
			QuerySet query = GetQuerySet(db);
			query.Top<SourceName>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<SourceNameCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of SourceNames
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<SourceName>();
            QuerySet query = GetQuerySet(db);
            query.Count<SourceName>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<SourceNameColumns> where, Database database = null)
		{
			SourceNameColumns c = new SourceNameColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<SourceName>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<SourceName>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<SourceName>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<SourceName>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static SourceName CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<SourceName>();			
			var dao = new SourceName();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static SourceName OneOrThrow(SourceNameCollection c)
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
