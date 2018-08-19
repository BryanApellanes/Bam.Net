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

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
	// schema = ServiceRegistry
	// connection Name = ServiceRegistry
	[Serializable]
	[Bam.Net.Data.Table("ServiceRegistryLoaderDescriptor", "ServiceRegistry")]
	public partial class ServiceRegistryLoaderDescriptor: Bam.Net.Data.Dao
	{
		public ServiceRegistryLoaderDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceRegistryLoaderDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceRegistryLoaderDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceRegistryLoaderDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ServiceRegistryLoaderDescriptor(DataRow data)
		{
			return new ServiceRegistryLoaderDescriptor(data);
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

	// property:Description, columnName:Description	
	[Bam.Net.Data.Column(Name="Description", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Description
	{
		get
		{
			return GetStringValue("Description");
		}
		set
		{
			SetValue("Description", value);
		}
	}

	// property:LoaderType, columnName:LoaderType	
	[Bam.Net.Data.Column(Name="LoaderType", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string LoaderType
	{
		get
		{
			return GetStringValue("LoaderType");
		}
		set
		{
			SetValue("LoaderType", value);
		}
	}

	// property:LoaderAssembly, columnName:LoaderAssembly	
	[Bam.Net.Data.Column(Name="LoaderAssembly", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string LoaderAssembly
	{
		get
		{
			return GetStringValue("LoaderAssembly");
		}
		set
		{
			SetValue("LoaderAssembly", value);
		}
	}

	// property:LoaderMethod, columnName:LoaderMethod	
	[Bam.Net.Data.Column(Name="LoaderMethod", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string LoaderMethod
	{
		get
		{
			return GetStringValue("LoaderMethod");
		}
		set
		{
			SetValue("LoaderMethod", value);
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
				var colFilter = new ServiceRegistryLoaderDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ServiceRegistryLoaderDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ServiceRegistryLoaderDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<ServiceRegistryLoaderDescriptor>();
			var results = new ServiceRegistryLoaderDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ServiceRegistryLoaderDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ServiceRegistryLoaderDescriptorColumns columns = new ServiceRegistryLoaderDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<ServiceRegistryLoaderDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ServiceRegistryLoaderDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Action<IEnumerable<ServiceRegistryLoaderDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ServiceRegistryLoaderDescriptorColumns columns = new ServiceRegistryLoaderDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<ServiceRegistryLoaderDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ServiceRegistryLoaderDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<ServiceRegistryLoaderDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Action<IEnumerable<ServiceRegistryLoaderDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ServiceRegistryLoaderDescriptorColumns columns = new ServiceRegistryLoaderDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ServiceRegistryLoaderDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static ServiceRegistryLoaderDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static ServiceRegistryLoaderDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ServiceRegistryLoaderDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ServiceRegistryLoaderDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ServiceRegistryLoaderDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ServiceRegistryLoaderDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ServiceRegistryLoaderDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ServiceRegistryLoaderDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptorCollection Where(Func<ServiceRegistryLoaderDescriptorColumns, QueryFilter<ServiceRegistryLoaderDescriptorColumns>> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			return new ServiceRegistryLoaderDescriptorCollection(database.GetQuery<ServiceRegistryLoaderDescriptorColumns, ServiceRegistryLoaderDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptorCollection Where(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			var results = new ServiceRegistryLoaderDescriptorCollection(database, database.GetQuery<ServiceRegistryLoaderDescriptorColumns, ServiceRegistryLoaderDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptorCollection Where(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			var results = new ServiceRegistryLoaderDescriptorCollection(database, database.GetQuery<ServiceRegistryLoaderDescriptorColumns, ServiceRegistryLoaderDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ServiceRegistryLoaderDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ServiceRegistryLoaderDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new ServiceRegistryLoaderDescriptorCollection(database, Select<ServiceRegistryLoaderDescriptorColumns>.From<ServiceRegistryLoaderDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static ServiceRegistryLoaderDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ServiceRegistryLoaderDescriptorColumns> whereDelegate = (c) => where;
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
		public static ServiceRegistryLoaderDescriptor GetOneWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ServiceRegistryLoaderDescriptorColumns c = new ServiceRegistryLoaderDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ServiceRegistryLoaderDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptor OneWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ServiceRegistryLoaderDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ServiceRegistryLoaderDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptor FirstOneWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptor FirstOneWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptor FirstOneWhere(QueryFilter where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ServiceRegistryLoaderDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptorCollection Top(int count, WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptorCollection Top(int count, WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy, Database database = null)
		{
			ServiceRegistryLoaderDescriptorColumns c = new ServiceRegistryLoaderDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ServiceRegistryLoaderDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ServiceRegistryLoaderDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceRegistryLoaderDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static ServiceRegistryLoaderDescriptorCollection Top(int count, QueryFilter where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceRegistryLoaderDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ServiceRegistryLoaderDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceRegistryLoaderDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ServiceRegistryLoaderDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceRegistryLoaderDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceRegistryLoaderDescriptorCollection>(0);
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
		public static ServiceRegistryLoaderDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceRegistryLoaderDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ServiceRegistryLoaderDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ServiceRegistryLoaderDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<ServiceRegistryLoaderDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryLoaderDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryLoaderDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database database = null)
		{
			ServiceRegistryLoaderDescriptorColumns c = new ServiceRegistryLoaderDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ServiceRegistryLoaderDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ServiceRegistryLoaderDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ServiceRegistryLoaderDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ServiceRegistryLoaderDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryLoaderDescriptor>();			
			var dao = new ServiceRegistryLoaderDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ServiceRegistryLoaderDescriptor OneOrThrow(ServiceRegistryLoaderDescriptorCollection c)
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
