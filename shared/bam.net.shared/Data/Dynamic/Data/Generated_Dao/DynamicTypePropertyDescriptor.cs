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

namespace Bam.Net.Data.Dynamic.Data.Dao
{
	// schema = DynamicTypeData
	// connection Name = DynamicTypeData
	[Serializable]
	[Bam.Net.Data.Table("DynamicTypePropertyDescriptor", "DynamicTypeData")]
	public partial class DynamicTypePropertyDescriptor: Bam.Net.Data.Dao
	{
		public DynamicTypePropertyDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DynamicTypePropertyDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DynamicTypePropertyDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DynamicTypePropertyDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator DynamicTypePropertyDescriptor(DataRow data)
		{
			return new DynamicTypePropertyDescriptor(data);
		}

		private void SetChildren()
		{
						
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

	// property:ParentTypeName, columnName:ParentTypeName	
	[Bam.Net.Data.Column(Name="ParentTypeName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ParentTypeName
	{
		get
		{
			return GetStringValue("ParentTypeName");
		}
		set
		{
			SetValue("ParentTypeName", value);
		}
	}

	// property:PropertyType, columnName:PropertyType	
	[Bam.Net.Data.Column(Name="PropertyType", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string PropertyType
	{
		get
		{
			return GetStringValue("PropertyType");
		}
		set
		{
			SetValue("PropertyType", value);
		}
	}

	// property:PropertyName, columnName:PropertyName	
	[Bam.Net.Data.Column(Name="PropertyName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string PropertyName
	{
		get
		{
			return GetStringValue("PropertyName");
		}
		set
		{
			SetValue("PropertyName", value);
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



	// start DynamicTypeDescriptorId -> DynamicTypeDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="DynamicTypePropertyDescriptor",
		Name="DynamicTypeDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="DynamicTypeDescriptor",
		Suffix="1")]
	public ulong? DynamicTypeDescriptorId
	{
		get
		{
			return GetULongValue("DynamicTypeDescriptorId");
		}
		set
		{
			SetValue("DynamicTypeDescriptorId", value);
		}
	}

	DynamicTypeDescriptor _dynamicTypeDescriptorOfDynamicTypeDescriptorId;
	public DynamicTypeDescriptor DynamicTypeDescriptorOfDynamicTypeDescriptorId
	{
		get
		{
			if(_dynamicTypeDescriptorOfDynamicTypeDescriptorId == null)
			{
				_dynamicTypeDescriptorOfDynamicTypeDescriptorId = Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.OneWhere(c => c.KeyColumn == this.DynamicTypeDescriptorId, this.Database);
			}
			return _dynamicTypeDescriptorOfDynamicTypeDescriptorId;
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
				var colFilter = new DynamicTypePropertyDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the DynamicTypePropertyDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DynamicTypePropertyDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<DynamicTypePropertyDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<DynamicTypePropertyDescriptor>();
			var results = new DynamicTypePropertyDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<DynamicTypePropertyDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DynamicTypePropertyDescriptorColumns columns = new DynamicTypePropertyDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<DynamicTypePropertyDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<DynamicTypePropertyDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Action<IEnumerable<DynamicTypePropertyDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DynamicTypePropertyDescriptorColumns columns = new DynamicTypePropertyDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<DynamicTypePropertyDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DynamicTypePropertyDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<DynamicTypePropertyDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<DynamicTypePropertyDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Action<IEnumerable<DynamicTypePropertyDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<DynamicTypePropertyDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DynamicTypePropertyDescriptorColumns columns = new DynamicTypePropertyDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (DynamicTypePropertyDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static DynamicTypePropertyDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static DynamicTypePropertyDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DynamicTypePropertyDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DynamicTypePropertyDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DynamicTypePropertyDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static DynamicTypePropertyDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static DynamicTypePropertyDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DynamicTypePropertyDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptorCollection Where(Func<DynamicTypePropertyDescriptorColumns, QueryFilter<DynamicTypePropertyDescriptorColumns>> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DynamicTypePropertyDescriptor>();
			return new DynamicTypePropertyDescriptorCollection(database.GetQuery<DynamicTypePropertyDescriptorColumns, DynamicTypePropertyDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptorCollection Where(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<DynamicTypePropertyDescriptor>();
			var results = new DynamicTypePropertyDescriptorCollection(database, database.GetQuery<DynamicTypePropertyDescriptorColumns, DynamicTypePropertyDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptorCollection Where(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<DynamicTypePropertyDescriptor>();
			var results = new DynamicTypePropertyDescriptorCollection(database, database.GetQuery<DynamicTypePropertyDescriptorColumns, DynamicTypePropertyDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;DynamicTypePropertyDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DynamicTypePropertyDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new DynamicTypePropertyDescriptorCollection(database, Select<DynamicTypePropertyDescriptorColumns>.From<DynamicTypePropertyDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static DynamicTypePropertyDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DynamicTypePropertyDescriptorColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database database = null)
		{
			SetOneWhere(where, out DynamicTypePropertyDescriptor ignore, database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, out DynamicTypePropertyDescriptor result, Database database = null)
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
		public static DynamicTypePropertyDescriptor GetOneWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DynamicTypePropertyDescriptorColumns c = new DynamicTypePropertyDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DynamicTypePropertyDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptor OneWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DynamicTypePropertyDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DynamicTypePropertyDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptor FirstOneWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptor FirstOneWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptor FirstOneWhere(QueryFilter where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DynamicTypePropertyDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptorCollection Top(int count, WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptorCollection Top(int count, WhereDelegate<DynamicTypePropertyDescriptorColumns> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy, Database database = null)
		{
			DynamicTypePropertyDescriptorColumns c = new DynamicTypePropertyDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<DynamicTypePropertyDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<DynamicTypePropertyDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DynamicTypePropertyDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DynamicTypePropertyDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static DynamicTypePropertyDescriptorCollection Top(int count, QueryFilter where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DynamicTypePropertyDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<DynamicTypePropertyDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DynamicTypePropertyDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DynamicTypePropertyDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DynamicTypePropertyDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<DynamicTypePropertyDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<DynamicTypePropertyDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<DynamicTypePropertyDescriptorCollection>(0);
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
		public static DynamicTypePropertyDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DynamicTypePropertyDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<DynamicTypePropertyDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DynamicTypePropertyDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of DynamicTypePropertyDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<DynamicTypePropertyDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<DynamicTypePropertyDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DynamicTypePropertyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DynamicTypePropertyDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database database = null)
		{
			DynamicTypePropertyDescriptorColumns c = new DynamicTypePropertyDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DynamicTypePropertyDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DynamicTypePropertyDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<DynamicTypePropertyDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DynamicTypePropertyDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static DynamicTypePropertyDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DynamicTypePropertyDescriptor>();			
			var dao = new DynamicTypePropertyDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static DynamicTypePropertyDescriptor OneOrThrow(DynamicTypePropertyDescriptorCollection c)
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
