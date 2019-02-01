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
	[Bam.Net.Data.Table("DataInstance", "DynamicTypeData")]
	public partial class DataInstance: Bam.Net.Data.Dao
	{
		public DataInstance():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DataInstance(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DataInstance(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DataInstance(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator DataInstance(DataRow data)
		{
			return new DataInstance(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("DataInstancePropertyValue_DataInstanceId", new DataInstancePropertyValueCollection(Database.GetQuery<DataInstancePropertyValueColumns, DataInstancePropertyValue>((c) => c.DataInstanceId == GetULongValue("Id")), this, "DataInstanceId"));				
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

	// property:TypeName, columnName:TypeName	
	[Bam.Net.Data.Column(Name="TypeName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string TypeName
	{
		get
		{
			return GetStringValue("TypeName");
		}
		set
		{
			SetValue("TypeName", value);
		}
	}

	// property:RootHash, columnName:RootHash	
	[Bam.Net.Data.Column(Name="RootHash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string RootHash
	{
		get
		{
			return GetStringValue("RootHash");
		}
		set
		{
			SetValue("RootHash", value);
		}
	}

	// property:ParentHash, columnName:ParentHash	
	[Bam.Net.Data.Column(Name="ParentHash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ParentHash
	{
		get
		{
			return GetStringValue("ParentHash");
		}
		set
		{
			SetValue("ParentHash", value);
		}
	}

	// property:Instancehash, columnName:Instancehash	
	[Bam.Net.Data.Column(Name="Instancehash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Instancehash
	{
		get
		{
			return GetStringValue("Instancehash");
		}
		set
		{
			SetValue("Instancehash", value);
		}
	}

	// property:Key, columnName:Key	
	[Bam.Net.Data.Column(Name="Key", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public ulong? Key
	{
		get
		{
			return GetULongValue("Key");
		}
		set
		{
			SetValue("Key", value);
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
	public DataInstancePropertyValueCollection DataInstancePropertyValuesByDataInstanceId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("DataInstancePropertyValue_DataInstanceId"))
			{
				SetChildren();
			}

			var c = (DataInstancePropertyValueCollection)this.ChildCollections["DataInstancePropertyValue_DataInstanceId"];
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
				var colFilter = new DataInstanceColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the DataInstance table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DataInstanceCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<DataInstance>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<DataInstance>();
			var results = new DataInstanceCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<DataInstance>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DataInstanceColumns columns = new DataInstanceColumns();
				var orderBy = Bam.Net.Data.Order.By<DataInstanceColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<DataInstance>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<DataInstanceColumns> where, Action<IEnumerable<DataInstance>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DataInstanceColumns columns = new DataInstanceColumns();
				var orderBy = Bam.Net.Data.Order.By<DataInstanceColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DataInstanceColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<DataInstance>> batchProcessor, Bam.Net.Data.OrderBy<DataInstanceColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<DataInstanceColumns> where, Action<IEnumerable<DataInstance>> batchProcessor, Bam.Net.Data.OrderBy<DataInstanceColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DataInstanceColumns columns = new DataInstanceColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (DataInstanceColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static DataInstance GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static DataInstance GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DataInstance GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DataInstance GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DataInstance GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static DataInstance GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static DataInstanceCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static DataInstanceCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DataInstanceColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DataInstanceColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DataInstanceCollection Where(Func<DataInstanceColumns, QueryFilter<DataInstanceColumns>> where, OrderBy<DataInstanceColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DataInstance>();
			return new DataInstanceCollection(database.GetQuery<DataInstanceColumns, DataInstance>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DataInstanceCollection Where(WhereDelegate<DataInstanceColumns> where, Database database = null)
		{		
			database = database ?? Db.For<DataInstance>();
			var results = new DataInstanceCollection(database, database.GetQuery<DataInstanceColumns, DataInstance>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstanceCollection Where(WhereDelegate<DataInstanceColumns> where, OrderBy<DataInstanceColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<DataInstance>();
			var results = new DataInstanceCollection(database, database.GetQuery<DataInstanceColumns, DataInstance>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;DataInstanceColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DataInstanceCollection Where(QiQuery where, Database database = null)
		{
			var results = new DataInstanceCollection(database, Select<DataInstanceColumns>.From<DataInstance>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static DataInstance GetOneWhere(QueryFilter where, Database database = null)
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
		public static DataInstance OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DataInstanceColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<DataInstanceColumns> where, Database database = null)
		{
			SetOneWhere(where, out DataInstance ignore, database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<DataInstanceColumns> where, out DataInstance result, Database database = null)
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
		public static DataInstance GetOneWhere(WhereDelegate<DataInstanceColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DataInstanceColumns c = new DataInstanceColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DataInstance instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstance OneWhere(WhereDelegate<DataInstanceColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DataInstanceColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DataInstance OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstance FirstOneWhere(WhereDelegate<DataInstanceColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstance FirstOneWhere(WhereDelegate<DataInstanceColumns> where, OrderBy<DataInstanceColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstance FirstOneWhere(QueryFilter where, OrderBy<DataInstanceColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DataInstanceColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstanceCollection Top(int count, WhereDelegate<DataInstanceColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static DataInstanceCollection Top(int count, WhereDelegate<DataInstanceColumns> where, OrderBy<DataInstanceColumns> orderBy, Database database = null)
		{
			DataInstanceColumns c = new DataInstanceColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<DataInstance>();
			QuerySet query = GetQuerySet(db); 
			query.Top<DataInstance>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DataInstanceColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DataInstanceCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DataInstanceCollection Top(int count, QueryFilter where, Database database)
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
		public static DataInstanceCollection Top(int count, QueryFilter where, OrderBy<DataInstanceColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DataInstance>();
			QuerySet query = GetQuerySet(db);
			query.Top<DataInstance>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DataInstanceColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DataInstanceCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DataInstanceCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<DataInstance>();
			QuerySet query = GetQuerySet(db);
			query.Top<DataInstance>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<DataInstanceCollection>(0);
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
		public static DataInstanceCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DataInstance>();
			QuerySet query = GetQuerySet(db);
			query.Top<DataInstance>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DataInstanceCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of DataInstances
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<DataInstance>();
            QuerySet query = GetQuerySet(db);
            query.Count<DataInstance>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstanceColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstanceColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<DataInstanceColumns> where, Database database = null)
		{
			DataInstanceColumns c = new DataInstanceColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DataInstance>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DataInstance>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<DataInstance>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DataInstance>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static DataInstance CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DataInstance>();			
			var dao = new DataInstance();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static DataInstance OneOrThrow(DataInstanceCollection c)
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
