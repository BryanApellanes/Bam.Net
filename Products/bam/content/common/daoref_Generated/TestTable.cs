/*
	This file was generated and should not be modified directly
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.DaoRef
{
	// schema = DaoRef
	// connection Name = DaoRef
	[Serializable]
	[Bam.Net.Data.Table("TestTable", "DaoRef")]
	public partial class TestTable: Dao
	{
		public TestTable():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TestTable(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TestTable(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TestTable(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator TestTable(DataRow data)
		{
			return new TestTable(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("TestFkTable_TestTableId", new TestFkTableCollection(Database.GetQuery<TestFkTableColumns, TestFkTable>((c) => c.TestTableId == GetLongValue("Id")), this, "TestTableId"));							
		}

	// property:Id, columnName:Id	
	[Exclude]
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

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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

	// property:Description, columnName:Description	
	[Bam.Net.Data.Column(Name="Description", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Description
	{
		get
		{
			return GetStringValue("Description");
		}
		set
		{
			SetValue("Description", value);
		}
	}



				

	[Exclude]	
	public TestFkTableCollection TestFkTablesByTestTableId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("TestFkTable_TestTableId"))
			{
				SetChildren();
			}

			var c = (TestFkTableCollection)this.ChildCollections["TestFkTable_TestTableId"];
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
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new TestTableColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the TestTable table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static TestTableCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<TestTable>();
			Database db = database ?? Db.For<TestTable>();
			var results = new TestTableCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<TestTableCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				TestTableColumns columns = new TestTableColumns();
				var orderBy = Order.By<TestTableColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<TestTableCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<TestTableColumns> where, Func<TestTableCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				TestTableColumns columns = new TestTableColumns();
				var orderBy = Order.By<TestTableColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (TestTableColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static TestTable GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static TestTable GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static TestTable GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static TestTable GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static TestTableCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static TestTableCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<TestTableColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a TestTableColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TestTableCollection Where(Func<TestTableColumns, QueryFilter<TestTableColumns>> where, OrderBy<TestTableColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<TestTable>();
			return new TestTableCollection(database.GetQuery<TestTableColumns, TestTable>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestTableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TestTableCollection Where(WhereDelegate<TestTableColumns> where, Database database = null)
		{		
			database = database ?? Db.For<TestTable>();
			var results = new TestTableCollection(database, database.GetQuery<TestTableColumns, TestTable>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestTableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static TestTableCollection Where(WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<TestTable>();
			var results = new TestTableCollection(database, database.GetQuery<TestTableColumns, TestTable>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;TestTableColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TestTableCollection Where(QiQuery where, Database database = null)
		{
			var results = new TestTableCollection(database, Select<TestTableColumns>.From<TestTable>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static TestTable GetOneWhere(QueryFilter where, Database database = null)
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
		public static TestTable OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<TestTableColumns> whereDelegate = (c) => where;
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
		public static TestTable GetOneWhere(WhereDelegate<TestTableColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				TestTableColumns c = new TestTableColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single TestTable instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestTableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TestTable OneWhere(WhereDelegate<TestTableColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TestTableColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TestTable OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestTableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TestTable FirstOneWhere(WhereDelegate<TestTableColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TestTableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TestTable FirstOneWhere(WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TestTableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TestTable FirstOneWhere(QueryFilter where, OrderBy<TestTableColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<TestTableColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a TestTableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TestTableCollection Top(int count, WhereDelegate<TestTableColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TestTableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static TestTableCollection Top(int count, WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy, Database database = null)
		{
			TestTableColumns c = new TestTableColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<TestTable>();
			QuerySet query = GetQuerySet(db); 
			query.Top<TestTable>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TestTableColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TestTableCollection>(0);
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
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static TestTableCollection Top(int count, QueryFilter where, OrderBy<TestTableColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<TestTable>();
			QuerySet query = GetQuerySet(db);
			query.Top<TestTable>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<TestTableColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TestTableCollection>(0);
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
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static TestTableCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<TestTable>();
			QuerySet query = GetQuerySet(db);
			query.Top<TestTable>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<TestTableCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TestTableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TestTableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<TestTableColumns> where, Database database = null)
		{
			TestTableColumns c = new TestTableColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<TestTable>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<TestTable>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static TestTable CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<TestTable>();			
			var dao = new TestTable();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static TestTable OneOrThrow(TestTableCollection c)
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
