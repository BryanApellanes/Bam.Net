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

namespace Bam.Net.Automation.Testing.Data.Dao
{
	// schema = Testing
	// connection Name = Testing
	[Serializable]
	[Bam.Net.Data.Table("TestExecution", "Testing")]
	public partial class TestExecution: Bam.Net.Data.Dao
	{
		public TestExecution():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TestExecution(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TestExecution(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TestExecution(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator TestExecution(DataRow data)
		{
			return new TestExecution(data);
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

	// property:StartedTime, columnName:StartedTime	
	[Bam.Net.Data.Column(Name="StartedTime", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? StartedTime
	{
		get
		{
			return GetDateTimeValue("StartedTime");
		}
		set
		{
			SetValue("StartedTime", value);
		}
	}

	// property:FinishedTime, columnName:FinishedTime	
	[Bam.Net.Data.Column(Name="FinishedTime", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? FinishedTime
	{
		get
		{
			return GetDateTimeValue("FinishedTime");
		}
		set
		{
			SetValue("FinishedTime", value);
		}
	}

	// property:Passed, columnName:Passed	
	[Bam.Net.Data.Column(Name="Passed", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? Passed
	{
		get
		{
			return GetBooleanValue("Passed");
		}
		set
		{
			SetValue("Passed", value);
		}
	}

	// property:Exception, columnName:Exception	
	[Bam.Net.Data.Column(Name="Exception", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Exception
	{
		get
		{
			return GetStringValue("Exception");
		}
		set
		{
			SetValue("Exception", value);
		}
	}

	// property:StackTrace, columnName:StackTrace	
	[Bam.Net.Data.Column(Name="StackTrace", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string StackTrace
	{
		get
		{
			return GetStringValue("StackTrace");
		}
		set
		{
			SetValue("StackTrace", value);
		}
	}

	// property:Tag, columnName:Tag	
	[Bam.Net.Data.Column(Name="Tag", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Tag
	{
		get
		{
			return GetStringValue("Tag");
		}
		set
		{
			SetValue("Tag", value);
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



	// start TestDefinitionId -> TestDefinitionId
	[Bam.Net.Data.ForeignKey(
        Table="TestExecution",
		Name="TestDefinitionId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="TestDefinition",
		Suffix="1")]
	public ulong? TestDefinitionId
	{
		get
		{
			return GetULongValue("TestDefinitionId");
		}
		set
		{
			SetValue("TestDefinitionId", value);
		}
	}

	TestDefinition _testDefinitionOfTestDefinitionId;
	public TestDefinition TestDefinitionOfTestDefinitionId
	{
		get
		{
			if(_testDefinitionOfTestDefinitionId == null)
			{
				_testDefinitionOfTestDefinitionId = Bam.Net.Automation.Testing.Data.Dao.TestDefinition.OneWhere(c => c.KeyColumn == this.TestDefinitionId, this.Database);
			}
			return _testDefinitionOfTestDefinitionId;
		}
	}
	
	// start TestSuiteExecutionSummaryId -> TestSuiteExecutionSummaryId
	[Bam.Net.Data.ForeignKey(
        Table="TestExecution",
		Name="TestSuiteExecutionSummaryId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="TestSuiteExecutionSummary",
		Suffix="2")]
	public ulong? TestSuiteExecutionSummaryId
	{
		get
		{
			return GetULongValue("TestSuiteExecutionSummaryId");
		}
		set
		{
			SetValue("TestSuiteExecutionSummaryId", value);
		}
	}

	TestSuiteExecutionSummary _testSuiteExecutionSummaryOfTestSuiteExecutionSummaryId;
	public TestSuiteExecutionSummary TestSuiteExecutionSummaryOfTestSuiteExecutionSummaryId
	{
		get
		{
			if(_testSuiteExecutionSummaryOfTestSuiteExecutionSummaryId == null)
			{
				_testSuiteExecutionSummaryOfTestSuiteExecutionSummaryId = Bam.Net.Automation.Testing.Data.Dao.TestSuiteExecutionSummary.OneWhere(c => c.KeyColumn == this.TestSuiteExecutionSummaryId, this.Database);
			}
			return _testSuiteExecutionSummaryOfTestSuiteExecutionSummaryId;
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
				var colFilter = new TestExecutionColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the TestExecution table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static TestExecutionCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<TestExecution>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<TestExecution>();
			var results = new TestExecutionCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<TestExecution>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				TestExecutionColumns columns = new TestExecutionColumns();
				var orderBy = Bam.Net.Data.Order.By<TestExecutionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<TestExecution>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<TestExecutionColumns> where, Action<IEnumerable<TestExecution>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				TestExecutionColumns columns = new TestExecutionColumns();
				var orderBy = Bam.Net.Data.Order.By<TestExecutionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (TestExecutionColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<TestExecution>> batchProcessor, Bam.Net.Data.OrderBy<TestExecutionColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<TestExecutionColumns> where, Action<IEnumerable<TestExecution>> batchProcessor, Bam.Net.Data.OrderBy<TestExecutionColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				TestExecutionColumns columns = new TestExecutionColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (TestExecutionColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static TestExecution GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static TestExecution GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static TestExecution GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static TestExecution GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static TestExecution GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static TestExecution GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static TestExecutionCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static TestExecutionCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<TestExecutionColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a TestExecutionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static TestExecutionCollection Where(Func<TestExecutionColumns, QueryFilter<TestExecutionColumns>> where, OrderBy<TestExecutionColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<TestExecution>();
			return new TestExecutionCollection(database.GetQuery<TestExecutionColumns, TestExecution>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static TestExecutionCollection Where(WhereDelegate<TestExecutionColumns> where, Database database = null)
		{		
			database = database ?? Db.For<TestExecution>();
			var results = new TestExecutionCollection(database, database.GetQuery<TestExecutionColumns, TestExecution>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestExecutionCollection Where(WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<TestExecution>();
			var results = new TestExecutionCollection(database, database.GetQuery<TestExecutionColumns, TestExecution>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;TestExecutionColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TestExecutionCollection Where(QiQuery where, Database database = null)
		{
			var results = new TestExecutionCollection(database, Select<TestExecutionColumns>.From<TestExecution>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static TestExecution GetOneWhere(QueryFilter where, Database database = null)
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
		public static TestExecution OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<TestExecutionColumns> whereDelegate = (c) => where;
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
		public static TestExecution GetOneWhere(WhereDelegate<TestExecutionColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				TestExecutionColumns c = new TestExecutionColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single TestExecution instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestExecution OneWhere(WhereDelegate<TestExecutionColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TestExecutionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TestExecution OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestExecution FirstOneWhere(WhereDelegate<TestExecutionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestExecution FirstOneWhere(WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestExecution FirstOneWhere(QueryFilter where, OrderBy<TestExecutionColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<TestExecutionColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestExecutionCollection Top(int count, WhereDelegate<TestExecutionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static TestExecutionCollection Top(int count, WhereDelegate<TestExecutionColumns> where, OrderBy<TestExecutionColumns> orderBy, Database database = null)
		{
			TestExecutionColumns c = new TestExecutionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<TestExecution>();
			QuerySet query = GetQuerySet(db); 
			query.Top<TestExecution>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TestExecutionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TestExecutionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static TestExecutionCollection Top(int count, QueryFilter where, Database database)
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
		public static TestExecutionCollection Top(int count, QueryFilter where, OrderBy<TestExecutionColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<TestExecution>();
			QuerySet query = GetQuerySet(db);
			query.Top<TestExecution>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<TestExecutionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TestExecutionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static TestExecutionCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<TestExecution>();
			QuerySet query = GetQuerySet(db);
			query.Top<TestExecution>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<TestExecutionCollection>(0);
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
		public static TestExecutionCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<TestExecution>();
			QuerySet query = GetQuerySet(db);
			query.Top<TestExecution>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<TestExecutionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of TestExecutions
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<TestExecution>();
            QuerySet query = GetQuerySet(db);
            query.Count<TestExecution>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestExecutionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestExecutionColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<TestExecutionColumns> where, Database database = null)
		{
			TestExecutionColumns c = new TestExecutionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<TestExecution>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<TestExecution>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<TestExecution>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<TestExecution>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static TestExecution CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<TestExecution>();			
			var dao = new TestExecution();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static TestExecution OneOrThrow(TestExecutionCollection c)
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
