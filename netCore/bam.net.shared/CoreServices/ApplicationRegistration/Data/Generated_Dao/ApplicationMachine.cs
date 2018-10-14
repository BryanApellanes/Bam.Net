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
	[Bam.Net.Data.Table("ApplicationMachine", "ApplicationRegistration")]
	public partial class ApplicationMachine: Bam.Net.Data.Dao
	{
		public ApplicationMachine():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ApplicationMachine(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ApplicationMachine(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ApplicationMachine(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ApplicationMachine(DataRow data)
		{
			return new ApplicationMachine(data);
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



	// start ApplicationId -> ApplicationId
	[Bam.Net.Data.ForeignKey(
        Table="ApplicationMachine",
		Name="ApplicationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Application",
		Suffix="1")]
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
	
	// start MachineId -> MachineId
	[Bam.Net.Data.ForeignKey(
        Table="ApplicationMachine",
		Name="MachineId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Machine",
		Suffix="2")]
	public ulong? MachineId
	{
		get
		{
			return GetULongValue("MachineId");
		}
		set
		{
			SetValue("MachineId", value);
		}
	}

	Machine _machineOfMachineId;
	public Machine MachineOfMachineId
	{
		get
		{
			if(_machineOfMachineId == null)
			{
				_machineOfMachineId = Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Machine.OneWhere(c => c.KeyColumn == this.MachineId, this.Database);
			}
			return _machineOfMachineId;
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
				var colFilter = new ApplicationMachineColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ApplicationMachine table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ApplicationMachineCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<ApplicationMachine>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<ApplicationMachine>();
			var results = new ApplicationMachineCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ApplicationMachine>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ApplicationMachineColumns columns = new ApplicationMachineColumns();
				var orderBy = Bam.Net.Data.Order.By<ApplicationMachineColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ApplicationMachine>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ApplicationMachineColumns> where, Action<IEnumerable<ApplicationMachine>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ApplicationMachineColumns columns = new ApplicationMachineColumns();
				var orderBy = Bam.Net.Data.Order.By<ApplicationMachineColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ApplicationMachineColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<ApplicationMachine>> batchProcessor, Bam.Net.Data.OrderBy<ApplicationMachineColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ApplicationMachineColumns> where, Action<IEnumerable<ApplicationMachine>> batchProcessor, Bam.Net.Data.OrderBy<ApplicationMachineColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ApplicationMachineColumns columns = new ApplicationMachineColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ApplicationMachineColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static ApplicationMachine GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static ApplicationMachine GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ApplicationMachine GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ApplicationMachine GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ApplicationMachine GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ApplicationMachine GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ApplicationMachineCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ApplicationMachineCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ApplicationMachineColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ApplicationMachineColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ApplicationMachineCollection Where(Func<ApplicationMachineColumns, QueryFilter<ApplicationMachineColumns>> where, OrderBy<ApplicationMachineColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ApplicationMachine>();
			return new ApplicationMachineCollection(database.GetQuery<ApplicationMachineColumns, ApplicationMachine>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApplicationMachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ApplicationMachineCollection Where(WhereDelegate<ApplicationMachineColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ApplicationMachine>();
			var results = new ApplicationMachineCollection(database, database.GetQuery<ApplicationMachineColumns, ApplicationMachine>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApplicationMachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ApplicationMachineCollection Where(WhereDelegate<ApplicationMachineColumns> where, OrderBy<ApplicationMachineColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ApplicationMachine>();
			var results = new ApplicationMachineCollection(database, database.GetQuery<ApplicationMachineColumns, ApplicationMachine>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ApplicationMachineColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ApplicationMachineCollection Where(QiQuery where, Database database = null)
		{
			var results = new ApplicationMachineCollection(database, Select<ApplicationMachineColumns>.From<ApplicationMachine>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ApplicationMachine GetOneWhere(QueryFilter where, Database database = null)
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
		public static ApplicationMachine OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ApplicationMachineColumns> whereDelegate = (c) => where;
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
		public static ApplicationMachine GetOneWhere(WhereDelegate<ApplicationMachineColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ApplicationMachineColumns c = new ApplicationMachineColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ApplicationMachine instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApplicationMachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ApplicationMachine OneWhere(WhereDelegate<ApplicationMachineColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ApplicationMachineColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ApplicationMachine OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApplicationMachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ApplicationMachine FirstOneWhere(WhereDelegate<ApplicationMachineColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ApplicationMachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ApplicationMachine FirstOneWhere(WhereDelegate<ApplicationMachineColumns> where, OrderBy<ApplicationMachineColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ApplicationMachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ApplicationMachine FirstOneWhere(QueryFilter where, OrderBy<ApplicationMachineColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ApplicationMachineColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ApplicationMachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ApplicationMachineCollection Top(int count, WhereDelegate<ApplicationMachineColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ApplicationMachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ApplicationMachineCollection Top(int count, WhereDelegate<ApplicationMachineColumns> where, OrderBy<ApplicationMachineColumns> orderBy, Database database = null)
		{
			ApplicationMachineColumns c = new ApplicationMachineColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ApplicationMachine>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ApplicationMachine>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ApplicationMachineColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ApplicationMachineCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ApplicationMachineCollection Top(int count, QueryFilter where, Database database)
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
		public static ApplicationMachineCollection Top(int count, QueryFilter where, OrderBy<ApplicationMachineColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ApplicationMachine>();
			QuerySet query = GetQuerySet(db);
			query.Top<ApplicationMachine>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ApplicationMachineColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ApplicationMachineCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ApplicationMachineCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<ApplicationMachine>();
			QuerySet query = GetQuerySet(db);
			query.Top<ApplicationMachine>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ApplicationMachineCollection>(0);
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
		public static ApplicationMachineCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ApplicationMachine>();
			QuerySet query = GetQuerySet(db);
			query.Top<ApplicationMachine>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ApplicationMachineCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ApplicationMachines
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ApplicationMachine>();
            QuerySet query = GetQuerySet(db);
            query.Count<ApplicationMachine>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApplicationMachineColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApplicationMachineColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ApplicationMachineColumns> where, Database database = null)
		{
			ApplicationMachineColumns c = new ApplicationMachineColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ApplicationMachine>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ApplicationMachine>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ApplicationMachine>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ApplicationMachine>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ApplicationMachine CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ApplicationMachine>();			
			var dao = new ApplicationMachine();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ApplicationMachine OneOrThrow(ApplicationMachineCollection c)
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
