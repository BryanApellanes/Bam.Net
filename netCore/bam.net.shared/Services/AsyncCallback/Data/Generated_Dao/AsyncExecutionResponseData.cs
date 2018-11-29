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
	[Bam.Net.Data.Table("AsyncExecutionResponseData", "AsyncCallback")]
	public partial class AsyncExecutionResponseData: Bam.Net.Data.Dao
	{
		public AsyncExecutionResponseData():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AsyncExecutionResponseData(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AsyncExecutionResponseData(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AsyncExecutionResponseData(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator AsyncExecutionResponseData(DataRow data)
		{
			return new AsyncExecutionResponseData(data);
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

	// property:RequestId, columnName:RequestId	
	[Bam.Net.Data.Column(Name="RequestId", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public ulong? RequestId
	{
		get
		{
			return GetULongValue("RequestId");
		}
		set
		{
			SetValue("RequestId", value);
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

	// property:ResultJson, columnName:ResultJson	
	[Bam.Net.Data.Column(Name="ResultJson", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ResultJson
	{
		get
		{
			return GetStringValue("ResultJson");
		}
		set
		{
			SetValue("ResultJson", value);
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
				var colFilter = new AsyncExecutionResponseDataColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the AsyncExecutionResponseData table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AsyncExecutionResponseDataCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionResponseData>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<AsyncExecutionResponseData>();
			var results = new AsyncExecutionResponseDataCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<AsyncExecutionResponseData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AsyncExecutionResponseDataColumns columns = new AsyncExecutionResponseDataColumns();
				var orderBy = Bam.Net.Data.Order.By<AsyncExecutionResponseDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<AsyncExecutionResponseData>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<AsyncExecutionResponseDataColumns> where, Action<IEnumerable<AsyncExecutionResponseData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AsyncExecutionResponseDataColumns columns = new AsyncExecutionResponseDataColumns();
				var orderBy = Bam.Net.Data.Order.By<AsyncExecutionResponseDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (AsyncExecutionResponseDataColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<AsyncExecutionResponseData>> batchProcessor, Bam.Net.Data.OrderBy<AsyncExecutionResponseDataColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<AsyncExecutionResponseDataColumns> where, Action<IEnumerable<AsyncExecutionResponseData>> batchProcessor, Bam.Net.Data.OrderBy<AsyncExecutionResponseDataColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AsyncExecutionResponseDataColumns columns = new AsyncExecutionResponseDataColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (AsyncExecutionResponseDataColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static AsyncExecutionResponseData GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static AsyncExecutionResponseData GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static AsyncExecutionResponseData GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AsyncExecutionResponseData GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AsyncExecutionResponseData GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static AsyncExecutionResponseData GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static AsyncExecutionResponseDataCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static AsyncExecutionResponseDataCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<AsyncExecutionResponseDataColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseDataCollection Where(Func<AsyncExecutionResponseDataColumns, QueryFilter<AsyncExecutionResponseDataColumns>> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<AsyncExecutionResponseData>();
			return new AsyncExecutionResponseDataCollection(database.GetQuery<AsyncExecutionResponseDataColumns, AsyncExecutionResponseData>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseDataCollection Where(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database database = null)
		{		
			database = database ?? Db.For<AsyncExecutionResponseData>();
			var results = new AsyncExecutionResponseDataCollection(database, database.GetQuery<AsyncExecutionResponseDataColumns, AsyncExecutionResponseData>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseDataCollection Where(WhereDelegate<AsyncExecutionResponseDataColumns> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<AsyncExecutionResponseData>();
			var results = new AsyncExecutionResponseDataCollection(database, database.GetQuery<AsyncExecutionResponseDataColumns, AsyncExecutionResponseData>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;AsyncExecutionResponseDataColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AsyncExecutionResponseDataCollection Where(QiQuery where, Database database = null)
		{
			var results = new AsyncExecutionResponseDataCollection(database, Select<AsyncExecutionResponseDataColumns>.From<AsyncExecutionResponseData>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseData GetOneWhere(QueryFilter where, Database database = null)
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
		public static AsyncExecutionResponseData OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<AsyncExecutionResponseDataColumns> whereDelegate = (c) => where;
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
		public static AsyncExecutionResponseData GetOneWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				AsyncExecutionResponseDataColumns c = new AsyncExecutionResponseDataColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AsyncExecutionResponseData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseData OneWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AsyncExecutionResponseDataColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AsyncExecutionResponseData OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseData FirstOneWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseData FirstOneWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseData FirstOneWhere(QueryFilter where, OrderBy<AsyncExecutionResponseDataColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<AsyncExecutionResponseDataColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseDataCollection Top(int count, WhereDelegate<AsyncExecutionResponseDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static AsyncExecutionResponseDataCollection Top(int count, WhereDelegate<AsyncExecutionResponseDataColumns> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy, Database database = null)
		{
			AsyncExecutionResponseDataColumns c = new AsyncExecutionResponseDataColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<AsyncExecutionResponseData>();
			QuerySet query = GetQuerySet(db); 
			query.Top<AsyncExecutionResponseData>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AsyncExecutionResponseDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AsyncExecutionResponseDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AsyncExecutionResponseDataCollection Top(int count, QueryFilter where, Database database)
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
		public static AsyncExecutionResponseDataCollection Top(int count, QueryFilter where, OrderBy<AsyncExecutionResponseDataColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionResponseData>();
			QuerySet query = GetQuerySet(db);
			query.Top<AsyncExecutionResponseData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<AsyncExecutionResponseDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AsyncExecutionResponseDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AsyncExecutionResponseDataCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionResponseData>();
			QuerySet query = GetQuerySet(db);
			query.Top<AsyncExecutionResponseData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<AsyncExecutionResponseDataCollection>(0);
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
		public static AsyncExecutionResponseDataCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionResponseData>();
			QuerySet query = GetQuerySet(db);
			query.Top<AsyncExecutionResponseData>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<AsyncExecutionResponseDataCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of AsyncExecutionResponseDatas
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<AsyncExecutionResponseData>();
            QuerySet query = GetQuerySet(db);
            query.Count<AsyncExecutionResponseData>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionResponseDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionResponseDataColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database database = null)
		{
			AsyncExecutionResponseDataColumns c = new AsyncExecutionResponseDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<AsyncExecutionResponseData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AsyncExecutionResponseData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<AsyncExecutionResponseData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AsyncExecutionResponseData>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static AsyncExecutionResponseData CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionResponseData>();			
			var dao = new AsyncExecutionResponseData();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static AsyncExecutionResponseData OneOrThrow(AsyncExecutionResponseDataCollection c)
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
