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

namespace Bam.Net.Analytics
{
	// schema = Analytics
	// connection Name = Analytics
	[Serializable]
	[Bam.Net.Data.Table("Counter", "Analytics")]
	public partial class Counter: Bam.Net.Data.Dao
	{
		public Counter():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Counter(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Counter(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Counter(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Counter(DataRow data)
		{
			return new Counter(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("MethodCounter_CounterId", new MethodCounterCollection(Database.GetQuery<MethodCounterColumns, MethodCounter>((c) => c.CounterId == GetULongValue("Id")), this, "CounterId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("LoadCounter_CounterId", new LoadCounterCollection(Database.GetQuery<LoadCounterColumns, LoadCounter>((c) => c.CounterId == GetULongValue("Id")), this, "CounterId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("ClickCounter_CounterId", new ClickCounterCollection(Database.GetQuery<ClickCounterColumns, ClickCounter>((c) => c.CounterId == GetULongValue("Id")), this, "CounterId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("LoginCounter_CounterId", new LoginCounterCollection(Database.GetQuery<LoginCounterColumns, LoginCounter>((c) => c.CounterId == GetULongValue("Id")), this, "CounterId"));				
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

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="Int", MaxLength="10", AllowNull=false)]
	public int? Value
	{
		get
		{
			return GetIntValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				

	[Bam.Net.Exclude]	
	public MethodCounterCollection MethodCountersByCounterId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("MethodCounter_CounterId"))
			{
				SetChildren();
			}

			var c = (MethodCounterCollection)this.ChildCollections["MethodCounter_CounterId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public LoadCounterCollection LoadCountersByCounterId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("LoadCounter_CounterId"))
			{
				SetChildren();
			}

			var c = (LoadCounterCollection)this.ChildCollections["LoadCounter_CounterId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public ClickCounterCollection ClickCountersByCounterId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ClickCounter_CounterId"))
			{
				SetChildren();
			}

			var c = (ClickCounterCollection)this.ChildCollections["ClickCounter_CounterId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public LoginCounterCollection LoginCountersByCounterId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("LoginCounter_CounterId"))
			{
				SetChildren();
			}

			var c = (LoginCounterCollection)this.ChildCollections["LoginCounter_CounterId"];
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
				var colFilter = new CounterColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Counter table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CounterCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Counter>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Counter>();
			var results = new CounterCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Counter>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CounterColumns columns = new CounterColumns();
				var orderBy = Bam.Net.Data.Order.By<CounterColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Counter>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<CounterColumns> where, Action<IEnumerable<Counter>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CounterColumns columns = new CounterColumns();
				var orderBy = Bam.Net.Data.Order.By<CounterColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (CounterColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Counter>> batchProcessor, Bam.Net.Data.OrderBy<CounterColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<CounterColumns> where, Action<IEnumerable<Counter>> batchProcessor, Bam.Net.Data.OrderBy<CounterColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				CounterColumns columns = new CounterColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (CounterColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Counter GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Counter GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Counter GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Counter GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Counter GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Counter GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static CounterCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static CounterCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<CounterColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CounterColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CounterCollection Where(Func<CounterColumns, QueryFilter<CounterColumns>> where, OrderBy<CounterColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Counter>();
			return new CounterCollection(database.GetQuery<CounterColumns, Counter>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static CounterCollection Where(WhereDelegate<CounterColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Counter>();
			var results = new CounterCollection(database, database.GetQuery<CounterColumns, Counter>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CounterCollection Where(WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Counter>();
			var results = new CounterCollection(database, database.GetQuery<CounterColumns, Counter>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;CounterColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CounterCollection Where(QiQuery where, Database database = null)
		{
			var results = new CounterCollection(database, Select<CounterColumns>.From<Counter>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Counter GetOneWhere(QueryFilter where, Database database = null)
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
		public static Counter OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<CounterColumns> whereDelegate = (c) => where;
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
		public static Counter GetOneWhere(WhereDelegate<CounterColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				CounterColumns c = new CounterColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Counter instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Counter OneWhere(WhereDelegate<CounterColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CounterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Counter OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Counter FirstOneWhere(WhereDelegate<CounterColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Counter FirstOneWhere(WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Counter FirstOneWhere(QueryFilter where, OrderBy<CounterColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<CounterColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static CounterCollection Top(int count, WhereDelegate<CounterColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static CounterCollection Top(int count, WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy, Database database = null)
		{
			CounterColumns c = new CounterColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Counter>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Counter>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CounterColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CounterCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static CounterCollection Top(int count, QueryFilter where, Database database)
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
		public static CounterCollection Top(int count, QueryFilter where, OrderBy<CounterColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Counter>();
			QuerySet query = GetQuerySet(db);
			query.Top<Counter>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<CounterColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CounterCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static CounterCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Counter>();
			QuerySet query = GetQuerySet(db);
			query.Top<Counter>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<CounterCollection>(0);
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
		public static CounterCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Counter>();
			QuerySet query = GetQuerySet(db);
			query.Top<Counter>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<CounterCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Counters
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Counter>();
            QuerySet query = GetQuerySet(db);
            query.Count<Counter>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<CounterColumns> where, Database database = null)
		{
			CounterColumns c = new CounterColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Counter>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Counter>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Counter>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Counter>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Counter CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Counter>();			
			var dao = new Counter();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Counter OneOrThrow(CounterCollection c)
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
