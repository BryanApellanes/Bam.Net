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

namespace Bam.Net.Services.AsyncCallback.Data.Dao
{
	// schema = AsyncCallback
	// connection Name = AsyncCallback
	[Serializable]
	[Bam.Net.Data.Table("AsyncExecutionData", "AsyncCallback")]
	public partial class AsyncExecutionData: Bam.Net.Data.Dao
	{
		public AsyncExecutionData():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AsyncExecutionData(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AsyncExecutionData(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AsyncExecutionData(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator AsyncExecutionData(DataRow data)
		{
			return new AsyncExecutionData(data);
		}

		private void SetChildren()
		{
						
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

	// property:RequestCuid, columnName:RequestCuid	
	[Bam.Net.Data.Column(Name="RequestCuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string RequestCuid
	{
		get
		{
			return GetStringValue("RequestCuid");
		}
		set
		{
			SetValue("RequestCuid", value);
		}
	}

	// property:RequestHash, columnName:RequestHash	
	[Bam.Net.Data.Column(Name="RequestHash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string RequestHash
	{
		get
		{
			return GetStringValue("RequestHash");
		}
		set
		{
			SetValue("RequestHash", value);
		}
	}

	// property:ResponseCuid, columnName:ResponseCuid	
	[Bam.Net.Data.Column(Name="ResponseCuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ResponseCuid
	{
		get
		{
			return GetStringValue("ResponseCuid");
		}
		set
		{
			SetValue("ResponseCuid", value);
		}
	}

	// property:ResponseHash, columnName:ResponseHash	
	[Bam.Net.Data.Column(Name="ResponseHash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ResponseHash
	{
		get
		{
			return GetStringValue("ResponseHash");
		}
		set
		{
			SetValue("ResponseHash", value);
		}
	}

	// property:Success, columnName:Success	
	[Bam.Net.Data.Column(Name="Success", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? Success
	{
		get
		{
			return GetBooleanValue("Success");
		}
		set
		{
			SetValue("Success", value);
		}
	}

	// property:Requested, columnName:Requested	
	[Bam.Net.Data.Column(Name="Requested", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Requested
	{
		get
		{
			return GetDateTimeValue("Requested");
		}
		set
		{
			SetValue("Requested", value);
		}
	}

	// property:Responded, columnName:Responded	
	[Bam.Net.Data.Column(Name="Responded", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Responded
	{
		get
		{
			return GetDateTimeValue("Responded");
		}
		set
		{
			SetValue("Responded", value);
		}
	}

	// property:Created, columnName:Created	
	[Bam.Net.Data.Column(Name="Created", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Created
	{
		get
		{
			return GetDateTimeValue("Created");
		}
		set
		{
			SetValue("Created", value);
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
				var colFilter = new AsyncExecutionDataColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the AsyncExecutionData table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AsyncExecutionDataCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionData>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<AsyncExecutionData>();
			var results = new AsyncExecutionDataCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<AsyncExecutionData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AsyncExecutionDataColumns columns = new AsyncExecutionDataColumns();
				var orderBy = Bam.Net.Data.Order.By<AsyncExecutionDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<AsyncExecutionData>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<AsyncExecutionDataColumns> where, Action<IEnumerable<AsyncExecutionData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AsyncExecutionDataColumns columns = new AsyncExecutionDataColumns();
				var orderBy = Bam.Net.Data.Order.By<AsyncExecutionDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (AsyncExecutionDataColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<AsyncExecutionData>> batchProcessor, Bam.Net.Data.OrderBy<AsyncExecutionDataColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<AsyncExecutionDataColumns> where, Action<IEnumerable<AsyncExecutionData>> batchProcessor, Bam.Net.Data.OrderBy<AsyncExecutionDataColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AsyncExecutionDataColumns columns = new AsyncExecutionDataColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (AsyncExecutionDataColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static AsyncExecutionData GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static AsyncExecutionData GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static AsyncExecutionData GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AsyncExecutionData GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AsyncExecutionData GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static AsyncExecutionData GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static AsyncExecutionDataCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static AsyncExecutionDataCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<AsyncExecutionDataColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AsyncExecutionDataColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionDataCollection Where(Func<AsyncExecutionDataColumns, QueryFilter<AsyncExecutionDataColumns>> where, OrderBy<AsyncExecutionDataColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<AsyncExecutionData>();
			return new AsyncExecutionDataCollection(database.GetQuery<AsyncExecutionDataColumns, AsyncExecutionData>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionDataCollection Where(WhereDelegate<AsyncExecutionDataColumns> where, Database database = null)
		{		
			database = database ?? Db.For<AsyncExecutionData>();
			var results = new AsyncExecutionDataCollection(database, database.GetQuery<AsyncExecutionDataColumns, AsyncExecutionData>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionDataCollection Where(WhereDelegate<AsyncExecutionDataColumns> where, OrderBy<AsyncExecutionDataColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<AsyncExecutionData>();
			var results = new AsyncExecutionDataCollection(database, database.GetQuery<AsyncExecutionDataColumns, AsyncExecutionData>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;AsyncExecutionDataColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AsyncExecutionDataCollection Where(QiQuery where, Database database = null)
		{
			var results = new AsyncExecutionDataCollection(database, Select<AsyncExecutionDataColumns>.From<AsyncExecutionData>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static AsyncExecutionData GetOneWhere(QueryFilter where, Database database = null)
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
		public static AsyncExecutionData OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<AsyncExecutionDataColumns> whereDelegate = (c) => where;
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
		public static AsyncExecutionData GetOneWhere(WhereDelegate<AsyncExecutionDataColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				AsyncExecutionDataColumns c = new AsyncExecutionDataColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AsyncExecutionData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionData OneWhere(WhereDelegate<AsyncExecutionDataColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AsyncExecutionDataColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AsyncExecutionData OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionData FirstOneWhere(WhereDelegate<AsyncExecutionDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionData FirstOneWhere(WhereDelegate<AsyncExecutionDataColumns> where, OrderBy<AsyncExecutionDataColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionData FirstOneWhere(QueryFilter where, OrderBy<AsyncExecutionDataColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<AsyncExecutionDataColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionDataCollection Top(int count, WhereDelegate<AsyncExecutionDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static AsyncExecutionDataCollection Top(int count, WhereDelegate<AsyncExecutionDataColumns> where, OrderBy<AsyncExecutionDataColumns> orderBy, Database database = null)
		{
			AsyncExecutionDataColumns c = new AsyncExecutionDataColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<AsyncExecutionData>();
			QuerySet query = GetQuerySet(db); 
			query.Top<AsyncExecutionData>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AsyncExecutionDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AsyncExecutionDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AsyncExecutionDataCollection Top(int count, QueryFilter where, Database database)
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
		public static AsyncExecutionDataCollection Top(int count, QueryFilter where, OrderBy<AsyncExecutionDataColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionData>();
			QuerySet query = GetQuerySet(db);
			query.Top<AsyncExecutionData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<AsyncExecutionDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AsyncExecutionDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AsyncExecutionDataCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionData>();
			QuerySet query = GetQuerySet(db);
			query.Top<AsyncExecutionData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<AsyncExecutionDataCollection>(0);
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
		public static AsyncExecutionDataCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionData>();
			QuerySet query = GetQuerySet(db);
			query.Top<AsyncExecutionData>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<AsyncExecutionDataCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of AsyncExecutionDatas
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<AsyncExecutionData>();
            QuerySet query = GetQuerySet(db);
            query.Count<AsyncExecutionData>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionDataColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<AsyncExecutionDataColumns> where, Database database = null)
		{
			AsyncExecutionDataColumns c = new AsyncExecutionDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<AsyncExecutionData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AsyncExecutionData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<AsyncExecutionData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AsyncExecutionData>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static AsyncExecutionData CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionData>();			
			var dao = new AsyncExecutionData();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static AsyncExecutionData OneOrThrow(AsyncExecutionDataCollection c)
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
