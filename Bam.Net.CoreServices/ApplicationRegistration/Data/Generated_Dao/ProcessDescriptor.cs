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
	[Bam.Net.Data.Table("ProcessDescriptor", "ApplicationRegistration")]
	public partial class ProcessDescriptor: Bam.Net.Data.Dao
	{
		public ProcessDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ProcessDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ProcessDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ProcessDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ProcessDescriptor(DataRow data)
		{
			return new ProcessDescriptor(data);
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

	// property:InstanceIdentifier, columnName:InstanceIdentifier	
	[Bam.Net.Data.Column(Name="InstanceIdentifier", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string InstanceIdentifier
	{
		get
		{
			return GetStringValue("InstanceIdentifier");
		}
		set
		{
			SetValue("InstanceIdentifier", value);
		}
	}

	// property:HashAlgorithm, columnName:HashAlgorithm	
	[Bam.Net.Data.Column(Name="HashAlgorithm", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string HashAlgorithm
	{
		get
		{
			return GetStringValue("HashAlgorithm");
		}
		set
		{
			SetValue("HashAlgorithm", value);
		}
	}

	// property:Hash, columnName:Hash	
	[Bam.Net.Data.Column(Name="Hash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Hash
	{
		get
		{
			return GetStringValue("Hash");
		}
		set
		{
			SetValue("Hash", value);
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

	// property:StartTime, columnName:StartTime	
	[Bam.Net.Data.Column(Name="StartTime", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? StartTime
	{
		get
		{
			return GetDateTimeValue("StartTime");
		}
		set
		{
			SetValue("StartTime", value);
		}
	}

	// property:HasExited, columnName:HasExited	
	[Bam.Net.Data.Column(Name="HasExited", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? HasExited
	{
		get
		{
			return GetBooleanValue("HasExited");
		}
		set
		{
			SetValue("HasExited", value);
		}
	}

	// property:ExitTime, columnName:ExitTime	
	[Bam.Net.Data.Column(Name="ExitTime", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? ExitTime
	{
		get
		{
			return GetDateTimeValue("ExitTime");
		}
		set
		{
			SetValue("ExitTime", value);
		}
	}

	// property:ExitCode, columnName:ExitCode	
	[Bam.Net.Data.Column(Name="ExitCode", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? ExitCode
	{
		get
		{
			return GetIntValue("ExitCode");
		}
		set
		{
			SetValue("ExitCode", value);
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



	// start ApplicationId -> ApplicationId
	[Bam.Net.Data.ForeignKey(
        Table="ProcessDescriptor",
		Name="ApplicationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
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
        Table="ProcessDescriptor",
		Name="MachineId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
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
				var colFilter = new ProcessDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ProcessDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ProcessDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<ProcessDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<ProcessDescriptor>();
			var results = new ProcessDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ProcessDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ProcessDescriptorColumns columns = new ProcessDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<ProcessDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ProcessDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ProcessDescriptorColumns> where, Action<IEnumerable<ProcessDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ProcessDescriptorColumns columns = new ProcessDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<ProcessDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ProcessDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<ProcessDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<ProcessDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ProcessDescriptorColumns> where, Action<IEnumerable<ProcessDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<ProcessDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ProcessDescriptorColumns columns = new ProcessDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ProcessDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static ProcessDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static ProcessDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ProcessDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ProcessDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ProcessDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ProcessDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ProcessDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ProcessDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ProcessDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ProcessDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ProcessDescriptorCollection Where(Func<ProcessDescriptorColumns, QueryFilter<ProcessDescriptorColumns>> where, OrderBy<ProcessDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ProcessDescriptor>();
			return new ProcessDescriptorCollection(database.GetQuery<ProcessDescriptorColumns, ProcessDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ProcessDescriptorCollection Where(WhereDelegate<ProcessDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ProcessDescriptor>();
			var results = new ProcessDescriptorCollection(database, database.GetQuery<ProcessDescriptorColumns, ProcessDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ProcessDescriptorCollection Where(WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ProcessDescriptor>();
			var results = new ProcessDescriptorCollection(database, database.GetQuery<ProcessDescriptorColumns, ProcessDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ProcessDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ProcessDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new ProcessDescriptorCollection(database, Select<ProcessDescriptorColumns>.From<ProcessDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ProcessDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static ProcessDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ProcessDescriptorColumns> whereDelegate = (c) => where;
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
		public static ProcessDescriptor GetOneWhere(WhereDelegate<ProcessDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ProcessDescriptorColumns c = new ProcessDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ProcessDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ProcessDescriptor OneWhere(WhereDelegate<ProcessDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ProcessDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ProcessDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ProcessDescriptor FirstOneWhere(WhereDelegate<ProcessDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ProcessDescriptor FirstOneWhere(WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ProcessDescriptor FirstOneWhere(QueryFilter where, OrderBy<ProcessDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ProcessDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ProcessDescriptorCollection Top(int count, WhereDelegate<ProcessDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ProcessDescriptorCollection Top(int count, WhereDelegate<ProcessDescriptorColumns> where, OrderBy<ProcessDescriptorColumns> orderBy, Database database = null)
		{
			ProcessDescriptorColumns c = new ProcessDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ProcessDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ProcessDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ProcessDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ProcessDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ProcessDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static ProcessDescriptorCollection Top(int count, QueryFilter where, OrderBy<ProcessDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ProcessDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ProcessDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ProcessDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ProcessDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ProcessDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<ProcessDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ProcessDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ProcessDescriptorCollection>(0);
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
		public static ProcessDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ProcessDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<ProcessDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ProcessDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ProcessDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ProcessDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<ProcessDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProcessDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProcessDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ProcessDescriptorColumns> where, Database database = null)
		{
			ProcessDescriptorColumns c = new ProcessDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ProcessDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ProcessDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ProcessDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ProcessDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ProcessDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ProcessDescriptor>();			
			var dao = new ProcessDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ProcessDescriptor OneOrThrow(ProcessDescriptorCollection c)
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
