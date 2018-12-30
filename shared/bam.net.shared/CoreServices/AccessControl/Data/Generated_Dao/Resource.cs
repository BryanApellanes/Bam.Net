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

namespace Bam.Net.CoreServices.AccessControl.Data.Dao
{
	// schema = AccessControl
	// connection Name = AccessControl
	[Serializable]
	[Bam.Net.Data.Table("Resource", "AccessControl")]
	public partial class Resource: Bam.Net.Data.Dao
	{
		public Resource():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Resource(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Resource(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Resource(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Resource(DataRow data)
		{
			return new Resource(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("PermissionSpecification_ResourceId", new PermissionSpecificationCollection(Database.GetQuery<PermissionSpecificationColumns, PermissionSpecification>((c) => c.ResourceId == GetULongValue("Id")), this, "ResourceId"));				
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

	// property:ParentId, columnName:ParentId	
	[Bam.Net.Data.Column(Name="ParentId", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public ulong? ParentId
	{
		get
		{
			return GetULongValue("ParentId");
		}
		set
		{
			SetValue("ParentId", value);
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

	// property:FullPath, columnName:FullPath	
	[Bam.Net.Data.Column(Name="FullPath", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string FullPath
	{
		get
		{
			return GetStringValue("FullPath");
		}
		set
		{
			SetValue("FullPath", value);
		}
	}

	// property:Children, columnName:Children	
	[Bam.Net.Data.Column(Name="Children", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Children
	{
		get
		{
			return GetStringValue("Children");
		}
		set
		{
			SetValue("Children", value);
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



	// start ResourceHostId -> ResourceHostId
	[Bam.Net.Data.ForeignKey(
        Table="Resource",
		Name="ResourceHostId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="ResourceHost",
		Suffix="1")]
	public ulong? ResourceHostId
	{
		get
		{
			return GetULongValue("ResourceHostId");
		}
		set
		{
			SetValue("ResourceHostId", value);
		}
	}

	ResourceHost _resourceHostOfResourceHostId;
	public ResourceHost ResourceHostOfResourceHostId
	{
		get
		{
			if(_resourceHostOfResourceHostId == null)
			{
				_resourceHostOfResourceHostId = Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.OneWhere(c => c.KeyColumn == this.ResourceHostId, this.Database);
			}
			return _resourceHostOfResourceHostId;
		}
	}
	
				

	[Bam.Net.Exclude]	
	public PermissionSpecificationCollection PermissionSpecificationsByResourceId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("PermissionSpecification_ResourceId"))
			{
				SetChildren();
			}

			var c = (PermissionSpecificationCollection)this.ChildCollections["PermissionSpecification_ResourceId"];
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
				var colFilter = new ResourceColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Resource table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ResourceCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Resource>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Resource>();
			var results = new ResourceCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Resource>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ResourceColumns columns = new ResourceColumns();
				var orderBy = Bam.Net.Data.Order.By<ResourceColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Resource>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ResourceColumns> where, Action<IEnumerable<Resource>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ResourceColumns columns = new ResourceColumns();
				var orderBy = Bam.Net.Data.Order.By<ResourceColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ResourceColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Resource>> batchProcessor, Bam.Net.Data.OrderBy<ResourceColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ResourceColumns> where, Action<IEnumerable<Resource>> batchProcessor, Bam.Net.Data.OrderBy<ResourceColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ResourceColumns columns = new ResourceColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ResourceColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Resource GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Resource GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Resource GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Resource GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Resource GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Resource GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ResourceCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ResourceCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ResourceColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ResourceColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ResourceCollection Where(Func<ResourceColumns, QueryFilter<ResourceColumns>> where, OrderBy<ResourceColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Resource>();
			return new ResourceCollection(database.GetQuery<ResourceColumns, Resource>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ResourceCollection Where(WhereDelegate<ResourceColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Resource>();
			var results = new ResourceCollection(database, database.GetQuery<ResourceColumns, Resource>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ResourceCollection Where(WhereDelegate<ResourceColumns> where, OrderBy<ResourceColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Resource>();
			var results = new ResourceCollection(database, database.GetQuery<ResourceColumns, Resource>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ResourceColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ResourceCollection Where(QiQuery where, Database database = null)
		{
			var results = new ResourceCollection(database, Select<ResourceColumns>.From<Resource>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Resource GetOneWhere(QueryFilter where, Database database = null)
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
		public static Resource OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ResourceColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<ResourceColumns> where, Database database = null)
		{
			SetOneWhere(where, out Resource ignore, database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<ResourceColumns> where, out Resource result, Database database = null)
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
		public static Resource GetOneWhere(WhereDelegate<ResourceColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ResourceColumns c = new ResourceColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Resource instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Resource OneWhere(WhereDelegate<ResourceColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ResourceColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Resource OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Resource FirstOneWhere(WhereDelegate<ResourceColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Resource FirstOneWhere(WhereDelegate<ResourceColumns> where, OrderBy<ResourceColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Resource FirstOneWhere(QueryFilter where, OrderBy<ResourceColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ResourceColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ResourceCollection Top(int count, WhereDelegate<ResourceColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ResourceCollection Top(int count, WhereDelegate<ResourceColumns> where, OrderBy<ResourceColumns> orderBy, Database database = null)
		{
			ResourceColumns c = new ResourceColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Resource>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Resource>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ResourceColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ResourceCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ResourceCollection Top(int count, QueryFilter where, Database database)
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
		public static ResourceCollection Top(int count, QueryFilter where, OrderBy<ResourceColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Resource>();
			QuerySet query = GetQuerySet(db);
			query.Top<Resource>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ResourceColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ResourceCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ResourceCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Resource>();
			QuerySet query = GetQuerySet(db);
			query.Top<Resource>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ResourceCollection>(0);
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
		public static ResourceCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Resource>();
			QuerySet query = GetQuerySet(db);
			query.Top<Resource>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ResourceCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Resources
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Resource>();
            QuerySet query = GetQuerySet(db);
            query.Count<Resource>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ResourceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ResourceColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ResourceColumns> where, Database database = null)
		{
			ResourceColumns c = new ResourceColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Resource>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Resource>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Resource>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Resource>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Resource CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Resource>();			
			var dao = new Resource();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Resource OneOrThrow(ResourceCollection c)
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
