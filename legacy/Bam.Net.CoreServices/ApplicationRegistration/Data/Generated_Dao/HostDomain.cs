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

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
	// schema = ApplicationRegistration
	// connection Name = ApplicationRegistration
	[Serializable]
	[Bam.Net.Data.Table("HostDomain", "ApplicationRegistration")]
	public partial class HostDomain: Bam.Net.Data.Dao
	{
		public HostDomain():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HostDomain(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HostDomain(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HostDomain(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator HostDomain(DataRow data)
		{
			return new HostDomain(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("HostDomainApplication_HostDomainId", new HostDomainApplicationCollection(Database.GetQuery<HostDomainApplicationColumns, HostDomainApplication>((c) => c.HostDomainId == GetULongValue("Id")), this, "HostDomainId"));				
			}			
            this.ChildCollections.Add("HostDomain_HostDomainApplication_Application",  new XrefDaoCollection<HostDomainApplication, Application>(this, false));
							
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

	// property:DefaultApplicationName, columnName:DefaultApplicationName	
	[Bam.Net.Data.Column(Name="DefaultApplicationName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string DefaultApplicationName
	{
		get
		{
			return GetStringValue("DefaultApplicationName");
		}
		set
		{
			SetValue("DefaultApplicationName", value);
		}
	}

	// property:DomainName, columnName:DomainName	
	[Bam.Net.Data.Column(Name="DomainName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string DomainName
	{
		get
		{
			return GetStringValue("DomainName");
		}
		set
		{
			SetValue("DomainName", value);
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

	// property:Authorized, columnName:Authorized	
	[Bam.Net.Data.Column(Name="Authorized", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? Authorized
	{
		get
		{
			return GetBooleanValue("Authorized");
		}
		set
		{
			SetValue("Authorized", value);
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
	public HostDomainApplicationCollection HostDomainApplicationsByHostDomainId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("HostDomainApplication_HostDomainId"))
			{
				SetChildren();
			}

			var c = (HostDomainApplicationCollection)this.ChildCollections["HostDomainApplication_HostDomainId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<HostDomainApplication, Application> Applications
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("HostDomain_HostDomainApplication_Application"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<HostDomainApplication, Application>)this.ChildCollections["HostDomain_HostDomainApplication_Application"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
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
				var colFilter = new HostDomainColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the HostDomain table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static HostDomainCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<HostDomain>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<HostDomain>();
			var results = new HostDomainCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<HostDomain>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				HostDomainColumns columns = new HostDomainColumns();
				var orderBy = Bam.Net.Data.Order.By<HostDomainColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<HostDomain>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<HostDomainColumns> where, Action<IEnumerable<HostDomain>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				HostDomainColumns columns = new HostDomainColumns();
				var orderBy = Bam.Net.Data.Order.By<HostDomainColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (HostDomainColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<HostDomain>> batchProcessor, Bam.Net.Data.OrderBy<HostDomainColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<HostDomainColumns> where, Action<IEnumerable<HostDomain>> batchProcessor, Bam.Net.Data.OrderBy<HostDomainColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				HostDomainColumns columns = new HostDomainColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (HostDomainColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static HostDomain GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static HostDomain GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static HostDomain GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static HostDomain GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static HostDomain GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static HostDomain GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static HostDomainCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static HostDomainCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<HostDomainColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a HostDomainColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static HostDomainCollection Where(Func<HostDomainColumns, QueryFilter<HostDomainColumns>> where, OrderBy<HostDomainColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<HostDomain>();
			return new HostDomainCollection(database.GetQuery<HostDomainColumns, HostDomain>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static HostDomainCollection Where(WhereDelegate<HostDomainColumns> where, Database database = null)
		{		
			database = database ?? Db.For<HostDomain>();
			var results = new HostDomainCollection(database, database.GetQuery<HostDomainColumns, HostDomain>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomainCollection Where(WhereDelegate<HostDomainColumns> where, OrderBy<HostDomainColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<HostDomain>();
			var results = new HostDomainCollection(database, database.GetQuery<HostDomainColumns, HostDomain>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;HostDomainColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HostDomainCollection Where(QiQuery where, Database database = null)
		{
			var results = new HostDomainCollection(database, Select<HostDomainColumns>.From<HostDomain>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static HostDomain GetOneWhere(QueryFilter where, Database database = null)
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
		public static HostDomain OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<HostDomainColumns> whereDelegate = (c) => where;
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
		public static HostDomain GetOneWhere(WhereDelegate<HostDomainColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				HostDomainColumns c = new HostDomainColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single HostDomain instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomain OneWhere(WhereDelegate<HostDomainColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<HostDomainColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HostDomain OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomain FirstOneWhere(WhereDelegate<HostDomainColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomain FirstOneWhere(WhereDelegate<HostDomainColumns> where, OrderBy<HostDomainColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomain FirstOneWhere(QueryFilter where, OrderBy<HostDomainColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<HostDomainColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomainCollection Top(int count, WhereDelegate<HostDomainColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static HostDomainCollection Top(int count, WhereDelegate<HostDomainColumns> where, OrderBy<HostDomainColumns> orderBy, Database database = null)
		{
			HostDomainColumns c = new HostDomainColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<HostDomain>();
			QuerySet query = GetQuerySet(db); 
			query.Top<HostDomain>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HostDomainColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HostDomainCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static HostDomainCollection Top(int count, QueryFilter where, Database database)
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
		public static HostDomainCollection Top(int count, QueryFilter where, OrderBy<HostDomainColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<HostDomain>();
			QuerySet query = GetQuerySet(db);
			query.Top<HostDomain>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<HostDomainColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HostDomainCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static HostDomainCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<HostDomain>();
			QuerySet query = GetQuerySet(db);
			query.Top<HostDomain>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<HostDomainCollection>(0);
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
		public static HostDomainCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<HostDomain>();
			QuerySet query = GetQuerySet(db);
			query.Top<HostDomain>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<HostDomainCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of HostDomains
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<HostDomain>();
            QuerySet query = GetQuerySet(db);
            query.Count<HostDomain>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<HostDomainColumns> where, Database database = null)
		{
			HostDomainColumns c = new HostDomainColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<HostDomain>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<HostDomain>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<HostDomain>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<HostDomain>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static HostDomain CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<HostDomain>();			
			var dao = new HostDomain();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static HostDomain OneOrThrow(HostDomainCollection c)
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
