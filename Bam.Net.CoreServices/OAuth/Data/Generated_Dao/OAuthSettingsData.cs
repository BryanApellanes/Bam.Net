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

namespace Bam.Net.CoreServices.OAuth.Data.Dao
{
	// schema = OAuthSettings
	// connection Name = OAuthSettings
	[Serializable]
	[Bam.Net.Data.Table("OAuthSettingsData", "OAuthSettings")]
	public partial class OAuthSettingsData: Bam.Net.Data.Dao
	{
		public OAuthSettingsData():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public OAuthSettingsData(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public OAuthSettingsData(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public OAuthSettingsData(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator OAuthSettingsData(DataRow data)
		{
			return new OAuthSettingsData(data);
		}

		private void SetChildren()
		{
						
		}

	// property:Id, columnName:Id	
	[Bam.Net.Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="VarChar", MaxLength="4000")]
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

	// property:ApplicationName, columnName:ApplicationName	
	[Bam.Net.Data.Column(Name="ApplicationName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ApplicationName
	{
		get
		{
			return GetStringValue("ApplicationName");
		}
		set
		{
			SetValue("ApplicationName", value);
		}
	}

	// property:ApplicationIdentifier, columnName:ApplicationIdentifier	
	[Bam.Net.Data.Column(Name="ApplicationIdentifier", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ApplicationIdentifier
	{
		get
		{
			return GetStringValue("ApplicationIdentifier");
		}
		set
		{
			SetValue("ApplicationIdentifier", value);
		}
	}

	// property:ProviderName, columnName:ProviderName	
	[Bam.Net.Data.Column(Name="ProviderName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ProviderName
	{
		get
		{
			return GetStringValue("ProviderName");
		}
		set
		{
			SetValue("ProviderName", value);
		}
	}

	// property:State, columnName:State	
	[Bam.Net.Data.Column(Name="State", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string State
	{
		get
		{
			return GetStringValue("State");
		}
		set
		{
			SetValue("State", value);
		}
	}

	// property:Code, columnName:Code	
	[Bam.Net.Data.Column(Name="Code", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Code
	{
		get
		{
			return GetStringValue("Code");
		}
		set
		{
			SetValue("Code", value);
		}
	}

	// property:ClientId, columnName:ClientId	
	[Bam.Net.Data.Column(Name="ClientId", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ClientId
	{
		get
		{
			return GetStringValue("ClientId");
		}
		set
		{
			SetValue("ClientId", value);
		}
	}

	// property:ClientSecret, columnName:ClientSecret	
	[Bam.Net.Data.Column(Name="ClientSecret", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ClientSecret
	{
		get
		{
			return GetStringValue("ClientSecret");
		}
		set
		{
			SetValue("ClientSecret", value);
		}
	}

	// property:AuthorizationUrl, columnName:AuthorizationUrl	
	[Bam.Net.Data.Column(Name="AuthorizationUrl", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AuthorizationUrl
	{
		get
		{
			return GetStringValue("AuthorizationUrl");
		}
		set
		{
			SetValue("AuthorizationUrl", value);
		}
	}

	// property:AuthorizationCallbackUrl, columnName:AuthorizationCallbackUrl	
	[Bam.Net.Data.Column(Name="AuthorizationCallbackUrl", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AuthorizationCallbackUrl
	{
		get
		{
			return GetStringValue("AuthorizationCallbackUrl");
		}
		set
		{
			SetValue("AuthorizationCallbackUrl", value);
		}
	}

	// property:AuthorizationUrlFormat, columnName:AuthorizationUrlFormat	
	[Bam.Net.Data.Column(Name="AuthorizationUrlFormat", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AuthorizationUrlFormat
	{
		get
		{
			return GetStringValue("AuthorizationUrlFormat");
		}
		set
		{
			SetValue("AuthorizationUrlFormat", value);
		}
	}

	// property:AuthorizationCallbackUrlFormat, columnName:AuthorizationCallbackUrlFormat	
	[Bam.Net.Data.Column(Name="AuthorizationCallbackUrlFormat", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AuthorizationCallbackUrlFormat
	{
		get
		{
			return GetStringValue("AuthorizationCallbackUrlFormat");
		}
		set
		{
			SetValue("AuthorizationCallbackUrlFormat", value);
		}
	}

	// property:CreatedBy, columnName:CreatedBy	
	[Bam.Net.Data.Column(Name="CreatedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string CreatedBy
	{
		get
		{
			return GetStringValue("CreatedBy");
		}
		set
		{
			SetValue("CreatedBy", value);
		}
	}

	// property:ModifiedBy, columnName:ModifiedBy	
	[Bam.Net.Data.Column(Name="ModifiedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ModifiedBy
	{
		get
		{
			return GetStringValue("ModifiedBy");
		}
		set
		{
			SetValue("ModifiedBy", value);
		}
	}

	// property:Modified, columnName:Modified	
	[Bam.Net.Data.Column(Name="Modified", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Modified
	{
		get
		{
			return GetDateTimeValue("Modified");
		}
		set
		{
			SetValue("Modified", value);
		}
	}

	// property:Deleted, columnName:Deleted	
	[Bam.Net.Data.Column(Name="Deleted", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Deleted
	{
		get
		{
			return GetDateTimeValue("Deleted");
		}
		set
		{
			SetValue("Deleted", value);
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
				var colFilter = new OAuthSettingsDataColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the OAuthSettingsData table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static OAuthSettingsDataCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<OAuthSettingsData>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<OAuthSettingsData>();
			var results = new OAuthSettingsDataCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<OAuthSettingsData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				OAuthSettingsDataColumns columns = new OAuthSettingsDataColumns();
				var orderBy = Bam.Net.Data.Order.By<OAuthSettingsDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<OAuthSettingsData>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<OAuthSettingsDataColumns> where, Action<IEnumerable<OAuthSettingsData>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				OAuthSettingsDataColumns columns = new OAuthSettingsDataColumns();
				var orderBy = Bam.Net.Data.Order.By<OAuthSettingsDataColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (OAuthSettingsDataColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<OAuthSettingsData>> batchProcessor, Bam.Net.Data.OrderBy<OAuthSettingsDataColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<OAuthSettingsDataColumns> where, Action<IEnumerable<OAuthSettingsData>> batchProcessor, Bam.Net.Data.OrderBy<OAuthSettingsDataColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				OAuthSettingsDataColumns columns = new OAuthSettingsDataColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (OAuthSettingsDataColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static OAuthSettingsData GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static OAuthSettingsData GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static OAuthSettingsData GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static OAuthSettingsData GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static OAuthSettingsData GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static OAuthSettingsData GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static OAuthSettingsDataCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static OAuthSettingsDataCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<OAuthSettingsDataColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a OAuthSettingsDataColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static OAuthSettingsDataCollection Where(Func<OAuthSettingsDataColumns, QueryFilter<OAuthSettingsDataColumns>> where, OrderBy<OAuthSettingsDataColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<OAuthSettingsData>();
			return new OAuthSettingsDataCollection(database.GetQuery<OAuthSettingsDataColumns, OAuthSettingsData>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static OAuthSettingsDataCollection Where(WhereDelegate<OAuthSettingsDataColumns> where, Database database = null)
		{		
			database = database ?? Db.For<OAuthSettingsData>();
			var results = new OAuthSettingsDataCollection(database, database.GetQuery<OAuthSettingsDataColumns, OAuthSettingsData>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OAuthSettingsDataCollection Where(WhereDelegate<OAuthSettingsDataColumns> where, OrderBy<OAuthSettingsDataColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<OAuthSettingsData>();
			var results = new OAuthSettingsDataCollection(database, database.GetQuery<OAuthSettingsDataColumns, OAuthSettingsData>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;OAuthSettingsDataColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static OAuthSettingsDataCollection Where(QiQuery where, Database database = null)
		{
			var results = new OAuthSettingsDataCollection(database, Select<OAuthSettingsDataColumns>.From<OAuthSettingsData>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static OAuthSettingsData GetOneWhere(QueryFilter where, Database database = null)
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
		public static OAuthSettingsData OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<OAuthSettingsDataColumns> whereDelegate = (c) => where;
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
		public static OAuthSettingsData GetOneWhere(WhereDelegate<OAuthSettingsDataColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				OAuthSettingsDataColumns c = new OAuthSettingsDataColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single OAuthSettingsData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OAuthSettingsData OneWhere(WhereDelegate<OAuthSettingsDataColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<OAuthSettingsDataColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static OAuthSettingsData OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OAuthSettingsData FirstOneWhere(WhereDelegate<OAuthSettingsDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OAuthSettingsData FirstOneWhere(WhereDelegate<OAuthSettingsDataColumns> where, OrderBy<OAuthSettingsDataColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OAuthSettingsData FirstOneWhere(QueryFilter where, OrderBy<OAuthSettingsDataColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<OAuthSettingsDataColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OAuthSettingsDataCollection Top(int count, WhereDelegate<OAuthSettingsDataColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static OAuthSettingsDataCollection Top(int count, WhereDelegate<OAuthSettingsDataColumns> where, OrderBy<OAuthSettingsDataColumns> orderBy, Database database = null)
		{
			OAuthSettingsDataColumns c = new OAuthSettingsDataColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<OAuthSettingsData>();
			QuerySet query = GetQuerySet(db); 
			query.Top<OAuthSettingsData>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<OAuthSettingsDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<OAuthSettingsDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static OAuthSettingsDataCollection Top(int count, QueryFilter where, Database database)
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
		public static OAuthSettingsDataCollection Top(int count, QueryFilter where, OrderBy<OAuthSettingsDataColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<OAuthSettingsData>();
			QuerySet query = GetQuerySet(db);
			query.Top<OAuthSettingsData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<OAuthSettingsDataColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<OAuthSettingsDataCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static OAuthSettingsDataCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<OAuthSettingsData>();
			QuerySet query = GetQuerySet(db);
			query.Top<OAuthSettingsData>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<OAuthSettingsDataCollection>(0);
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
		public static OAuthSettingsDataCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<OAuthSettingsData>();
			QuerySet query = GetQuerySet(db);
			query.Top<OAuthSettingsData>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<OAuthSettingsDataCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of OAuthSettingsDatas
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<OAuthSettingsData>();
            QuerySet query = GetQuerySet(db);
            query.Count<OAuthSettingsData>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<OAuthSettingsDataColumns> where, Database database = null)
		{
			OAuthSettingsDataColumns c = new OAuthSettingsDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<OAuthSettingsData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<OAuthSettingsData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<OAuthSettingsData>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<OAuthSettingsData>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static OAuthSettingsData CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<OAuthSettingsData>();			
			var dao = new OAuthSettingsData();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static OAuthSettingsData OneOrThrow(OAuthSettingsDataCollection c)
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
