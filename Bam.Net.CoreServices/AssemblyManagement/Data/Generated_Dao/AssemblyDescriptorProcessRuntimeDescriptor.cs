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

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
	// schema = AssemblyService
	// connection Name = AssemblyService
	[Serializable]
	[Bam.Net.Data.Table("AssemblyDescriptorProcessRuntimeDescriptor", "AssemblyService")]
	public partial class AssemblyDescriptorProcessRuntimeDescriptor: Bam.Net.Data.Dao
	{
		public AssemblyDescriptorProcessRuntimeDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyDescriptorProcessRuntimeDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyDescriptorProcessRuntimeDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyDescriptorProcessRuntimeDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator AssemblyDescriptorProcessRuntimeDescriptor(DataRow data)
		{
			return new AssemblyDescriptorProcessRuntimeDescriptor(data);
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



	// start AssemblyDescriptorId -> AssemblyDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="AssemblyDescriptorProcessRuntimeDescriptor",
		Name="AssemblyDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="AssemblyDescriptor",
		Suffix="1")]
	public ulong? AssemblyDescriptorId
	{
		get
		{
			return GetULongValue("AssemblyDescriptorId");
		}
		set
		{
			SetValue("AssemblyDescriptorId", value);
		}
	}

	AssemblyDescriptor _assemblyDescriptorOfAssemblyDescriptorId;
	public AssemblyDescriptor AssemblyDescriptorOfAssemblyDescriptorId
	{
		get
		{
			if(_assemblyDescriptorOfAssemblyDescriptorId == null)
			{
				_assemblyDescriptorOfAssemblyDescriptorId = Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor.OneWhere(c => c.KeyColumn == this.AssemblyDescriptorId, this.Database);
			}
			return _assemblyDescriptorOfAssemblyDescriptorId;
		}
	}
	
	// start ProcessRuntimeDescriptorId -> ProcessRuntimeDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="AssemblyDescriptorProcessRuntimeDescriptor",
		Name="ProcessRuntimeDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="ProcessRuntimeDescriptor",
		Suffix="2")]
	public ulong? ProcessRuntimeDescriptorId
	{
		get
		{
			return GetULongValue("ProcessRuntimeDescriptorId");
		}
		set
		{
			SetValue("ProcessRuntimeDescriptorId", value);
		}
	}

	ProcessRuntimeDescriptor _processRuntimeDescriptorOfProcessRuntimeDescriptorId;
	public ProcessRuntimeDescriptor ProcessRuntimeDescriptorOfProcessRuntimeDescriptorId
	{
		get
		{
			if(_processRuntimeDescriptorOfProcessRuntimeDescriptorId == null)
			{
				_processRuntimeDescriptorOfProcessRuntimeDescriptorId = Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.OneWhere(c => c.KeyColumn == this.ProcessRuntimeDescriptorId, this.Database);
			}
			return _processRuntimeDescriptorOfProcessRuntimeDescriptorId;
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
				var colFilter = new AssemblyDescriptorProcessRuntimeDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the AssemblyDescriptorProcessRuntimeDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<AssemblyDescriptorProcessRuntimeDescriptor>();
			var results = new AssemblyDescriptorProcessRuntimeDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<AssemblyDescriptorProcessRuntimeDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyDescriptorProcessRuntimeDescriptorColumns columns = new AssemblyDescriptorProcessRuntimeDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyDescriptorProcessRuntimeDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<AssemblyDescriptorProcessRuntimeDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Action<IEnumerable<AssemblyDescriptorProcessRuntimeDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyDescriptorProcessRuntimeDescriptorColumns columns = new AssemblyDescriptorProcessRuntimeDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyDescriptorProcessRuntimeDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (AssemblyDescriptorProcessRuntimeDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<AssemblyDescriptorProcessRuntimeDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Action<IEnumerable<AssemblyDescriptorProcessRuntimeDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyDescriptorProcessRuntimeDescriptorColumns columns = new AssemblyDescriptorProcessRuntimeDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (AssemblyDescriptorProcessRuntimeDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static AssemblyDescriptorProcessRuntimeDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static AssemblyDescriptorProcessRuntimeDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static AssemblyDescriptorProcessRuntimeDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AssemblyDescriptorProcessRuntimeDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AssemblyDescriptorProcessRuntimeDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static AssemblyDescriptorProcessRuntimeDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Where(Func<AssemblyDescriptorProcessRuntimeDescriptorColumns, QueryFilter<AssemblyDescriptorProcessRuntimeDescriptorColumns>> where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			return new AssemblyDescriptorProcessRuntimeDescriptorCollection(database.GetQuery<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Where(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			var results = new AssemblyDescriptorProcessRuntimeDescriptorCollection(database, database.GetQuery<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Where(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			var results = new AssemblyDescriptorProcessRuntimeDescriptorCollection(database, database.GetQuery<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;AssemblyDescriptorProcessRuntimeDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new AssemblyDescriptorProcessRuntimeDescriptorCollection(database, Select<AssemblyDescriptorProcessRuntimeDescriptorColumns>.From<AssemblyDescriptorProcessRuntimeDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static AssemblyDescriptorProcessRuntimeDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> whereDelegate = (c) => where;
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
		public static AssemblyDescriptorProcessRuntimeDescriptor GetOneWhere(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				AssemblyDescriptorProcessRuntimeDescriptorColumns c = new AssemblyDescriptorProcessRuntimeDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AssemblyDescriptorProcessRuntimeDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptor OneWhere(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyDescriptorProcessRuntimeDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptor FirstOneWhere(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptor FirstOneWhere(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptor FirstOneWhere(QueryFilter where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy, Database database = null)
		{
			AssemblyDescriptorProcessRuntimeDescriptorColumns c = new AssemblyDescriptorProcessRuntimeDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<AssemblyDescriptorProcessRuntimeDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorProcessRuntimeDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Top(int count, QueryFilter where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyDescriptorProcessRuntimeDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorProcessRuntimeDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyDescriptorProcessRuntimeDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorProcessRuntimeDescriptorCollection>(0);
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
		public static AssemblyDescriptorProcessRuntimeDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyDescriptorProcessRuntimeDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorProcessRuntimeDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of AssemblyDescriptorProcessRuntimeDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<AssemblyDescriptorProcessRuntimeDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorProcessRuntimeDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorProcessRuntimeDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database database = null)
		{
			AssemblyDescriptorProcessRuntimeDescriptorColumns c = new AssemblyDescriptorProcessRuntimeDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyDescriptorProcessRuntimeDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyDescriptorProcessRuntimeDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static AssemblyDescriptorProcessRuntimeDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorProcessRuntimeDescriptor>();			
			var dao = new AssemblyDescriptorProcessRuntimeDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static AssemblyDescriptorProcessRuntimeDescriptor OneOrThrow(AssemblyDescriptorProcessRuntimeDescriptorCollection c)
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
