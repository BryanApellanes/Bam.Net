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

namespace Bam.Net.Services.DataReplication.Data.Dao
{
	// schema = DataReplication
	// connection Name = DataReplication
	[Serializable]
	[Bam.Net.Data.Table("DataProperty", "DataReplication")]
	public partial class DataProperty: Bam.Net.Data.Dao
	{
		public DataProperty():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DataProperty(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DataProperty(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DataProperty(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator DataProperty(DataRow data)
		{
			return new DataProperty(data);
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

	// property:InstanceCuid, columnName:InstanceCuid	
	[Bam.Net.Data.Column(Name="InstanceCuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string InstanceCuid
	{
		get
		{
			return GetStringValue("InstanceCuid");
		}
		set
		{
			SetValue("InstanceCuid", value);
		}
	}

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
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



	// start CreateOperationId -> CreateOperationId
	[Bam.Net.Data.ForeignKey(
        Table="DataProperty",
		Name="CreateOperationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="CreateOperation",
		Suffix="1")]
	public long? CreateOperationId
	{
		get
		{
			return GetLongValue("CreateOperationId");
		}
		set
		{
			SetValue("CreateOperationId", value);
		}
	}

	CreateOperation _createOperationOfCreateOperationId;
	public CreateOperation CreateOperationOfCreateOperationId
	{
		get
		{
			if(_createOperationOfCreateOperationId == null)
			{
				_createOperationOfCreateOperationId = Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.OneWhere(c => c.KeyColumn == this.CreateOperationId, this.Database);
			}
			return _createOperationOfCreateOperationId;
		}
	}
	
	// start DeleteEventId -> DeleteEventId
	[Bam.Net.Data.ForeignKey(
        Table="DataProperty",
		Name="DeleteEventId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="DeleteEvent",
		Suffix="2")]
	public long? DeleteEventId
	{
		get
		{
			return GetLongValue("DeleteEventId");
		}
		set
		{
			SetValue("DeleteEventId", value);
		}
	}

	DeleteEvent _deleteEventOfDeleteEventId;
	public DeleteEvent DeleteEventOfDeleteEventId
	{
		get
		{
			if(_deleteEventOfDeleteEventId == null)
			{
				_deleteEventOfDeleteEventId = Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.OneWhere(c => c.KeyColumn == this.DeleteEventId, this.Database);
			}
			return _deleteEventOfDeleteEventId;
		}
	}
	
	// start DeleteOperationId -> DeleteOperationId
	[Bam.Net.Data.ForeignKey(
        Table="DataProperty",
		Name="DeleteOperationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="DeleteOperation",
		Suffix="3")]
	public long? DeleteOperationId
	{
		get
		{
			return GetLongValue("DeleteOperationId");
		}
		set
		{
			SetValue("DeleteOperationId", value);
		}
	}

	DeleteOperation _deleteOperationOfDeleteOperationId;
	public DeleteOperation DeleteOperationOfDeleteOperationId
	{
		get
		{
			if(_deleteOperationOfDeleteOperationId == null)
			{
				_deleteOperationOfDeleteOperationId = Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.OneWhere(c => c.KeyColumn == this.DeleteOperationId, this.Database);
			}
			return _deleteOperationOfDeleteOperationId;
		}
	}
	
	// start QueryOperationId -> QueryOperationId
	[Bam.Net.Data.ForeignKey(
        Table="DataProperty",
		Name="QueryOperationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="QueryOperation",
		Suffix="4")]
	public long? QueryOperationId
	{
		get
		{
			return GetLongValue("QueryOperationId");
		}
		set
		{
			SetValue("QueryOperationId", value);
		}
	}

	QueryOperation _queryOperationOfQueryOperationId;
	public QueryOperation QueryOperationOfQueryOperationId
	{
		get
		{
			if(_queryOperationOfQueryOperationId == null)
			{
				_queryOperationOfQueryOperationId = Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.OneWhere(c => c.KeyColumn == this.QueryOperationId, this.Database);
			}
			return _queryOperationOfQueryOperationId;
		}
	}
	
	// start SaveOperationId -> SaveOperationId
	[Bam.Net.Data.ForeignKey(
        Table="DataProperty",
		Name="SaveOperationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="SaveOperation",
		Suffix="5")]
	public long? SaveOperationId
	{
		get
		{
			return GetLongValue("SaveOperationId");
		}
		set
		{
			SetValue("SaveOperationId", value);
		}
	}

	SaveOperation _saveOperationOfSaveOperationId;
	public SaveOperation SaveOperationOfSaveOperationId
	{
		get
		{
			if(_saveOperationOfSaveOperationId == null)
			{
				_saveOperationOfSaveOperationId = Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.OneWhere(c => c.KeyColumn == this.SaveOperationId, this.Database);
			}
			return _saveOperationOfSaveOperationId;
		}
	}
	
	// start UpdateOperationId -> UpdateOperationId
	[Bam.Net.Data.ForeignKey(
        Table="DataProperty",
		Name="UpdateOperationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="UpdateOperation",
		Suffix="6")]
	public long? UpdateOperationId
	{
		get
		{
			return GetLongValue("UpdateOperationId");
		}
		set
		{
			SetValue("UpdateOperationId", value);
		}
	}

	UpdateOperation _updateOperationOfUpdateOperationId;
	public UpdateOperation UpdateOperationOfUpdateOperationId
	{
		get
		{
			if(_updateOperationOfUpdateOperationId == null)
			{
				_updateOperationOfUpdateOperationId = Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.OneWhere(c => c.KeyColumn == this.UpdateOperationId, this.Database);
			}
			return _updateOperationOfUpdateOperationId;
		}
	}
	
	// start WriteEventId -> WriteEventId
	[Bam.Net.Data.ForeignKey(
        Table="DataProperty",
		Name="WriteEventId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="WriteEvent",
		Suffix="7")]
	public long? WriteEventId
	{
		get
		{
			return GetLongValue("WriteEventId");
		}
		set
		{
			SetValue("WriteEventId", value);
		}
	}

	WriteEvent _writeEventOfWriteEventId;
	public WriteEvent WriteEventOfWriteEventId
	{
		get
		{
			if(_writeEventOfWriteEventId == null)
			{
				_writeEventOfWriteEventId = Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.OneWhere(c => c.KeyColumn == this.WriteEventId, this.Database);
			}
			return _writeEventOfWriteEventId;
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
				var colFilter = new DataPropertyColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the DataProperty table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DataPropertyCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<DataProperty>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<DataProperty>();
			var results = new DataPropertyCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<DataProperty>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DataPropertyColumns columns = new DataPropertyColumns();
				var orderBy = Bam.Net.Data.Order.By<DataPropertyColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<DataProperty>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<DataPropertyColumns> where, Action<IEnumerable<DataProperty>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DataPropertyColumns columns = new DataPropertyColumns();
				var orderBy = Bam.Net.Data.Order.By<DataPropertyColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DataPropertyColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<DataProperty>> batchProcessor, Bam.Net.Data.OrderBy<DataPropertyColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<DataPropertyColumns> where, Action<IEnumerable<DataProperty>> batchProcessor, Bam.Net.Data.OrderBy<DataPropertyColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DataPropertyColumns columns = new DataPropertyColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (DataPropertyColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static DataProperty GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DataProperty GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DataProperty GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static DataProperty GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static DataPropertyCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static DataPropertyCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DataPropertyColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DataPropertyColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DataPropertyCollection Where(Func<DataPropertyColumns, QueryFilter<DataPropertyColumns>> where, OrderBy<DataPropertyColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DataProperty>();
			return new DataPropertyCollection(database.GetQuery<DataPropertyColumns, DataProperty>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DataPropertyCollection Where(WhereDelegate<DataPropertyColumns> where, Database database = null)
		{		
			database = database ?? Db.For<DataProperty>();
			var results = new DataPropertyCollection(database, database.GetQuery<DataPropertyColumns, DataProperty>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataPropertyCollection Where(WhereDelegate<DataPropertyColumns> where, OrderBy<DataPropertyColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<DataProperty>();
			var results = new DataPropertyCollection(database, database.GetQuery<DataPropertyColumns, DataProperty>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;DataPropertyColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DataPropertyCollection Where(QiQuery where, Database database = null)
		{
			var results = new DataPropertyCollection(database, Select<DataPropertyColumns>.From<DataProperty>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static DataProperty GetOneWhere(QueryFilter where, Database database = null)
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
		public static DataProperty OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DataPropertyColumns> whereDelegate = (c) => where;
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
		public static DataProperty GetOneWhere(WhereDelegate<DataPropertyColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DataPropertyColumns c = new DataPropertyColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DataProperty instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataProperty OneWhere(WhereDelegate<DataPropertyColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DataPropertyColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DataProperty OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataProperty FirstOneWhere(WhereDelegate<DataPropertyColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataProperty FirstOneWhere(WhereDelegate<DataPropertyColumns> where, OrderBy<DataPropertyColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataProperty FirstOneWhere(QueryFilter where, OrderBy<DataPropertyColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DataPropertyColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DataPropertyCollection Top(int count, WhereDelegate<DataPropertyColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static DataPropertyCollection Top(int count, WhereDelegate<DataPropertyColumns> where, OrderBy<DataPropertyColumns> orderBy, Database database = null)
		{
			DataPropertyColumns c = new DataPropertyColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<DataProperty>();
			QuerySet query = GetQuerySet(db); 
			query.Top<DataProperty>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DataPropertyColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DataPropertyCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DataPropertyCollection Top(int count, QueryFilter where, Database database)
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
		public static DataPropertyCollection Top(int count, QueryFilter where, OrderBy<DataPropertyColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DataProperty>();
			QuerySet query = GetQuerySet(db);
			query.Top<DataProperty>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DataPropertyColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DataPropertyCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DataPropertyCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<DataProperty>();
			QuerySet query = GetQuerySet(db);
			query.Top<DataProperty>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<DataPropertyCollection>(0);
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
		public static DataPropertyCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DataProperty>();
			QuerySet query = GetQuerySet(db);
			query.Top<DataProperty>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DataPropertyCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of DataProperties
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<DataProperty>();
            QuerySet query = GetQuerySet(db);
            query.Count<DataProperty>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<DataPropertyColumns> where, Database database = null)
		{
			DataPropertyColumns c = new DataPropertyColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DataProperty>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DataProperty>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<DataProperty>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DataProperty>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static DataProperty CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DataProperty>();			
			var dao = new DataProperty();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static DataProperty OneOrThrow(DataPropertyCollection c)
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
