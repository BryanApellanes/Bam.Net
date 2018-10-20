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
	[Bam.Net.Data.Table("ServiceRegistryDescriptor", "ServiceRegistry")]
	public partial class ServiceRegistryDescriptor: Bam.Net.Data.Dao
	{
		public ServiceRegistryDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceRegistryDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceRegistryDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceRegistryDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ServiceRegistryDescriptor(DataRow data)
		{
			return new ServiceRegistryDescriptor(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("ServiceDescriptorServiceRegistryDescriptor_ServiceRegistryDescriptorId", new ServiceDescriptorServiceRegistryDescriptorCollection(Database.GetQuery<ServiceDescriptorServiceRegistryDescriptorColumns, ServiceDescriptorServiceRegistryDescriptor>((c) => c.ServiceRegistryDescriptorId == GetULongValue("Id")), this, "ServiceRegistryDescriptorId"));				
			}						
            this.ChildCollections.Add("ServiceRegistryDescriptor_ServiceDescriptorServiceRegistryDescriptor_ServiceDescriptor",  new XrefDaoCollection<ServiceDescriptorServiceRegistryDescriptor, ServiceDescriptor>(this, false));
				
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



				

	[Bam.Net.Exclude]	
	public ServiceDescriptorServiceRegistryDescriptorCollection ServiceDescriptorServiceRegistryDescriptorsByServiceRegistryDescriptorId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ServiceDescriptorServiceRegistryDescriptor_ServiceRegistryDescriptorId"))
			{
				SetChildren();
			}

			var c = (ServiceDescriptorServiceRegistryDescriptorCollection)this.ChildCollections["ServiceDescriptorServiceRegistryDescriptor_ServiceRegistryDescriptorId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<ServiceDescriptorServiceRegistryDescriptor, ServiceDescriptor> ServiceDescriptors
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("ServiceRegistryDescriptor_ServiceDescriptorServiceRegistryDescriptor_ServiceDescriptor"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ServiceDescriptorServiceRegistryDescriptor, ServiceDescriptor>)this.ChildCollections["ServiceRegistryDescriptor_ServiceDescriptorServiceRegistryDescriptor_ServiceDescriptor"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }		/// <summary>
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
				var colFilter = new ServiceRegistryDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ServiceRegistryDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ServiceRegistryDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<ServiceRegistryDescriptor>();
			var results = new ServiceRegistryDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ServiceRegistryDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ServiceRegistryDescriptorColumns columns = new ServiceRegistryDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<ServiceRegistryDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ServiceRegistryDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ServiceRegistryDescriptorColumns> where, Action<IEnumerable<ServiceRegistryDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ServiceRegistryDescriptorColumns columns = new ServiceRegistryDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<ServiceRegistryDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ServiceRegistryDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<ServiceRegistryDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<ServiceRegistryDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ServiceRegistryDescriptorColumns> where, Action<IEnumerable<ServiceRegistryDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<ServiceRegistryDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ServiceRegistryDescriptorColumns columns = new ServiceRegistryDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ServiceRegistryDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static ServiceRegistryDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static ServiceRegistryDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ServiceRegistryDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ServiceRegistryDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ServiceRegistryDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ServiceRegistryDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ServiceRegistryDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ServiceRegistryDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptorCollection Where(Func<ServiceRegistryDescriptorColumns, QueryFilter<ServiceRegistryDescriptorColumns>> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ServiceRegistryDescriptor>();
			return new ServiceRegistryDescriptorCollection(database.GetQuery<ServiceRegistryDescriptorColumns, ServiceRegistryDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptorCollection Where(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ServiceRegistryDescriptor>();
			var results = new ServiceRegistryDescriptorCollection(database, database.GetQuery<ServiceRegistryDescriptorColumns, ServiceRegistryDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptorCollection Where(WhereDelegate<ServiceRegistryDescriptorColumns> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ServiceRegistryDescriptor>();
			var results = new ServiceRegistryDescriptorCollection(database, database.GetQuery<ServiceRegistryDescriptorColumns, ServiceRegistryDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ServiceRegistryDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ServiceRegistryDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new ServiceRegistryDescriptorCollection(database, Select<ServiceRegistryDescriptorColumns>.From<ServiceRegistryDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static ServiceRegistryDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ServiceRegistryDescriptorColumns> whereDelegate = (c) => where;
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
		public static ServiceRegistryDescriptor GetOneWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ServiceRegistryDescriptorColumns c = new ServiceRegistryDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ServiceRegistryDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptor OneWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ServiceRegistryDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ServiceRegistryDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptor FirstOneWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptor FirstOneWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptor FirstOneWhere(QueryFilter where, OrderBy<ServiceRegistryDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ServiceRegistryDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptorCollection Top(int count, WhereDelegate<ServiceRegistryDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptorCollection Top(int count, WhereDelegate<ServiceRegistryDescriptorColumns> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy, Database database = null)
		{
			ServiceRegistryDescriptorColumns c = new ServiceRegistryDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ServiceRegistryDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ServiceRegistryDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceRegistryDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static ServiceRegistryDescriptorCollection Top(int count, QueryFilter where, OrderBy<ServiceRegistryDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceRegistryDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ServiceRegistryDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceRegistryDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ServiceRegistryDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceRegistryDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceRegistryDescriptorCollection>(0);
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
		public static ServiceRegistryDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceRegistryDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ServiceRegistryDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ServiceRegistryDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ServiceRegistryDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<ServiceRegistryDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceRegistryDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceRegistryDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database database = null)
		{
			ServiceRegistryDescriptorColumns c = new ServiceRegistryDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ServiceRegistryDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ServiceRegistryDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ServiceRegistryDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ServiceRegistryDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ServiceRegistryDescriptor>();			
			var dao = new ServiceRegistryDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ServiceRegistryDescriptor OneOrThrow(ServiceRegistryDescriptorCollection c)
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
