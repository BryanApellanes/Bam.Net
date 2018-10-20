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
	[Bam.Net.Data.Table("HostDomainApplication", "ApplicationRegistration")]
	public partial class HostDomainApplication: Bam.Net.Data.Dao
	{
		public HostDomainApplication():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HostDomainApplication(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HostDomainApplication(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HostDomainApplication(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator HostDomainApplication(DataRow data)
		{
			return new HostDomainApplication(data);
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



	// start HostDomainId -> HostDomainId
	[Bam.Net.Data.ForeignKey(
        Table="HostDomainApplication",
		Name="HostDomainId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="HostDomain",
		Suffix="1")]
	public ulong? HostDomainId
	{
		get
		{
			return GetULongValue("HostDomainId");
		}
		set
		{
			SetValue("HostDomainId", value);
		}
	}

	HostDomain _hostDomainOfHostDomainId;
	public HostDomain HostDomainOfHostDomainId
	{
		get
		{
			if(_hostDomainOfHostDomainId == null)
			{
				_hostDomainOfHostDomainId = Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomain.OneWhere(c => c.KeyColumn == this.HostDomainId, this.Database);
			}
			return _hostDomainOfHostDomainId;
		}
	}
	
	// start ApplicationId -> ApplicationId
	[Bam.Net.Data.ForeignKey(
        Table="HostDomainApplication",
		Name="ApplicationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Application",
		Suffix="2")]
	public ulong? ApplicationId
	{
		get
		{
			return GetULongValue("ApplicationId");
		}
		set
		{
			SetValue("ApplicationId", value);
		}
	}

	Application _applicationOfApplicationId;
	public Application ApplicationOfApplicationId
	{
		get
		{
			if(_applicationOfApplicationId == null)
			{
				_applicationOfApplicationId = Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application.OneWhere(c => c.KeyColumn == this.ApplicationId, this.Database);
			}
			return _applicationOfApplicationId;
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
				var colFilter = new HostDomainApplicationColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the HostDomainApplication table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static HostDomainApplicationCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<HostDomainApplication>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<HostDomainApplication>();
			var results = new HostDomainApplicationCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<HostDomainApplication>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				HostDomainApplicationColumns columns = new HostDomainApplicationColumns();
				var orderBy = Bam.Net.Data.Order.By<HostDomainApplicationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<HostDomainApplication>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<HostDomainApplicationColumns> where, Action<IEnumerable<HostDomainApplication>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				HostDomainApplicationColumns columns = new HostDomainApplicationColumns();
				var orderBy = Bam.Net.Data.Order.By<HostDomainApplicationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (HostDomainApplicationColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<HostDomainApplication>> batchProcessor, Bam.Net.Data.OrderBy<HostDomainApplicationColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<HostDomainApplicationColumns> where, Action<IEnumerable<HostDomainApplication>> batchProcessor, Bam.Net.Data.OrderBy<HostDomainApplicationColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				HostDomainApplicationColumns columns = new HostDomainApplicationColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (HostDomainApplicationColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static HostDomainApplication GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static HostDomainApplication GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static HostDomainApplication GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static HostDomainApplication GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static HostDomainApplication GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static HostDomainApplication GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static HostDomainApplicationCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static HostDomainApplicationCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<HostDomainApplicationColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a HostDomainApplicationColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static HostDomainApplicationCollection Where(Func<HostDomainApplicationColumns, QueryFilter<HostDomainApplicationColumns>> where, OrderBy<HostDomainApplicationColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<HostDomainApplication>();
			return new HostDomainApplicationCollection(database.GetQuery<HostDomainApplicationColumns, HostDomainApplication>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static HostDomainApplicationCollection Where(WhereDelegate<HostDomainApplicationColumns> where, Database database = null)
		{		
			database = database ?? Db.For<HostDomainApplication>();
			var results = new HostDomainApplicationCollection(database, database.GetQuery<HostDomainApplicationColumns, HostDomainApplication>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomainApplicationCollection Where(WhereDelegate<HostDomainApplicationColumns> where, OrderBy<HostDomainApplicationColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<HostDomainApplication>();
			var results = new HostDomainApplicationCollection(database, database.GetQuery<HostDomainApplicationColumns, HostDomainApplication>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;HostDomainApplicationColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HostDomainApplicationCollection Where(QiQuery where, Database database = null)
		{
			var results = new HostDomainApplicationCollection(database, Select<HostDomainApplicationColumns>.From<HostDomainApplication>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static HostDomainApplication GetOneWhere(QueryFilter where, Database database = null)
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
		public static HostDomainApplication OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<HostDomainApplicationColumns> whereDelegate = (c) => where;
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
		public static HostDomainApplication GetOneWhere(WhereDelegate<HostDomainApplicationColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				HostDomainApplicationColumns c = new HostDomainApplicationColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single HostDomainApplication instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomainApplication OneWhere(WhereDelegate<HostDomainApplicationColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<HostDomainApplicationColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HostDomainApplication OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomainApplication FirstOneWhere(WhereDelegate<HostDomainApplicationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HostDomainApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomainApplication FirstOneWhere(WhereDelegate<HostDomainApplicationColumns> where, OrderBy<HostDomainApplicationColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HostDomainApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomainApplication FirstOneWhere(QueryFilter where, OrderBy<HostDomainApplicationColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<HostDomainApplicationColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a HostDomainApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static HostDomainApplicationCollection Top(int count, WhereDelegate<HostDomainApplicationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HostDomainApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static HostDomainApplicationCollection Top(int count, WhereDelegate<HostDomainApplicationColumns> where, OrderBy<HostDomainApplicationColumns> orderBy, Database database = null)
		{
			HostDomainApplicationColumns c = new HostDomainApplicationColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<HostDomainApplication>();
			QuerySet query = GetQuerySet(db); 
			query.Top<HostDomainApplication>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HostDomainApplicationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HostDomainApplicationCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static HostDomainApplicationCollection Top(int count, QueryFilter where, Database database)
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
		public static HostDomainApplicationCollection Top(int count, QueryFilter where, OrderBy<HostDomainApplicationColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<HostDomainApplication>();
			QuerySet query = GetQuerySet(db);
			query.Top<HostDomainApplication>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<HostDomainApplicationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HostDomainApplicationCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static HostDomainApplicationCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<HostDomainApplication>();
			QuerySet query = GetQuerySet(db);
			query.Top<HostDomainApplication>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<HostDomainApplicationCollection>(0);
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
		public static HostDomainApplicationCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<HostDomainApplication>();
			QuerySet query = GetQuerySet(db);
			query.Top<HostDomainApplication>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<HostDomainApplicationCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of HostDomainApplications
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<HostDomainApplication>();
            QuerySet query = GetQuerySet(db);
            query.Count<HostDomainApplication>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HostDomainApplicationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HostDomainApplicationColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<HostDomainApplicationColumns> where, Database database = null)
		{
			HostDomainApplicationColumns c = new HostDomainApplicationColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<HostDomainApplication>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<HostDomainApplication>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<HostDomainApplication>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<HostDomainApplication>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static HostDomainApplication CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<HostDomainApplication>();			
			var dao = new HostDomainApplication();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static HostDomainApplication OneOrThrow(HostDomainApplicationCollection c)
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
