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

namespace Bam.Net.CoreServices.WebHooks.Data.Dao
{
	// schema = WebHooks
	// connection Name = WebHooks
	[Serializable]
	[Bam.Net.Data.Table("WebHookDescriptorWebHookSubscriber", "WebHooks")]
	public partial class WebHookDescriptorWebHookSubscriber: Bam.Net.Data.Dao
	{
		public WebHookDescriptorWebHookSubscriber():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public WebHookDescriptorWebHookSubscriber(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public WebHookDescriptorWebHookSubscriber(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public WebHookDescriptorWebHookSubscriber(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator WebHookDescriptorWebHookSubscriber(DataRow data)
		{
			return new WebHookDescriptorWebHookSubscriber(data);
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



	// start WebHookDescriptorId -> WebHookDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="WebHookDescriptorWebHookSubscriber",
		Name="WebHookDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="WebHookDescriptor",
		Suffix="1")]
	public ulong? WebHookDescriptorId
	{
		get
		{
			return GetULongValue("WebHookDescriptorId");
		}
		set
		{
			SetValue("WebHookDescriptorId", value);
		}
	}

	WebHookDescriptor _webHookDescriptorOfWebHookDescriptorId;
	public WebHookDescriptor WebHookDescriptorOfWebHookDescriptorId
	{
		get
		{
			if(_webHookDescriptorOfWebHookDescriptorId == null)
			{
				_webHookDescriptorOfWebHookDescriptorId = Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.OneWhere(c => c.KeyColumn == this.WebHookDescriptorId, this.Database);
			}
			return _webHookDescriptorOfWebHookDescriptorId;
		}
	}
	
	// start WebHookSubscriberId -> WebHookSubscriberId
	[Bam.Net.Data.ForeignKey(
        Table="WebHookDescriptorWebHookSubscriber",
		Name="WebHookSubscriberId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="WebHookSubscriber",
		Suffix="2")]
	public ulong? WebHookSubscriberId
	{
		get
		{
			return GetULongValue("WebHookSubscriberId");
		}
		set
		{
			SetValue("WebHookSubscriberId", value);
		}
	}

	WebHookSubscriber _webHookSubscriberOfWebHookSubscriberId;
	public WebHookSubscriber WebHookSubscriberOfWebHookSubscriberId
	{
		get
		{
			if(_webHookSubscriberOfWebHookSubscriberId == null)
			{
				_webHookSubscriberOfWebHookSubscriberId = Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.OneWhere(c => c.KeyColumn == this.WebHookSubscriberId, this.Database);
			}
			return _webHookSubscriberOfWebHookSubscriberId;
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
				var colFilter = new WebHookDescriptorWebHookSubscriberColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the WebHookDescriptorWebHookSubscriber table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static WebHookDescriptorWebHookSubscriberCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<WebHookDescriptorWebHookSubscriber>();
			var results = new WebHookDescriptorWebHookSubscriberCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<WebHookDescriptorWebHookSubscriber>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				WebHookDescriptorWebHookSubscriberColumns columns = new WebHookDescriptorWebHookSubscriberColumns();
				var orderBy = Bam.Net.Data.Order.By<WebHookDescriptorWebHookSubscriberColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<WebHookDescriptorWebHookSubscriber>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Action<IEnumerable<WebHookDescriptorWebHookSubscriber>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				WebHookDescriptorWebHookSubscriberColumns columns = new WebHookDescriptorWebHookSubscriberColumns();
				var orderBy = Bam.Net.Data.Order.By<WebHookDescriptorWebHookSubscriberColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (WebHookDescriptorWebHookSubscriberColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<WebHookDescriptorWebHookSubscriber>> batchProcessor, Bam.Net.Data.OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Action<IEnumerable<WebHookDescriptorWebHookSubscriber>> batchProcessor, Bam.Net.Data.OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				WebHookDescriptorWebHookSubscriberColumns columns = new WebHookDescriptorWebHookSubscriberColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (WebHookDescriptorWebHookSubscriberColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static WebHookDescriptorWebHookSubscriber GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static WebHookDescriptorWebHookSubscriber GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static WebHookDescriptorWebHookSubscriber GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static WebHookDescriptorWebHookSubscriber GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static WebHookDescriptorWebHookSubscriber GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static WebHookDescriptorWebHookSubscriber GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriberCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static WebHookDescriptorWebHookSubscriberCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriberCollection Where(Func<WebHookDescriptorWebHookSubscriberColumns, QueryFilter<WebHookDescriptorWebHookSubscriberColumns>> where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			return new WebHookDescriptorWebHookSubscriberCollection(database.GetQuery<WebHookDescriptorWebHookSubscriberColumns, WebHookDescriptorWebHookSubscriber>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriberCollection Where(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database database = null)
		{		
			database = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			var results = new WebHookDescriptorWebHookSubscriberCollection(database, database.GetQuery<WebHookDescriptorWebHookSubscriberColumns, WebHookDescriptorWebHookSubscriber>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriberCollection Where(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			var results = new WebHookDescriptorWebHookSubscriberCollection(database, database.GetQuery<WebHookDescriptorWebHookSubscriberColumns, WebHookDescriptorWebHookSubscriber>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;WebHookDescriptorWebHookSubscriberColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static WebHookDescriptorWebHookSubscriberCollection Where(QiQuery where, Database database = null)
		{
			var results = new WebHookDescriptorWebHookSubscriberCollection(database, Select<WebHookDescriptorWebHookSubscriberColumns>.From<WebHookDescriptorWebHookSubscriber>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriber GetOneWhere(QueryFilter where, Database database = null)
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
		public static WebHookDescriptorWebHookSubscriber OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> whereDelegate = (c) => where;
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
		public static WebHookDescriptorWebHookSubscriber GetOneWhere(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				WebHookDescriptorWebHookSubscriberColumns c = new WebHookDescriptorWebHookSubscriberColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single WebHookDescriptorWebHookSubscriber instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriber OneWhere(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<WebHookDescriptorWebHookSubscriberColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static WebHookDescriptorWebHookSubscriber OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriber FirstOneWhere(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriber FirstOneWhere(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriber FirstOneWhere(QueryFilter where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriberCollection Top(int count, WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriberCollection Top(int count, WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy, Database database = null)
		{
			WebHookDescriptorWebHookSubscriberColumns c = new WebHookDescriptorWebHookSubscriberColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			QuerySet query = GetQuerySet(db); 
			query.Top<WebHookDescriptorWebHookSubscriber>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<WebHookDescriptorWebHookSubscriberColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<WebHookDescriptorWebHookSubscriberCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriberCollection Top(int count, QueryFilter where, Database database)
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
		public static WebHookDescriptorWebHookSubscriberCollection Top(int count, QueryFilter where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			QuerySet query = GetQuerySet(db);
			query.Top<WebHookDescriptorWebHookSubscriber>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<WebHookDescriptorWebHookSubscriberColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<WebHookDescriptorWebHookSubscriberCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static WebHookDescriptorWebHookSubscriberCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			QuerySet query = GetQuerySet(db);
			query.Top<WebHookDescriptorWebHookSubscriber>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<WebHookDescriptorWebHookSubscriberCollection>(0);
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
		public static WebHookDescriptorWebHookSubscriberCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			QuerySet query = GetQuerySet(db);
			query.Top<WebHookDescriptorWebHookSubscriber>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<WebHookDescriptorWebHookSubscriberCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of WebHookDescriptorWebHookSubscribers
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
            QuerySet query = GetQuerySet(db);
            query.Count<WebHookDescriptorWebHookSubscriber>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WebHookDescriptorWebHookSubscriberColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WebHookDescriptorWebHookSubscriberColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database database = null)
		{
			WebHookDescriptorWebHookSubscriberColumns c = new WebHookDescriptorWebHookSubscriberColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<WebHookDescriptorWebHookSubscriber>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<WebHookDescriptorWebHookSubscriber>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static WebHookDescriptorWebHookSubscriber CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<WebHookDescriptorWebHookSubscriber>();			
			var dao = new WebHookDescriptorWebHookSubscriber();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static WebHookDescriptorWebHookSubscriber OneOrThrow(WebHookDescriptorWebHookSubscriberCollection c)
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
