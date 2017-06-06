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

namespace Bam.Net.Services.AssemblyManagement.Data.Dao
{
	// schema = AssemblyManagement
	// connection Name = AssemblyManagement
	[Serializable]
	[Bam.Net.Data.Table("AssemblyReferenceDescriptorAssemblyDescriptor", "AssemblyManagement")]
	public partial class AssemblyReferenceDescriptorAssemblyDescriptor: Bam.Net.Data.Dao
	{
		public AssemblyReferenceDescriptorAssemblyDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyReferenceDescriptorAssemblyDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyReferenceDescriptorAssemblyDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyReferenceDescriptorAssemblyDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator AssemblyReferenceDescriptorAssemblyDescriptor(DataRow data)
		{
			return new AssemblyReferenceDescriptorAssemblyDescriptor(data);
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



	// start AssemblyReferenceDescriptorId -> AssemblyReferenceDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="AssemblyReferenceDescriptorAssemblyDescriptor",
		Name="AssemblyReferenceDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="AssemblyReferenceDescriptor",
		Suffix="1")]
	public long? AssemblyReferenceDescriptorId
	{
		get
		{
			return GetLongValue("AssemblyReferenceDescriptorId");
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
				_assemblyReferenceDescriptorOfAssemblyReferenceDescriptorId = Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.OneWhere(c => c.KeyColumn == this.AssemblyReferenceDescriptorId, this.Database);
			}
			return _assemblyReferenceDescriptorOfAssemblyReferenceDescriptorId;
		}
	}
	
	// start AssemblyDescriptorId -> AssemblyDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="AssemblyReferenceDescriptorAssemblyDescriptor",
		Name="AssemblyDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="AssemblyDescriptor",
		Suffix="2")]
	public long? AssemblyDescriptorId
	{
		get
		{
			return GetLongValue("AssemblyDescriptorId");
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
				_assemblyDescriptorOfAssemblyDescriptorId = Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor.OneWhere(c => c.KeyColumn == this.AssemblyDescriptorId, this.Database);
			}
			return _assemblyDescriptorOfAssemblyDescriptorId;
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
				var colFilter = new AssemblyReferenceDescriptorAssemblyDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the AssemblyReferenceDescriptorAssemblyDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<AssemblyReferenceDescriptorAssemblyDescriptor>();
			Database db = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
			var results = new AssemblyReferenceDescriptorAssemblyDescriptorCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<AssemblyReferenceDescriptorAssemblyDescriptor>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				AssemblyReferenceDescriptorAssemblyDescriptorColumns columns = new AssemblyReferenceDescriptorAssemblyDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyReferenceDescriptorAssemblyDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<AssemblyReferenceDescriptorAssemblyDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Action<IEnumerable<AssemblyReferenceDescriptorAssemblyDescriptor>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				AssemblyReferenceDescriptorAssemblyDescriptorColumns columns = new AssemblyReferenceDescriptorAssemblyDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyReferenceDescriptorAssemblyDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (AssemblyReferenceDescriptorAssemblyDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static AssemblyReferenceDescriptorAssemblyDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static AssemblyReferenceDescriptorAssemblyDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AssemblyReferenceDescriptorAssemblyDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static AssemblyReferenceDescriptorAssemblyDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Where(Func<AssemblyReferenceDescriptorAssemblyDescriptorColumns, QueryFilter<AssemblyReferenceDescriptorAssemblyDescriptorColumns>> where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
			return new AssemblyReferenceDescriptorAssemblyDescriptorCollection(database.GetQuery<AssemblyReferenceDescriptorAssemblyDescriptorColumns, AssemblyReferenceDescriptorAssemblyDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Where(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
			var results = new AssemblyReferenceDescriptorAssemblyDescriptorCollection(database, database.GetQuery<AssemblyReferenceDescriptorAssemblyDescriptorColumns, AssemblyReferenceDescriptorAssemblyDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Where(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
			var results = new AssemblyReferenceDescriptorAssemblyDescriptorCollection(database, database.GetQuery<AssemblyReferenceDescriptorAssemblyDescriptorColumns, AssemblyReferenceDescriptorAssemblyDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;AssemblyReferenceDescriptorAssemblyDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new AssemblyReferenceDescriptorAssemblyDescriptorCollection(database, Select<AssemblyReferenceDescriptorAssemblyDescriptorColumns>.From<AssemblyReferenceDescriptorAssemblyDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static AssemblyReferenceDescriptorAssemblyDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> whereDelegate = (c) => where;
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
		public static AssemblyReferenceDescriptorAssemblyDescriptor GetOneWhere(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				AssemblyReferenceDescriptorAssemblyDescriptorColumns c = new AssemblyReferenceDescriptorAssemblyDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AssemblyReferenceDescriptorAssemblyDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptor OneWhere(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyReferenceDescriptorAssemblyDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptor FirstOneWhere(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptor FirstOneWhere(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptor FirstOneWhere(QueryFilter where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Top(int count, WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Top(int count, WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy, Database database = null)
		{
			AssemblyReferenceDescriptorAssemblyDescriptorColumns c = new AssemblyReferenceDescriptorAssemblyDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<AssemblyReferenceDescriptorAssemblyDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyReferenceDescriptorAssemblyDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Top(int count, QueryFilter where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyReferenceDescriptorAssemblyDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyReferenceDescriptorAssemblyDescriptorCollection>(0);
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
		public static AssemblyReferenceDescriptorAssemblyDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyReferenceDescriptorAssemblyDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<AssemblyReferenceDescriptorAssemblyDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of AssemblyReferenceDescriptorAssemblyDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<AssemblyReferenceDescriptorAssemblyDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorAssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorAssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database database = null)
		{
			AssemblyReferenceDescriptorAssemblyDescriptorColumns c = new AssemblyReferenceDescriptorAssemblyDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyReferenceDescriptorAssemblyDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyReferenceDescriptorAssemblyDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static AssemblyReferenceDescriptorAssemblyDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyReferenceDescriptorAssemblyDescriptor>();			
			var dao = new AssemblyReferenceDescriptorAssemblyDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static AssemblyReferenceDescriptorAssemblyDescriptor OneOrThrow(AssemblyReferenceDescriptorAssemblyDescriptorCollection c)
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
