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
	[Bam.Net.Data.Table("DataInstancePropertyValue", "DynamicTypeData")]
	public partial class DataInstancePropertyValue: Bam.Net.Data.Dao
	{
		public DataInstancePropertyValue():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DataInstancePropertyValue(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DataInstancePropertyValue(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DataInstancePropertyValue(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator DataInstancePropertyValue(DataRow data)
		{
			return new DataInstancePropertyValue(data);
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

	// property:InstanceHash, columnName:InstanceHash	
	[Bam.Net.Data.Column(Name="InstanceHash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string InstanceHash
	{
		get
		{
			return GetStringValue("InstanceHash");
		}
		set
		{
			SetValue("InstanceHash", value);
		}
	}

	// property:ParentTypeNamespace, columnName:ParentTypeNamespace	
	[Bam.Net.Data.Column(Name="ParentTypeNamespace", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ParentTypeNamespace
	{
		get
		{
			return GetStringValue("ParentTypeNamespace");
		}
		set
		{
			SetValue("ParentTypeNamespace", value);
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

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
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



	// start DataInstanceId -> DataInstanceId
	[Bam.Net.Data.ForeignKey(
        Table="DataInstancePropertyValue",
		Name="DataInstanceId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="DataInstance",
		Suffix="1")]
	public ulong? DataInstanceId
	{
		get
		{
			return GetULongValue("DataInstanceId");
		}
		set
		{
			SetValue("DataInstanceId", value);
		}
	}

	DataInstance _dataInstanceOfDataInstanceId;
	public DataInstance DataInstanceOfDataInstanceId
	{
		get
		{
			if(_dataInstanceOfDataInstanceId == null)
			{
				_dataInstanceOfDataInstanceId = Bam.Net.Data.Dynamic.Data.Dao.DataInstance.OneWhere(c => c.KeyColumn == this.DataInstanceId, this.Database);
			}
			return _dataInstanceOfDataInstanceId;
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
				var colFilter = new DataInstancePropertyValueColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the DataInstancePropertyValue table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DataInstancePropertyValueCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<DataInstancePropertyValue>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<DataInstancePropertyValue>();
			var results = new DataInstancePropertyValueCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<DataInstancePropertyValue>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DataInstancePropertyValueColumns columns = new DataInstancePropertyValueColumns();
				var orderBy = Bam.Net.Data.Order.By<DataInstancePropertyValueColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<DataInstancePropertyValue>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<DataInstancePropertyValueColumns> where, Action<IEnumerable<DataInstancePropertyValue>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DataInstancePropertyValueColumns columns = new DataInstancePropertyValueColumns();
				var orderBy = Bam.Net.Data.Order.By<DataInstancePropertyValueColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DataInstancePropertyValueColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<DataInstancePropertyValue>> batchProcessor, Bam.Net.Data.OrderBy<DataInstancePropertyValueColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<DataInstancePropertyValueColumns> where, Action<IEnumerable<DataInstancePropertyValue>> batchProcessor, Bam.Net.Data.OrderBy<DataInstancePropertyValueColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DataInstancePropertyValueColumns columns = new DataInstancePropertyValueColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (DataInstancePropertyValueColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static DataInstancePropertyValue GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static DataInstancePropertyValue GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DataInstancePropertyValue GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DataInstancePropertyValue GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DataInstancePropertyValue GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static DataInstancePropertyValue GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static DataInstancePropertyValueCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static DataInstancePropertyValueCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DataInstancePropertyValueColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValueCollection Where(Func<DataInstancePropertyValueColumns, QueryFilter<DataInstancePropertyValueColumns>> where, OrderBy<DataInstancePropertyValueColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DataInstancePropertyValue>();
			return new DataInstancePropertyValueCollection(database.GetQuery<DataInstancePropertyValueColumns, DataInstancePropertyValue>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValueCollection Where(WhereDelegate<DataInstancePropertyValueColumns> where, Database database = null)
		{		
			database = database ?? Db.For<DataInstancePropertyValue>();
			var results = new DataInstancePropertyValueCollection(database, database.GetQuery<DataInstancePropertyValueColumns, DataInstancePropertyValue>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValueCollection Where(WhereDelegate<DataInstancePropertyValueColumns> where, OrderBy<DataInstancePropertyValueColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<DataInstancePropertyValue>();
			var results = new DataInstancePropertyValueCollection(database, database.GetQuery<DataInstancePropertyValueColumns, DataInstancePropertyValue>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;DataInstancePropertyValueColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DataInstancePropertyValueCollection Where(QiQuery where, Database database = null)
		{
			var results = new DataInstancePropertyValueCollection(database, Select<DataInstancePropertyValueColumns>.From<DataInstancePropertyValue>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValue GetOneWhere(QueryFilter where, Database database = null)
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
		public static DataInstancePropertyValue OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DataInstancePropertyValueColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<DataInstancePropertyValueColumns> where, Database database = null)
		{
			SetOneWhere(where, out DataInstancePropertyValue ignore, database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<DataInstancePropertyValueColumns> where, out DataInstancePropertyValue result, Database database = null)
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
		public static DataInstancePropertyValue GetOneWhere(WhereDelegate<DataInstancePropertyValueColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DataInstancePropertyValueColumns c = new DataInstancePropertyValueColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DataInstancePropertyValue instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValue OneWhere(WhereDelegate<DataInstancePropertyValueColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DataInstancePropertyValueColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DataInstancePropertyValue OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValue FirstOneWhere(WhereDelegate<DataInstancePropertyValueColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValue FirstOneWhere(WhereDelegate<DataInstancePropertyValueColumns> where, OrderBy<DataInstancePropertyValueColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValue FirstOneWhere(QueryFilter where, OrderBy<DataInstancePropertyValueColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DataInstancePropertyValueColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValueCollection Top(int count, WhereDelegate<DataInstancePropertyValueColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static DataInstancePropertyValueCollection Top(int count, WhereDelegate<DataInstancePropertyValueColumns> where, OrderBy<DataInstancePropertyValueColumns> orderBy, Database database = null)
		{
			DataInstancePropertyValueColumns c = new DataInstancePropertyValueColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<DataInstancePropertyValue>();
			QuerySet query = GetQuerySet(db); 
			query.Top<DataInstancePropertyValue>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DataInstancePropertyValueColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DataInstancePropertyValueCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DataInstancePropertyValueCollection Top(int count, QueryFilter where, Database database)
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
		public static DataInstancePropertyValueCollection Top(int count, QueryFilter where, OrderBy<DataInstancePropertyValueColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DataInstancePropertyValue>();
			QuerySet query = GetQuerySet(db);
			query.Top<DataInstancePropertyValue>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DataInstancePropertyValueColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DataInstancePropertyValueCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DataInstancePropertyValueCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<DataInstancePropertyValue>();
			QuerySet query = GetQuerySet(db);
			query.Top<DataInstancePropertyValue>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<DataInstancePropertyValueCollection>(0);
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
		public static DataInstancePropertyValueCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DataInstancePropertyValue>();
			QuerySet query = GetQuerySet(db);
			query.Top<DataInstancePropertyValue>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DataInstancePropertyValueCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of DataInstancePropertyValues
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<DataInstancePropertyValue>();
            QuerySet query = GetQuerySet(db);
            query.Count<DataInstancePropertyValue>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataInstancePropertyValueColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataInstancePropertyValueColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<DataInstancePropertyValueColumns> where, Database database = null)
		{
			DataInstancePropertyValueColumns c = new DataInstancePropertyValueColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DataInstancePropertyValue>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DataInstancePropertyValue>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<DataInstancePropertyValue>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DataInstancePropertyValue>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static DataInstancePropertyValue CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DataInstancePropertyValue>();			
			var dao = new DataInstancePropertyValue();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static DataInstancePropertyValue OneOrThrow(DataInstancePropertyValueCollection c)
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
