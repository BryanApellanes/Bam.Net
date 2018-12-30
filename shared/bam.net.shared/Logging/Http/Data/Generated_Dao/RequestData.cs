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

namespace Bam.Net.Logging.Http.Data.Dao
{
	// schema = HttpLogging
	// connection Name = HttpLogging
	[Serializable]
	[Bam.Net.Data.Table("RequestData", "HttpLogging")]
	public partial class RequestData: Bam.Net.Data.Dao
	{
		public RequestData():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public RequestData(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public RequestData(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public RequestData(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator RequestData(DataRow data)
		{
			return new RequestData(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("CookieData_RequestDataId", new CookieDataCollection(Database.GetQuery<CookieDataColumns, CookieData>((c) => c.RequestDataId == GetULongValue("Id")), this, "RequestDataId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("HeaderData_RequestDataId", new HeaderDataCollection(Database.GetQuery<HeaderDataColumns, HeaderData>((c) => c.RequestDataId == GetULongValue("Id")), this, "RequestDataId"));				
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

	// property:AcceptTypes, columnName:AcceptTypes	
	[Bam.Net.Data.Column(Name="AcceptTypes", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AcceptTypes
	{
		get
		{
			return GetStringValue("AcceptTypes");
		}
		set
		{
			SetValue("AcceptTypes", value);
		}
	}

	// property:ContentEncoding, columnName:ContentEncoding	
	[Bam.Net.Data.Column(Name="ContentEncoding", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ContentEncoding
	{
		get
		{
			return GetStringValue("ContentEncoding");
		}
		set
		{
			SetValue("ContentEncoding", value);
		}
	}

	// property:ContentLength, columnName:ContentLength	
	[Bam.Net.Data.Column(Name="ContentLength", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public long? ContentLength
	{
		get
		{
			return GetLongValue("ContentLength");
		}
		set
		{
			SetValue("ContentLength", value);
		}
	}

	// property:ContentType, columnName:ContentType	
	[Bam.Net.Data.Column(Name="ContentType", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ContentType
	{
		get
		{
			return GetStringValue("ContentType");
		}
		set
		{
			SetValue("ContentType", value);
		}
	}

	// property:HttpMethod, columnName:HttpMethod	
	[Bam.Net.Data.Column(Name="HttpMethod", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string HttpMethod
	{
		get
		{
			return GetStringValue("HttpMethod");
		}
		set
		{
			SetValue("HttpMethod", value);
		}
	}

	// property:UrlId, columnName:UrlId	
	[Bam.Net.Data.Column(Name="UrlId", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public ulong? UrlId
	{
		get
		{
			return GetULongValue("UrlId");
		}
		set
		{
			SetValue("UrlId", value);
		}
	}

	// property:UrlReferrerId, columnName:UrlReferrerId	
	[Bam.Net.Data.Column(Name="UrlReferrerId", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public ulong? UrlReferrerId
	{
		get
		{
			return GetULongValue("UrlReferrerId");
		}
		set
		{
			SetValue("UrlReferrerId", value);
		}
	}

	// property:UserAgent, columnName:UserAgent	
	[Bam.Net.Data.Column(Name="UserAgent", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string UserAgent
	{
		get
		{
			return GetStringValue("UserAgent");
		}
		set
		{
			SetValue("UserAgent", value);
		}
	}

	// property:UserHostAddress, columnName:UserHostAddress	
	[Bam.Net.Data.Column(Name="UserHostAddress", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string UserHostAddress
	{
		get
		{
			return GetStringValue("UserHostAddress");
		}
		set
		{
			SetValue("UserHostAddress", value);
		}
	}

	// property:UserHostName, columnName:UserHostName	
	[Bam.Net.Data.Column(Name="UserHostName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string UserHostName
	{
		get
		{
			return GetStringValue("UserHostName");
		}
		set
		{
			SetValue("UserHostName", value);
		}
	}

	// property:UserLanguages, columnName:UserLanguages	
	[Bam.Net.Data.Column(Name="UserLanguages", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string UserLanguages
	{
		get
		{
			return GetStringValue("UserLanguages");
		}
		set
		{
			SetValue("UserLanguages", value);
		}
	}

	// property:RawUrl, columnName:RawUrl	
	[Bam.Net.Data.Column(Name="RawUrl", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string RawUrl
	{
		get
		{
			return GetStringValue("RawUrl");
		}
		set
		{
			SetValue("RawUrl", value);
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



				

	[Bam.Net.Exclude]	
	public CookieDataCollection CookieDatasByRequestDataId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("CookieData_RequestDataId"))
			{
				SetChildren();
			}

			var c = (CookieDataCollection)this.ChildCollections["CookieData_RequestDataId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public HeaderDataCollection HeaderDatasByRequestDataId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("HeaderData_RequestDataId"))
			{
				SetChildren();
			}

			var c = (HeaderDataCollection)this.ChildCollections["HeaderData_RequestDataId"];
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
				var colFilter = new RequestDataColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the RequestData table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static RequestDataCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<RequestData>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<RequestData>();
			var results = new RequestDataCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<RequestData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				RequestDataColumns columns = new RequestDataColumns();
				var orderBy = Bam.Net.Data.Order.By<RequestDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<RequestData>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<RequestDataColumns> where, Action<IEnumerable<RequestData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				RequestDataColumns columns = new RequestDataColumns();
				var orderBy = Bam.Net.Data.Order.By<RequestDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (RequestDataColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<RequestData>> batchProcessor, Bam.Net.Data.OrderBy<RequestDataColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<RequestDataColumns> where, Action<IEnumerable<RequestData>> batchProcessor, Bam.Net.Data.OrderBy<RequestDataColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				RequestDataColumns columns = new RequestDataColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (RequestDataColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static RequestData GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static RequestData GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static RequestData GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static RequestData GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static RequestData GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static RequestData GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static RequestDataCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static RequestDataCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<RequestDataColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a RequestDataColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static RequestDataCollection Where(Func<RequestDataColumns, QueryFilter<RequestDataColumns>> where, OrderBy<RequestDataColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<RequestData>();
			return new RequestDataCollection(database.GetQuery<RequestDataColumns, RequestData>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static RequestDataCollection Where(WhereDelegate<RequestDataColumns> where, Database database = null)
		{		
			database = database ?? Db.For<RequestData>();
			var results = new RequestDataCollection(database, database.GetQuery<RequestDataColumns, RequestData>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static RequestDataCollection Where(WhereDelegate<RequestDataColumns> where, OrderBy<RequestDataColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<RequestData>();
			var results = new RequestDataCollection(database, database.GetQuery<RequestDataColumns, RequestData>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;RequestDataColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static RequestDataCollection Where(QiQuery where, Database database = null)
		{
			var results = new RequestDataCollection(database, Select<RequestDataColumns>.From<RequestData>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static RequestData GetOneWhere(QueryFilter where, Database database = null)
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
		public static RequestData OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<RequestDataColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<RequestDataColumns> where, Database database = null)
		{
			SetOneWhere(where, out RequestData ignore, database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<RequestDataColumns> where, out RequestData result, Database database = null)
		{
			result = GetOneWhere(where, database);
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static RequestData GetOneWhere(WhereDelegate<RequestDataColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				RequestDataColumns c = new RequestDataColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single RequestData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static RequestData OneWhere(WhereDelegate<RequestDataColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RequestDataColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static RequestData OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static RequestData FirstOneWhere(WhereDelegate<RequestDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static RequestData FirstOneWhere(WhereDelegate<RequestDataColumns> where, OrderBy<RequestDataColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static RequestData FirstOneWhere(QueryFilter where, OrderBy<RequestDataColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<RequestDataColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static RequestDataCollection Top(int count, WhereDelegate<RequestDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static RequestDataCollection Top(int count, WhereDelegate<RequestDataColumns> where, OrderBy<RequestDataColumns> orderBy, Database database = null)
		{
			RequestDataColumns c = new RequestDataColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<RequestData>();
			QuerySet query = GetQuerySet(db); 
			query.Top<RequestData>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<RequestDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<RequestDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static RequestDataCollection Top(int count, QueryFilter where, Database database)
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
		public static RequestDataCollection Top(int count, QueryFilter where, OrderBy<RequestDataColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<RequestData>();
			QuerySet query = GetQuerySet(db);
			query.Top<RequestData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<RequestDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<RequestDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static RequestDataCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<RequestData>();
			QuerySet query = GetQuerySet(db);
			query.Top<RequestData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<RequestDataCollection>(0);
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
		public static RequestDataCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<RequestData>();
			QuerySet query = GetQuerySet(db);
			query.Top<RequestData>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<RequestDataCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of RequestDatas
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<RequestData>();
            QuerySet query = GetQuerySet(db);
            query.Count<RequestData>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<RequestDataColumns> where, Database database = null)
		{
			RequestDataColumns c = new RequestDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<RequestData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<RequestData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<RequestData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<RequestData>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static RequestData CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<RequestData>();			
			var dao = new RequestData();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static RequestData OneOrThrow(RequestDataCollection c)
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
