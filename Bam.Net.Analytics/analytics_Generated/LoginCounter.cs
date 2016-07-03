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

namespace Bam.Net.Analytics
{
	// schema = Analytics
	// connection Name = Analytics
	[Serializable]
	[Bam.Net.Data.Table("LoginCounter", "Analytics")]
	public partial class LoginCounter: Dao
	{
		public LoginCounter():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LoginCounter(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LoginCounter(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LoginCounter(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator LoginCounter(DataRow data)
		{
			return new LoginCounter(data);
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



	// start CounterId -> CounterId
	[Bam.Net.Data.ForeignKey(
        Table="LoginCounter",
		Name="CounterId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Counter",
		Suffix="1")]
	public long? CounterId
	{
		get
		{
			return GetLongValue("CounterId");
		}
		set
		{
			SetValue("CounterId", value);
		}
	}

	Counter _counterOfCounterId;
	public Counter CounterOfCounterId
	{
		get
		{
			if(_counterOfCounterId == null)
			{
				_counterOfCounterId = Bam.Net.Analytics.Counter.OneWhere(c => c.KeyColumn == this.CounterId, this.Database);
			}
			return _counterOfCounterId;
		}
	}
	
	// start UserIdentifierId -> UserIdentifierId
	[Bam.Net.Data.ForeignKey(
        Table="LoginCounter",
		Name="UserIdentifierId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="UserIdentifier",
		Suffix="2")]
	public long? UserIdentifierId
	{
		get
		{
			return GetLongValue("UserIdentifierId");
		}
		set
		{
			SetValue("UserIdentifierId", value);
		}
	}

	UserIdentifier _userIdentifierOfUserIdentifierId;
	public UserIdentifier UserIdentifierOfUserIdentifierId
	{
		get
		{
			if(_userIdentifierOfUserIdentifierId == null)
			{
				_userIdentifierOfUserIdentifierId = Bam.Net.Analytics.UserIdentifier.OneWhere(c => c.KeyColumn == this.UserIdentifierId, this.Database);
			}
			return _userIdentifierOfUserIdentifierId;
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
				var colFilter = new LoginCounterColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the LoginCounter table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static LoginCounterCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<LoginCounter>();
			Database db = database ?? Db.For<LoginCounter>();
			var results = new LoginCounterCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<LoginCounterCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				LoginCounterColumns columns = new LoginCounterColumns();
				var orderBy = Order.By<LoginCounterColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<LoginCounterCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<LoginCounterColumns> where, Func<LoginCounterCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				LoginCounterColumns columns = new LoginCounterColumns();
				var orderBy = Order.By<LoginCounterColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (LoginCounterColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static LoginCounter GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static LoginCounter GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static LoginCounter GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static LoginCounter GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static LoginCounterCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static LoginCounterCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<LoginCounterColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a LoginCounterColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginCounterCollection Where(Func<LoginCounterColumns, QueryFilter<LoginCounterColumns>> where, OrderBy<LoginCounterColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<LoginCounter>();
			return new LoginCounterCollection(database.GetQuery<LoginCounterColumns, LoginCounter>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginCounterCollection Where(WhereDelegate<LoginCounterColumns> where, Database database = null)
		{		
			database = database ?? Db.For<LoginCounter>();
			var results = new LoginCounterCollection(database, database.GetQuery<LoginCounterColumns, LoginCounter>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static LoginCounterCollection Where(WhereDelegate<LoginCounterColumns> where, OrderBy<LoginCounterColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<LoginCounter>();
			var results = new LoginCounterCollection(database, database.GetQuery<LoginCounterColumns, LoginCounter>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;LoginCounterColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static LoginCounterCollection Where(QiQuery where, Database database = null)
		{
			var results = new LoginCounterCollection(database, Select<LoginCounterColumns>.From<LoginCounter>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static LoginCounter GetOneWhere(QueryFilter where, Database database = null)
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
		public static LoginCounter OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<LoginCounterColumns> whereDelegate = (c) => where;
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
		public static LoginCounter GetOneWhere(WhereDelegate<LoginCounterColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				LoginCounterColumns c = new LoginCounterColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single LoginCounter instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LoginCounter OneWhere(WhereDelegate<LoginCounterColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LoginCounterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static LoginCounter OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LoginCounter FirstOneWhere(WhereDelegate<LoginCounterColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LoginCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LoginCounter FirstOneWhere(WhereDelegate<LoginCounterColumns> where, OrderBy<LoginCounterColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LoginCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LoginCounter FirstOneWhere(QueryFilter where, OrderBy<LoginCounterColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<LoginCounterColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a LoginCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LoginCounterCollection Top(int count, WhereDelegate<LoginCounterColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LoginCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static LoginCounterCollection Top(int count, WhereDelegate<LoginCounterColumns> where, OrderBy<LoginCounterColumns> orderBy, Database database = null)
		{
			LoginCounterColumns c = new LoginCounterColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<LoginCounter>();
			QuerySet query = GetQuerySet(db); 
			query.Top<LoginCounter>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LoginCounterColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LoginCounterCollection>(0);
			results.Database = db;
			return results;
		}

		public static LoginCounterCollection Top(int count, QueryFilter where, Database database)
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
		/// <param name="db"></param>
		public static LoginCounterCollection Top(int count, QueryFilter where, OrderBy<LoginCounterColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<LoginCounter>();
			QuerySet query = GetQuerySet(db);
			query.Top<LoginCounter>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<LoginCounterColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LoginCounterCollection>(0);
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
		public static LoginCounterCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<LoginCounter>();
			QuerySet query = GetQuerySet(db);
			query.Top<LoginCounter>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<LoginCounterCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<LoginCounterColumns> where, Database database = null)
		{
			LoginCounterColumns c = new LoginCounterColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<LoginCounter>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<LoginCounter>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static LoginCounter CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<LoginCounter>();			
			var dao = new LoginCounter();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static LoginCounter OneOrThrow(LoginCounterCollection c)
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
