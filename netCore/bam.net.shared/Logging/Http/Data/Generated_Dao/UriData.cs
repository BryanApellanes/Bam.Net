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
	[Bam.Net.Data.Table("UriData", "HttpLogging")]
	public partial class UriData: Bam.Net.Data.Dao
	{
		public UriData():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UriData(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UriData(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UriData(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator UriData(DataRow data)
		{
			return new UriData(data);
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

	// property:IsDefaultPort, columnName:IsDefaultPort	
	[Bam.Net.Data.Column(Name="IsDefaultPort", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsDefaultPort
	{
		get
		{
			return GetBooleanValue("IsDefaultPort");
		}
		set
		{
			SetValue("IsDefaultPort", value);
		}
	}

	// property:Authority, columnName:Authority	
	[Bam.Net.Data.Column(Name="Authority", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Authority
	{
		get
		{
			return GetStringValue("Authority");
		}
		set
		{
			SetValue("Authority", value);
		}
	}

	// property:DnsSafeHost, columnName:DnsSafeHost	
	[Bam.Net.Data.Column(Name="DnsSafeHost", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string DnsSafeHost
	{
		get
		{
			return GetStringValue("DnsSafeHost");
		}
		set
		{
			SetValue("DnsSafeHost", value);
		}
	}

	// property:Fragment, columnName:Fragment	
	[Bam.Net.Data.Column(Name="Fragment", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Fragment
	{
		get
		{
			return GetStringValue("Fragment");
		}
		set
		{
			SetValue("Fragment", value);
		}
	}

	// property:Host, columnName:Host	
	[Bam.Net.Data.Column(Name="Host", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Host
	{
		get
		{
			return GetStringValue("Host");
		}
		set
		{
			SetValue("Host", value);
		}
	}

	// property:IdnHost, columnName:IdnHost	
	[Bam.Net.Data.Column(Name="IdnHost", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string IdnHost
	{
		get
		{
			return GetStringValue("IdnHost");
		}
		set
		{
			SetValue("IdnHost", value);
		}
	}

	// property:IsAbsoluteUri, columnName:IsAbsoluteUri	
	[Bam.Net.Data.Column(Name="IsAbsoluteUri", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsAbsoluteUri
	{
		get
		{
			return GetBooleanValue("IsAbsoluteUri");
		}
		set
		{
			SetValue("IsAbsoluteUri", value);
		}
	}

	// property:IsFile, columnName:IsFile	
	[Bam.Net.Data.Column(Name="IsFile", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsFile
	{
		get
		{
			return GetBooleanValue("IsFile");
		}
		set
		{
			SetValue("IsFile", value);
		}
	}

	// property:IsUnc, columnName:IsUnc	
	[Bam.Net.Data.Column(Name="IsUnc", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsUnc
	{
		get
		{
			return GetBooleanValue("IsUnc");
		}
		set
		{
			SetValue("IsUnc", value);
		}
	}

	// property:LocalPath, columnName:LocalPath	
	[Bam.Net.Data.Column(Name="LocalPath", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string LocalPath
	{
		get
		{
			return GetStringValue("LocalPath");
		}
		set
		{
			SetValue("LocalPath", value);
		}
	}

	// property:OriginalString, columnName:OriginalString	
	[Bam.Net.Data.Column(Name="OriginalString", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string OriginalString
	{
		get
		{
			return GetStringValue("OriginalString");
		}
		set
		{
			SetValue("OriginalString", value);
		}
	}

	// property:PathAndQuery, columnName:PathAndQuery	
	[Bam.Net.Data.Column(Name="PathAndQuery", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string PathAndQuery
	{
		get
		{
			return GetStringValue("PathAndQuery");
		}
		set
		{
			SetValue("PathAndQuery", value);
		}
	}

	// property:Port, columnName:Port	
	[Bam.Net.Data.Column(Name="Port", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? Port
	{
		get
		{
			return GetIntValue("Port");
		}
		set
		{
			SetValue("Port", value);
		}
	}

	// property:QueryString, columnName:QueryString	
	[Bam.Net.Data.Column(Name="QueryString", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string QueryString
	{
		get
		{
			return GetStringValue("QueryString");
		}
		set
		{
			SetValue("QueryString", value);
		}
	}

	// property:Scheme, columnName:Scheme	
	[Bam.Net.Data.Column(Name="Scheme", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Scheme
	{
		get
		{
			return GetStringValue("Scheme");
		}
		set
		{
			SetValue("Scheme", value);
		}
	}

	// property:AbsoluteUri, columnName:AbsoluteUri	
	[Bam.Net.Data.Column(Name="AbsoluteUri", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AbsoluteUri
	{
		get
		{
			return GetStringValue("AbsoluteUri");
		}
		set
		{
			SetValue("AbsoluteUri", value);
		}
	}

	// property:IsLoopback, columnName:IsLoopback	
	[Bam.Net.Data.Column(Name="IsLoopback", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsLoopback
	{
		get
		{
			return GetBooleanValue("IsLoopback");
		}
		set
		{
			SetValue("IsLoopback", value);
		}
	}

	// property:AbsolutePath, columnName:AbsolutePath	
	[Bam.Net.Data.Column(Name="AbsolutePath", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AbsolutePath
	{
		get
		{
			return GetStringValue("AbsolutePath");
		}
		set
		{
			SetValue("AbsolutePath", value);
		}
	}

	// property:UserInfo, columnName:UserInfo	
	[Bam.Net.Data.Column(Name="UserInfo", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string UserInfo
	{
		get
		{
			return GetStringValue("UserInfo");
		}
		set
		{
			SetValue("UserInfo", value);
		}
	}

	// property:UserEscaped, columnName:UserEscaped	
	[Bam.Net.Data.Column(Name="UserEscaped", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? UserEscaped
	{
		get
		{
			return GetBooleanValue("UserEscaped");
		}
		set
		{
			SetValue("UserEscaped", value);
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
				var colFilter = new UriDataColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the UriData table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static UriDataCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<UriData>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<UriData>();
			var results = new UriDataCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<UriData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				UriDataColumns columns = new UriDataColumns();
				var orderBy = Bam.Net.Data.Order.By<UriDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<UriData>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<UriDataColumns> where, Action<IEnumerable<UriData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				UriDataColumns columns = new UriDataColumns();
				var orderBy = Bam.Net.Data.Order.By<UriDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (UriDataColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<UriData>> batchProcessor, Bam.Net.Data.OrderBy<UriDataColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<UriDataColumns> where, Action<IEnumerable<UriData>> batchProcessor, Bam.Net.Data.OrderBy<UriDataColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				UriDataColumns columns = new UriDataColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (UriDataColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static UriData GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static UriData GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static UriData GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static UriData GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static UriData GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static UriData GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static UriDataCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static UriDataCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<UriDataColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UriDataColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static UriDataCollection Where(Func<UriDataColumns, QueryFilter<UriDataColumns>> where, OrderBy<UriDataColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<UriData>();
			return new UriDataCollection(database.GetQuery<UriDataColumns, UriData>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static UriDataCollection Where(WhereDelegate<UriDataColumns> where, Database database = null)
		{		
			database = database ?? Db.For<UriData>();
			var results = new UriDataCollection(database, database.GetQuery<UriDataColumns, UriData>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UriDataCollection Where(WhereDelegate<UriDataColumns> where, OrderBy<UriDataColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<UriData>();
			var results = new UriDataCollection(database, database.GetQuery<UriDataColumns, UriData>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;UriDataColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static UriDataCollection Where(QiQuery where, Database database = null)
		{
			var results = new UriDataCollection(database, Select<UriDataColumns>.From<UriData>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static UriData GetOneWhere(QueryFilter where, Database database = null)
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
		public static UriData OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<UriDataColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<UriDataColumns> where, Database database = null)
		{
			SetOneWhere(where, out UriData ignore, database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<UriDataColumns> where, out UriData result, Database database = null)
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
		public static UriData GetOneWhere(WhereDelegate<UriDataColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				UriDataColumns c = new UriDataColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UriData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UriData OneWhere(WhereDelegate<UriDataColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UriDataColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static UriData OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UriData FirstOneWhere(WhereDelegate<UriDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UriData FirstOneWhere(WhereDelegate<UriDataColumns> where, OrderBy<UriDataColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UriData FirstOneWhere(QueryFilter where, OrderBy<UriDataColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<UriDataColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UriDataCollection Top(int count, WhereDelegate<UriDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static UriDataCollection Top(int count, WhereDelegate<UriDataColumns> where, OrderBy<UriDataColumns> orderBy, Database database = null)
		{
			UriDataColumns c = new UriDataColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<UriData>();
			QuerySet query = GetQuerySet(db); 
			query.Top<UriData>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UriDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UriDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static UriDataCollection Top(int count, QueryFilter where, Database database)
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
		public static UriDataCollection Top(int count, QueryFilter where, OrderBy<UriDataColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<UriData>();
			QuerySet query = GetQuerySet(db);
			query.Top<UriData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<UriDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UriDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static UriDataCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<UriData>();
			QuerySet query = GetQuerySet(db);
			query.Top<UriData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<UriDataCollection>(0);
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
		public static UriDataCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<UriData>();
			QuerySet query = GetQuerySet(db);
			query.Top<UriData>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<UriDataCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of UriDatas
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<UriData>();
            QuerySet query = GetQuerySet(db);
            query.Count<UriData>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<UriDataColumns> where, Database database = null)
		{
			UriDataColumns c = new UriDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<UriData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<UriData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<UriData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<UriData>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static UriData CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<UriData>();			
			var dao = new UriData();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static UriData OneOrThrow(UriDataCollection c)
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
