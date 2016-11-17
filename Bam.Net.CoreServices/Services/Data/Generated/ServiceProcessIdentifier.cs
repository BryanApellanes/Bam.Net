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

namespace Bam.Net.CoreServices.Data.Daos
{
	// schema = CoreRegistry
	// connection Name = CoreRegistry
	[Serializable]
	[Bam.Net.Data.Table("ServiceProcessIdentifier", "CoreRegistry")]
	public partial class ServiceProcessIdentifier: Dao
	{
		public ServiceProcessIdentifier():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceProcessIdentifier(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceProcessIdentifier(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ServiceProcessIdentifier(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ServiceProcessIdentifier(DataRow data)
		{
			return new ServiceProcessIdentifier(data);
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

	// property:MachineName, columnName:MachineName	
	[Bam.Net.Data.Column(Name="MachineName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string MachineName
	{
		get
		{
			return GetStringValue("MachineName");
		}
		set
		{
			SetValue("MachineName", value);
		}
	}

	// property:ProcessId, columnName:ProcessId	
	[Bam.Net.Data.Column(Name="ProcessId", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? ProcessId
	{
		get
		{
			return GetIntValue("ProcessId");
		}
		set
		{
			SetValue("ProcessId", value);
		}
	}

	// property:FilePath, columnName:FilePath	
	[Bam.Net.Data.Column(Name="FilePath", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string FilePath
	{
		get
		{
			return GetStringValue("FilePath");
		}
		set
		{
			SetValue("FilePath", value);
		}
	}

	// property:CommandLine, columnName:CommandLine	
	[Bam.Net.Data.Column(Name="CommandLine", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string CommandLine
	{
		get
		{
			return GetStringValue("CommandLine");
		}
		set
		{
			SetValue("CommandLine", value);
		}
	}

	// property:IpAddresses, columnName:IpAddresses	
	[Bam.Net.Data.Column(Name="IpAddresses", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string IpAddresses
	{
		get
		{
			return GetStringValue("IpAddresses");
		}
		set
		{
			SetValue("IpAddresses", value);
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
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new ServiceProcessIdentifierColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ServiceProcessIdentifier table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ServiceProcessIdentifierCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ServiceProcessIdentifier>();
			Database db = database ?? Db.For<ServiceProcessIdentifier>();
			var results = new ServiceProcessIdentifierCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ServiceProcessIdentifier>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ServiceProcessIdentifierColumns columns = new ServiceProcessIdentifierColumns();
				var orderBy = Bam.Net.Data.Order.By<ServiceProcessIdentifierColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}
			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ServiceProcessIdentifier>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ServiceProcessIdentifierColumns> where, Action<IEnumerable<ServiceProcessIdentifier>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ServiceProcessIdentifierColumns columns = new ServiceProcessIdentifierColumns();
				var orderBy = Bam.Net.Data.Order.By<ServiceProcessIdentifierColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ServiceProcessIdentifierColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static ServiceProcessIdentifier GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ServiceProcessIdentifier GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ServiceProcessIdentifier GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ServiceProcessIdentifier GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ServiceProcessIdentifierCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ServiceProcessIdentifierCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ServiceProcessIdentifierColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifierCollection Where(Func<ServiceProcessIdentifierColumns, QueryFilter<ServiceProcessIdentifierColumns>> where, OrderBy<ServiceProcessIdentifierColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ServiceProcessIdentifier>();
			return new ServiceProcessIdentifierCollection(database.GetQuery<ServiceProcessIdentifierColumns, ServiceProcessIdentifier>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifierCollection Where(WhereDelegate<ServiceProcessIdentifierColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ServiceProcessIdentifier>();
			var results = new ServiceProcessIdentifierCollection(database, database.GetQuery<ServiceProcessIdentifierColumns, ServiceProcessIdentifier>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifierCollection Where(WhereDelegate<ServiceProcessIdentifierColumns> where, OrderBy<ServiceProcessIdentifierColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ServiceProcessIdentifier>();
			var results = new ServiceProcessIdentifierCollection(database, database.GetQuery<ServiceProcessIdentifierColumns, ServiceProcessIdentifier>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ServiceProcessIdentifierColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ServiceProcessIdentifierCollection Where(QiQuery where, Database database = null)
		{
			var results = new ServiceProcessIdentifierCollection(database, Select<ServiceProcessIdentifierColumns>.From<ServiceProcessIdentifier>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifier GetOneWhere(QueryFilter where, Database database = null)
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
		public static ServiceProcessIdentifier OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ServiceProcessIdentifierColumns> whereDelegate = (c) => where;
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
		public static ServiceProcessIdentifier GetOneWhere(WhereDelegate<ServiceProcessIdentifierColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ServiceProcessIdentifierColumns c = new ServiceProcessIdentifierColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ServiceProcessIdentifier instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifier OneWhere(WhereDelegate<ServiceProcessIdentifierColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ServiceProcessIdentifierColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ServiceProcessIdentifier OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifier FirstOneWhere(WhereDelegate<ServiceProcessIdentifierColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifier FirstOneWhere(WhereDelegate<ServiceProcessIdentifierColumns> where, OrderBy<ServiceProcessIdentifierColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifier FirstOneWhere(QueryFilter where, OrderBy<ServiceProcessIdentifierColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ServiceProcessIdentifierColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifierCollection Top(int count, WhereDelegate<ServiceProcessIdentifierColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifierCollection Top(int count, WhereDelegate<ServiceProcessIdentifierColumns> where, OrderBy<ServiceProcessIdentifierColumns> orderBy, Database database = null)
		{
			ServiceProcessIdentifierColumns c = new ServiceProcessIdentifierColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ServiceProcessIdentifier>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ServiceProcessIdentifier>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ServiceProcessIdentifierColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceProcessIdentifierCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ServiceProcessIdentifierCollection Top(int count, QueryFilter where, Database database)
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
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ServiceProcessIdentifierCollection Top(int count, QueryFilter where, OrderBy<ServiceProcessIdentifierColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ServiceProcessIdentifier>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceProcessIdentifier>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ServiceProcessIdentifierColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ServiceProcessIdentifierCollection>(0);
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
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ServiceProcessIdentifierCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ServiceProcessIdentifier>();
			QuerySet query = GetQuerySet(db);
			query.Top<ServiceProcessIdentifier>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ServiceProcessIdentifierCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ServiceProcessIdentifiers
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ServiceProcessIdentifier>();
            QuerySet query = GetQuerySet(db);
            query.Count<ServiceProcessIdentifier>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ServiceProcessIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ServiceProcessIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ServiceProcessIdentifierColumns> where, Database database = null)
		{
			ServiceProcessIdentifierColumns c = new ServiceProcessIdentifierColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ServiceProcessIdentifier>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ServiceProcessIdentifier>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ServiceProcessIdentifier>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ServiceProcessIdentifier>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ServiceProcessIdentifier CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ServiceProcessIdentifier>();			
			var dao = new ServiceProcessIdentifier();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ServiceProcessIdentifier OneOrThrow(ServiceProcessIdentifierCollection c)
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
