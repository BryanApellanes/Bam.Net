/*
	This file was generated and should not be modified directly
*/
// Model is Table

namespace Bam.Net.Server.Tests.Dao
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Common;
	using System.Linq;
	using System.Threading.Tasks;
	using Bam.Net.Data;
	using Bam.Net.Data.Qi;

	// schema = _b78ecae83f4076d455c3057554d9fb44737b24b0_Dao
	// connection Name = _b78ecae83f4076d455c3057554d9fb44737b24b0_Dao
	[Serializable]
	[Bam.Net.Data.Table("TestStudent", "_b78ecae83f4076d455c3057554d9fb44737b24b0_Dao")]
	public partial class TestStudent: Bam.Net.Data.Dao
	{
		public TestStudent():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TestStudent(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TestStudent(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TestStudent(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator TestStudent(DataRow data)
		{
			return new TestStudent(data);
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



	// start TestClassId -> TestClassId
	[Bam.Net.Data.ForeignKey(
        Table="TestStudent",
		Name="TestClassId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="TestClass",
		Suffix="1")]
	public ulong? TestClassId
	{
		get
		{
			return GetULongValue("TestClassId");
		}
		set
		{
			SetValue("TestClassId", value);
		}
	}

	TestClass _testClassOfTestClassId;
	public TestClass TestClassOfTestClassId
	{
		get
		{
			if(_testClassOfTestClassId == null)
			{
				_testClassOfTestClassId = Bam.Net.Server.Tests.Dao.TestClass.OneWhere(c => c.KeyColumn == this.TestClassId, this.Database);
			}
			return _testClassOfTestClassId;
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
				var colFilter = new TestStudentColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the TestStudent table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static TestStudentCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<TestStudent>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<TestStudent>();
			var results = new TestStudentCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<TestStudent>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				TestStudentColumns columns = new TestStudentColumns();
				var orderBy = Bam.Net.Data.Order.By<TestStudentColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<TestStudent>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<TestStudentColumns> where, Action<IEnumerable<TestStudent>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				TestStudentColumns columns = new TestStudentColumns();
				var orderBy = Bam.Net.Data.Order.By<TestStudentColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (TestStudentColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<TestStudent>> batchProcessor, Bam.Net.Data.OrderBy<TestStudentColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<TestStudentColumns> where, Action<IEnumerable<TestStudent>> batchProcessor, Bam.Net.Data.OrderBy<TestStudentColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				TestStudentColumns columns = new TestStudentColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (TestStudentColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static TestStudent GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static TestStudent GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static TestStudent GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static TestStudent GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static TestStudent GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static TestStudent GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static TestStudentCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static TestStudentCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<TestStudentColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a TestStudentColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static TestStudentCollection Where(Func<TestStudentColumns, QueryFilter<TestStudentColumns>> where, OrderBy<TestStudentColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<TestStudent>();
			return new TestStudentCollection(database.GetQuery<TestStudentColumns, TestStudent>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestStudentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static TestStudentCollection Where(WhereDelegate<TestStudentColumns> where, Database database = null)
		{		
			database = database ?? Db.For<TestStudent>();
			var results = new TestStudentCollection(database, database.GetQuery<TestStudentColumns, TestStudent>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestStudentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestStudentCollection Where(WhereDelegate<TestStudentColumns> where, OrderBy<TestStudentColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<TestStudent>();
			var results = new TestStudentCollection(database, database.GetQuery<TestStudentColumns, TestStudent>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;TestStudentColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TestStudentCollection Where(QiQuery where, Database database = null)
		{
			var results = new TestStudentCollection(database, Select<TestStudentColumns>.From<TestStudent>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static TestStudent GetOneWhere(QueryFilter where, Database database = null)
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
		public static TestStudent OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<TestStudentColumns> whereDelegate = (c) => where;
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
		public static TestStudent GetOneWhere(WhereDelegate<TestStudentColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				TestStudentColumns c = new TestStudentColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single TestStudent instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestStudentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestStudent OneWhere(WhereDelegate<TestStudentColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TestStudentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TestStudent OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestStudentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestStudent FirstOneWhere(WhereDelegate<TestStudentColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TestStudentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestStudent FirstOneWhere(WhereDelegate<TestStudentColumns> where, OrderBy<TestStudentColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TestStudentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestStudent FirstOneWhere(QueryFilter where, OrderBy<TestStudentColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<TestStudentColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a TestStudentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static TestStudentCollection Top(int count, WhereDelegate<TestStudentColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TestStudentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static TestStudentCollection Top(int count, WhereDelegate<TestStudentColumns> where, OrderBy<TestStudentColumns> orderBy, Database database = null)
		{
			TestStudentColumns c = new TestStudentColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<TestStudent>();
			QuerySet query = GetQuerySet(db); 
			query.Top<TestStudent>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TestStudentColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TestStudentCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static TestStudentCollection Top(int count, QueryFilter where, Database database)
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
		public static TestStudentCollection Top(int count, QueryFilter where, OrderBy<TestStudentColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<TestStudent>();
			QuerySet query = GetQuerySet(db);
			query.Top<TestStudent>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<TestStudentColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TestStudentCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static TestStudentCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<TestStudent>();
			QuerySet query = GetQuerySet(db);
			query.Top<TestStudent>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<TestStudentCollection>(0);
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
		public static TestStudentCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<TestStudent>();
			QuerySet query = GetQuerySet(db);
			query.Top<TestStudent>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<TestStudentCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of TestStudents
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<TestStudent>();
            QuerySet query = GetQuerySet(db);
            query.Count<TestStudent>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestStudentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestStudentColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<TestStudentColumns> where, Database database = null)
		{
			TestStudentColumns c = new TestStudentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<TestStudent>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<TestStudent>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<TestStudent>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<TestStudent>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static TestStudent CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<TestStudent>();			
			var dao = new TestStudent();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static TestStudent OneOrThrow(TestStudentCollection c)
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
