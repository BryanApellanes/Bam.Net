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
	[Bam.Net.Data.Table("AsyncExecutionRequestData", "AsyncCallback")]
	public partial class AsyncExecutionRequestData: Bam.Net.Data.Dao
	{
		public AsyncExecutionRequestData():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AsyncExecutionRequestData(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AsyncExecutionRequestData(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AsyncExecutionRequestData(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator AsyncExecutionRequestData(DataRow data)
		{
			return new AsyncExecutionRequestData(data);
		}

		private void SetChildren()
		{
						
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

	// property:ClassName, columnName:ClassName	
	[Bam.Net.Data.Column(Name="ClassName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ClassName
	{
		get
		{
			return GetStringValue("ClassName");
		}
		set
		{
			SetValue("ClassName", value);
		}
	}

	// property:MethodName, columnName:MethodName	
	[Bam.Net.Data.Column(Name="MethodName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string MethodName
	{
		get
		{
			return GetStringValue("MethodName");
		}
		set
		{
			SetValue("MethodName", value);
		}
	}

	// property:JsonParams, columnName:JsonParams	
	[Bam.Net.Data.Column(Name="JsonParams", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string JsonParams
	{
		get
		{
			return GetStringValue("JsonParams");
		}
		set
		{
			SetValue("JsonParams", value);
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
				var colFilter = new AsyncExecutionRequestDataColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the AsyncExecutionRequestData table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AsyncExecutionRequestDataCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<AsyncExecutionRequestData>();
			Database db = database ?? Db.For<AsyncExecutionRequestData>();
			var results = new AsyncExecutionRequestDataCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<AsyncExecutionRequestData>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				AsyncExecutionRequestDataColumns columns = new AsyncExecutionRequestDataColumns();
				var orderBy = Bam.Net.Data.Order.By<AsyncExecutionRequestDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<AsyncExecutionRequestData>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<AsyncExecutionRequestDataColumns> where, Action<IEnumerable<AsyncExecutionRequestData>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				AsyncExecutionRequestDataColumns columns = new AsyncExecutionRequestDataColumns();
				var orderBy = Bam.Net.Data.Order.By<AsyncExecutionRequestDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (AsyncExecutionRequestDataColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static AsyncExecutionRequestData GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static AsyncExecutionRequestData GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AsyncExecutionRequestData GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static AsyncExecutionRequestData GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static AsyncExecutionRequestDataCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static AsyncExecutionRequestDataCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<AsyncExecutionRequestDataColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestDataCollection Where(Func<AsyncExecutionRequestDataColumns, QueryFilter<AsyncExecutionRequestDataColumns>> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<AsyncExecutionRequestData>();
			return new AsyncExecutionRequestDataCollection(database.GetQuery<AsyncExecutionRequestDataColumns, AsyncExecutionRequestData>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestDataCollection Where(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database database = null)
		{		
			database = database ?? Db.For<AsyncExecutionRequestData>();
			var results = new AsyncExecutionRequestDataCollection(database, database.GetQuery<AsyncExecutionRequestDataColumns, AsyncExecutionRequestData>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestDataCollection Where(WhereDelegate<AsyncExecutionRequestDataColumns> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<AsyncExecutionRequestData>();
			var results = new AsyncExecutionRequestDataCollection(database, database.GetQuery<AsyncExecutionRequestDataColumns, AsyncExecutionRequestData>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;AsyncExecutionRequestDataColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AsyncExecutionRequestDataCollection Where(QiQuery where, Database database = null)
		{
			var results = new AsyncExecutionRequestDataCollection(database, Select<AsyncExecutionRequestDataColumns>.From<AsyncExecutionRequestData>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestData GetOneWhere(QueryFilter where, Database database = null)
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
		public static AsyncExecutionRequestData OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<AsyncExecutionRequestDataColumns> whereDelegate = (c) => where;
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
		public static AsyncExecutionRequestData GetOneWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				AsyncExecutionRequestDataColumns c = new AsyncExecutionRequestDataColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AsyncExecutionRequestData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestData OneWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AsyncExecutionRequestDataColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AsyncExecutionRequestData OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestData FirstOneWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestData FirstOneWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestData FirstOneWhere(QueryFilter where, OrderBy<AsyncExecutionRequestDataColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<AsyncExecutionRequestDataColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestDataCollection Top(int count, WhereDelegate<AsyncExecutionRequestDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static AsyncExecutionRequestDataCollection Top(int count, WhereDelegate<AsyncExecutionRequestDataColumns> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy, Database database = null)
		{
			AsyncExecutionRequestDataColumns c = new AsyncExecutionRequestDataColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<AsyncExecutionRequestData>();
			QuerySet query = GetQuerySet(db); 
			query.Top<AsyncExecutionRequestData>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AsyncExecutionRequestDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AsyncExecutionRequestDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AsyncExecutionRequestDataCollection Top(int count, QueryFilter where, Database database)
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
		public static AsyncExecutionRequestDataCollection Top(int count, QueryFilter where, OrderBy<AsyncExecutionRequestDataColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionRequestData>();
			QuerySet query = GetQuerySet(db);
			query.Top<AsyncExecutionRequestData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<AsyncExecutionRequestDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AsyncExecutionRequestDataCollection>(0);
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
		public static AsyncExecutionRequestDataCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionRequestData>();
			QuerySet query = GetQuerySet(db);
			query.Top<AsyncExecutionRequestData>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<AsyncExecutionRequestDataCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of AsyncExecutionRequestDatas
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<AsyncExecutionRequestData>();
            QuerySet query = GetQuerySet(db);
            query.Count<AsyncExecutionRequestData>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AsyncExecutionRequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AsyncExecutionRequestDataColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database database = null)
		{
			AsyncExecutionRequestDataColumns c = new AsyncExecutionRequestDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<AsyncExecutionRequestData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AsyncExecutionRequestData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<AsyncExecutionRequestData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AsyncExecutionRequestData>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static AsyncExecutionRequestData CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<AsyncExecutionRequestData>();			
			var dao = new AsyncExecutionRequestData();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static AsyncExecutionRequestData OneOrThrow(AsyncExecutionRequestDataCollection c)
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
