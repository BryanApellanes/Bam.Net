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
	[Bam.Net.Data.Table("ServiceDescriptorServiceRegistryDescriptor", "ServiceRegistry")]
	public partial class ServiceDescriptorServiceRegistryDescriptor: Bam.Net.Data.Dao
	{
		public ServiceDescriptorServiceRegistryDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceDescriptorServiceRegistryDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceDescriptorServiceRegistryDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceDescriptorServiceRegistryDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ServiceDescriptorServiceRegistryDescriptor(DataRow data)
		{
			return new ServiceDescriptorServiceRegistryDescriptor(data);
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



	// start ServiceDescriptorId -> ServiceDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="ServiceDescriptorServiceRegistryDescriptor",
		Name="ServiceDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="ServiceDescriptor",
		Suffix="1")]
	public ulong? ServiceDescriptorId
	{
		get
		{
			return GetULongValue("ServiceDescriptorId");
		}
		set
		{
			SetValue("ServiceDescriptorId", value);
		}
	}

	ServiceDescriptor _serviceDescriptorOfServiceDescriptorId;
	public ServiceDescriptor ServiceDescriptorOfServiceDescriptorId
	{
		get
		{
			if(_serviceDescriptorOfServiceDescriptorId == null)
			{
				_serviceDescriptorOfServiceDescriptorId = Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.OneWhere(c => c.KeyColumn == this.ServiceDescriptorId, this.Database);
			}
			return _serviceDescriptorOfServiceDescriptorId;
		}
	}
	
	// start ServiceRegistryDescriptorId -> ServiceRegistryDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="ServiceDescriptorServiceRegistryDescriptor",
		Name="ServiceRegistryDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="ServiceRegistryDescriptor",
		Suffix="2")]
	public ulong? ServiceRegistryDescriptorId
	{
		get
		{
			return GetULongValue("ServiceRegistryDescriptorId");
		}
		set
		{
			SetValue("ServiceRegistryDescriptorId", value);
		}
	}

	ServiceRegistryDescriptor _serviceRegistryDescriptorOfServiceRegistryDescriptorId;
	public ServiceRegistryDescriptor ServiceRegistryDescriptorOfServiceRegistryDescriptorId
	{
		get
		{
			if(_serviceRegistryDescriptorOfServiceRegistryDescriptorId == null)
			{
				_serviceRegistryDescriptorOfServiceRegistryDescriptorId = Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.OneWhere(c => c.KeyColumn == this.ServiceRegistryDescriptorId, this.Database);
			}
			return _serviceRegistryDescriptorOfServiceRegistryDescriptorId;
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
				var colFilter = new ServiceDescriptorServiceRegistryDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ServiceDescriptorServiceRegistryDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ServiceDescriptorServiceRegistryDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<ServiceDescriptorServiceRegistryDescriptor>();
			var results = new ServiceDescriptorServiceRegistryDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ServiceDescriptorServiceRegistryDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ServiceDescriptorServiceRegistryDescriptorColumns columns = new ServiceDescriptorServiceRegistryDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<ServiceDescriptorServiceRegistryDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ServiceDescriptorServiceRegistryDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Action<IEnumerable<ServiceDescriptorServiceRegistryDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ServiceDescriptorServiceRegistryDescriptorColumns columns = new ServiceDescriptorServiceRegistryDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<ServiceDescriptorServiceRegistryDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ServiceDescriptorServiceRegistryDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<ServiceDescriptorServiceRegistryDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Action<IEnumerable<ServiceDescriptorServiceRegistryDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ServiceDescriptorServiceRegistryDescriptorColumns columns = new ServiceDescriptorServiceRegistryDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ServiceDescriptorServiceRegistryDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static ServiceDescriptorServiceRegistryDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static ServiceDescriptorServiceRegistryDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ServiceDescriptorServiceRegistryDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ServiceDescriptorServiceRegistryDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ServiceDescriptorServiceRegistryDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ServiceDescriptorServiceRegistryDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ServiceDescriptorServiceRegistryDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptorCollection Where(Func<ServiceDescriptorServiceRegistryDescriptorColumns, QueryFilter<ServiceDescriptorServiceRegistryDescriptorColumns>> where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			return new ServiceDescriptorServiceRegistryDescriptorCollection(database.GetQuery<ServiceDescriptorServiceRegistryDescriptorColumns, ServiceDescriptorServiceRegistryDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptorCollection Where(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			var results = new ServiceDescriptorServiceRegistryDescriptorCollection(database, database.GetQuery<ServiceDescriptorServiceRegistryDescriptorColumns, ServiceDescriptorServiceRegistryDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptorCollection Where(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			var results = new ServiceDescriptorServiceRegistryDescriptorCollection(database, database.GetQuery<ServiceDescriptorServiceRegistryDescriptorColumns, ServiceDescriptorServiceRegistryDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ServiceDescriptorServiceRegistryDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ServiceDescriptorServiceRegistryDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new ServiceDescriptorServiceRegistryDescriptorCollection(database, Select<ServiceDescriptorServiceRegistryDescriptorColumns>.From<ServiceDescriptorServiceRegistryDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static ServiceDescriptorServiceRegistryDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> whereDelegate = (c) => where;
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
		public static ServiceDescriptorServiceRegistryDescriptor GetOneWhere(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ServiceDescriptorServiceRegistryDescriptorColumns c = new ServiceDescriptorServiceRegistryDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ServiceDescriptorServiceRegistryDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptor OneWhere(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ServiceDescriptorServiceRegistryDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptor FirstOneWhere(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptor FirstOneWhere(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptor FirstOneWhere(QueryFilter where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptorCollection Top(int count, WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptorCollection Top(int count, WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy, Database database = null)
		{
			ServiceDescriptorServiceRegistryDescriptorColumns c = new ServiceDescriptorServiceRegistryDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ServiceDescriptorServiceRegistryDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceDescriptorServiceRegistryDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static ServiceDescriptorServiceRegistryDescriptorCollection Top(int count, QueryFilter where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceDescriptorServiceRegistryDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceDescriptorServiceRegistryDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ServiceDescriptorServiceRegistryDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceDescriptorServiceRegistryDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceDescriptorServiceRegistryDescriptorCollection>(0);
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
		public static ServiceDescriptorServiceRegistryDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceDescriptorServiceRegistryDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ServiceDescriptorServiceRegistryDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ServiceDescriptorServiceRegistryDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<ServiceDescriptorServiceRegistryDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceDescriptorServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceDescriptorServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database database = null)
		{
			ServiceDescriptorServiceRegistryDescriptorColumns c = new ServiceDescriptorServiceRegistryDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ServiceDescriptorServiceRegistryDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ServiceDescriptorServiceRegistryDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ServiceDescriptorServiceRegistryDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ServiceDescriptorServiceRegistryDescriptor>();			
			var dao = new ServiceDescriptorServiceRegistryDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ServiceDescriptorServiceRegistryDescriptor OneOrThrow(ServiceDescriptorServiceRegistryDescriptorCollection c)
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
