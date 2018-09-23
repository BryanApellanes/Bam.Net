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
	[Bam.Net.Data.Table("AssemblyDescriptorAssemblyReferenceDescriptor", "AssemblyService")]
	public partial class AssemblyDescriptorAssemblyReferenceDescriptor: Bam.Net.Data.Dao
	{
		public AssemblyDescriptorAssemblyReferenceDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyDescriptorAssemblyReferenceDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyDescriptorAssemblyReferenceDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyDescriptorAssemblyReferenceDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator AssemblyDescriptorAssemblyReferenceDescriptor(DataRow data)
		{
			return new AssemblyDescriptorAssemblyReferenceDescriptor(data);
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
        Table="AssemblyDescriptorAssemblyReferenceDescriptor",
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
	
	// start AssemblyReferenceDescriptorId -> AssemblyReferenceDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="AssemblyDescriptorAssemblyReferenceDescriptor",
		Name="AssemblyReferenceDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="AssemblyReferenceDescriptor",
		Suffix="2")]
	public ulong? AssemblyReferenceDescriptorId
	{
		get
		{
			return GetULongValue("AssemblyReferenceDescriptorId");
		}
		set
		{
			SetValue("AssemblyReferenceDescriptorId", value);
		}
	}

	AssemblyReferenceDescriptor _assemblyReferenceDescriptorOfAssemblyReferenceDescriptorId;
	public AssemblyReferenceDescriptor AssemblyReferenceDescriptorOfAssemblyReferenceDescriptorId
	{
		get
		{
			if(_assemblyReferenceDescriptorOfAssemblyReferenceDescriptorId == null)
			{
				_assemblyReferenceDescriptorOfAssemblyReferenceDescriptorId = Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.OneWhere(c => c.KeyColumn == this.AssemblyReferenceDescriptorId, this.Database);
			}
			return _assemblyReferenceDescriptorOfAssemblyReferenceDescriptorId;
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
				var colFilter = new AssemblyDescriptorAssemblyReferenceDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the AssemblyDescriptorAssemblyReferenceDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<AssemblyDescriptorAssemblyReferenceDescriptor>();
			var results = new AssemblyDescriptorAssemblyReferenceDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<AssemblyDescriptorAssemblyReferenceDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyDescriptorAssemblyReferenceDescriptorColumns columns = new AssemblyDescriptorAssemblyReferenceDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyDescriptorAssemblyReferenceDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<AssemblyDescriptorAssemblyReferenceDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, Action<IEnumerable<AssemblyDescriptorAssemblyReferenceDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyDescriptorAssemblyReferenceDescriptorColumns columns = new AssemblyDescriptorAssemblyReferenceDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyDescriptorAssemblyReferenceDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (AssemblyDescriptorAssemblyReferenceDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<AssemblyDescriptorAssemblyReferenceDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, Action<IEnumerable<AssemblyDescriptorAssemblyReferenceDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyDescriptorAssemblyReferenceDescriptorColumns columns = new AssemblyDescriptorAssemblyReferenceDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (AssemblyDescriptorAssemblyReferenceDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static AssemblyDescriptorAssemblyReferenceDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static AssemblyDescriptorAssemblyReferenceDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static AssemblyDescriptorAssemblyReferenceDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AssemblyDescriptorAssemblyReferenceDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AssemblyDescriptorAssemblyReferenceDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static AssemblyDescriptorAssemblyReferenceDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Where(Func<AssemblyDescriptorAssemblyReferenceDescriptorColumns, QueryFilter<AssemblyDescriptorAssemblyReferenceDescriptorColumns>> where, OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			return new AssemblyDescriptorAssemblyReferenceDescriptorCollection(database.GetQuery<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Where(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			var results = new AssemblyDescriptorAssemblyReferenceDescriptorCollection(database, database.GetQuery<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Where(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			var results = new AssemblyDescriptorAssemblyReferenceDescriptorCollection(database, database.GetQuery<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;AssemblyDescriptorAssemblyReferenceDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new AssemblyDescriptorAssemblyReferenceDescriptorCollection(database, Select<AssemblyDescriptorAssemblyReferenceDescriptorColumns>.From<AssemblyDescriptorAssemblyReferenceDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static AssemblyDescriptorAssemblyReferenceDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> whereDelegate = (c) => where;
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
		public static AssemblyDescriptorAssemblyReferenceDescriptor GetOneWhere(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				AssemblyDescriptorAssemblyReferenceDescriptorColumns c = new AssemblyDescriptorAssemblyReferenceDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AssemblyDescriptorAssemblyReferenceDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptor OneWhere(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyDescriptorAssemblyReferenceDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptor FirstOneWhere(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptor FirstOneWhere(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptor FirstOneWhere(QueryFilter where, OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy, Database database = null)
		{
			AssemblyDescriptorAssemblyReferenceDescriptorColumns c = new AssemblyDescriptorAssemblyReferenceDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<AssemblyDescriptorAssemblyReferenceDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorAssemblyReferenceDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Top(int count, QueryFilter where, OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyDescriptorAssemblyReferenceDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyDescriptorAssemblyReferenceDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorAssemblyReferenceDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyDescriptorAssemblyReferenceDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorAssemblyReferenceDescriptorCollection>(0);
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
		public static AssemblyDescriptorAssemblyReferenceDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyDescriptorAssemblyReferenceDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorAssemblyReferenceDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of AssemblyDescriptorAssemblyReferenceDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<AssemblyDescriptorAssemblyReferenceDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorAssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorAssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<AssemblyDescriptorAssemblyReferenceDescriptorColumns> where, Database database = null)
		{
			AssemblyDescriptorAssemblyReferenceDescriptorColumns c = new AssemblyDescriptorAssemblyReferenceDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyDescriptorAssemblyReferenceDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyDescriptorAssemblyReferenceDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static AssemblyDescriptorAssemblyReferenceDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptorAssemblyReferenceDescriptor>();			
			var dao = new AssemblyDescriptorAssemblyReferenceDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static AssemblyDescriptorAssemblyReferenceDescriptor OneOrThrow(AssemblyDescriptorAssemblyReferenceDescriptorCollection c)
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
