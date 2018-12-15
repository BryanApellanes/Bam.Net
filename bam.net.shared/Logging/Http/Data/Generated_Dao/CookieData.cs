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
	[Bam.Net.Data.Table("CookieData", "HttpLogging")]
	public partial class CookieData: Bam.Net.Data.Dao
	{
		public CookieData():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CookieData(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CookieData(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CookieData(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator CookieData(DataRow data)
		{
			return new CookieData(data);
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

	// property:TimeStamp, columnName:TimeStamp	
	[Bam.Net.Data.Column(Name="TimeStamp", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? TimeStamp
	{
		get
		{
			return GetDateTimeValue("TimeStamp");
		}
		set
		{
			SetValue("TimeStamp", value);
		}
	}

	// property:Secure, columnName:Secure	
	[Bam.Net.Data.Column(Name="Secure", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? Secure
	{
		get
		{
			return GetBooleanValue("Secure");
		}
		set
		{
			SetValue("Secure", value);
		}
	}

	// property:Port, columnName:Port	
	[Bam.Net.Data.Column(Name="Port", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Port
	{
		get
		{
			return GetStringValue("Port");
		}
		set
		{
			SetValue("Port", value);
		}
	}

	// property:Path, columnName:Path	
	[Bam.Net.Data.Column(Name="Path", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Path
	{
		get
		{
			return GetStringValue("Path");
		}
		set
		{
			SetValue("Path", value);
		}
	}

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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

	// property:HttpOnly, columnName:HttpOnly	
	[Bam.Net.Data.Column(Name="HttpOnly", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? HttpOnly
	{
		get
		{
			return GetBooleanValue("HttpOnly");
		}
		set
		{
			SetValue("HttpOnly", value);
		}
	}

	// property:Expires, columnName:Expires	
	[Bam.Net.Data.Column(Name="Expires", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Expires
	{
		get
		{
			return GetDateTimeValue("Expires");
		}
		set
		{
			SetValue("Expires", value);
		}
	}

	// property:Domain, columnName:Domain	
	[Bam.Net.Data.Column(Name="Domain", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Domain
	{
		get
		{
			return GetStringValue("Domain");
		}
		set
		{
			SetValue("Domain", value);
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

	// property:Discard, columnName:Discard	
	[Bam.Net.Data.Column(Name="Discard", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? Discard
	{
		get
		{
			return GetBooleanValue("Discard");
		}
		set
		{
			SetValue("Discard", value);
		}
	}

	// property:UriId, columnName:UriId	
	[Bam.Net.Data.Column(Name="UriId", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public ulong? UriId
	{
		get
		{
			return GetULongValue("UriId");
		}
		set
		{
			SetValue("UriId", value);
		}
	}

	// property:Comment, columnName:Comment	
	[Bam.Net.Data.Column(Name="Comment", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Comment
	{
		get
		{
			return GetStringValue("Comment");
		}
		set
		{
			SetValue("Comment", value);
		}
	}

	// property:Expired, columnName:Expired	
	[Bam.Net.Data.Column(Name="Expired", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? Expired
	{
		get
		{
			return GetBooleanValue("Expired");
		}
		set
		{
			SetValue("Expired", value);
		}
	}

	// property:Version, columnName:Version	
	[Bam.Net.Data.Column(Name="Version", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? Version
	{
		get
		{
			return GetIntValue("Version");
		}
		set
		{
			SetValue("Version", value);
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



	// start RequestDataId -> RequestDataId
	[Bam.Net.Data.ForeignKey(
        Table="CookieData",
		Name="RequestDataId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="RequestData",
		Suffix="1")]
	public ulong? RequestDataId
	{
		get
		{
			return GetULongValue("RequestDataId");
		}
		set
		{
			SetValue("RequestDataId", value);
		}
	}

	RequestData _requestDataOfRequestDataId;
	public RequestData RequestDataOfRequestDataId
	{
		get
		{
			if(_requestDataOfRequestDataId == null)
			{
				_requestDataOfRequestDataId = Bam.Net.Logging.Http.Data.Dao.RequestData.OneWhere(c => c.KeyColumn == this.RequestDataId, this.Database);
			}
			return _requestDataOfRequestDataId;
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
				var colFilter = new CookieDataColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the CookieData table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CookieDataCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<CookieData>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<CookieData>();
			var results = new CookieDataCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<CookieData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CookieDataColumns columns = new CookieDataColumns();
				var orderBy = Bam.Net.Data.Order.By<CookieDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<CookieData>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<CookieDataColumns> where, Action<IEnumerable<CookieData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CookieDataColumns columns = new CookieDataColumns();
				var orderBy = Bam.Net.Data.Order.By<CookieDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (CookieDataColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<CookieData>> batchProcessor, Bam.Net.Data.OrderBy<CookieDataColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<CookieDataColumns> where, Action<IEnumerable<CookieData>> batchProcessor, Bam.Net.Data.OrderBy<CookieDataColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CookieDataColumns columns = new CookieDataColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (CookieDataColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static CookieData GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static CookieData GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static CookieData GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static CookieData GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static CookieData GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static CookieData GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static CookieDataCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static CookieDataCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<CookieDataColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CookieDataColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CookieDataCollection Where(Func<CookieDataColumns, QueryFilter<CookieDataColumns>> where, OrderBy<CookieDataColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<CookieData>();
			return new CookieDataCollection(database.GetQuery<CookieDataColumns, CookieData>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CookieDataCollection Where(WhereDelegate<CookieDataColumns> where, Database database = null)
		{		
			database = database ?? Db.For<CookieData>();
			var results = new CookieDataCollection(database, database.GetQuery<CookieDataColumns, CookieData>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CookieDataCollection Where(WhereDelegate<CookieDataColumns> where, OrderBy<CookieDataColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<CookieData>();
			var results = new CookieDataCollection(database, database.GetQuery<CookieDataColumns, CookieData>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;CookieDataColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CookieDataCollection Where(QiQuery where, Database database = null)
		{
			var results = new CookieDataCollection(database, Select<CookieDataColumns>.From<CookieData>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static CookieData GetOneWhere(QueryFilter where, Database database = null)
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
		public static CookieData OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<CookieDataColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<CookieDataColumns> where, Database database = null)
		{
			SetOneWhere(where, out CookieData ignore, database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<CookieDataColumns> where, out CookieData result, Database database = null)
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
		public static CookieData GetOneWhere(WhereDelegate<CookieDataColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				CookieDataColumns c = new CookieDataColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single CookieData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CookieData OneWhere(WhereDelegate<CookieDataColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CookieDataColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CookieData OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CookieData FirstOneWhere(WhereDelegate<CookieDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CookieData FirstOneWhere(WhereDelegate<CookieDataColumns> where, OrderBy<CookieDataColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CookieData FirstOneWhere(QueryFilter where, OrderBy<CookieDataColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<CookieDataColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CookieDataCollection Top(int count, WhereDelegate<CookieDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static CookieDataCollection Top(int count, WhereDelegate<CookieDataColumns> where, OrderBy<CookieDataColumns> orderBy, Database database = null)
		{
			CookieDataColumns c = new CookieDataColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<CookieData>();
			QuerySet query = GetQuerySet(db); 
			query.Top<CookieData>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CookieDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CookieDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static CookieDataCollection Top(int count, QueryFilter where, Database database)
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
		public static CookieDataCollection Top(int count, QueryFilter where, OrderBy<CookieDataColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<CookieData>();
			QuerySet query = GetQuerySet(db);
			query.Top<CookieData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<CookieDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CookieDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static CookieDataCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<CookieData>();
			QuerySet query = GetQuerySet(db);
			query.Top<CookieData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<CookieDataCollection>(0);
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
		public static CookieDataCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<CookieData>();
			QuerySet query = GetQuerySet(db);
			query.Top<CookieData>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<CookieDataCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of CookieDatas
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<CookieData>();
            QuerySet query = GetQuerySet(db);
            query.Count<CookieData>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<CookieDataColumns> where, Database database = null)
		{
			CookieDataColumns c = new CookieDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<CookieData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<CookieData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<CookieData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<CookieData>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static CookieData CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<CookieData>();			
			var dao = new CookieData();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static CookieData OneOrThrow(CookieDataCollection c)
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
