/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Automation.Data
{
	// schema = Automation
	// connection Name = Automation
	[Serializable]
	[Bam.Net.Data.Table("RunningJob", "Automation")]
	public partial class RunningJob: Dao
	{
		public RunningJob():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public RunningJob(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public RunningJob(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public RunningJob(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator RunningJob(DataRow data)
		{
			return new RunningJob(data);
		}

		private void SetChildren()
		{
						
		}

﻿	// property:Id, columnName:Id	
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

﻿	// property:Uuid, columnName:Uuid	
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

﻿	// property:Success, columnName:Success	
	[Bam.Net.Data.Column(Name="Success", DbDataType="Bit", MaxLength="1", AllowNull=false)]
	public bool? Success
	{
		get
		{
			return GetBooleanValue("Success");
		}
		set
		{
			SetValue("Success", value);
		}
	}

﻿	// property:Message, columnName:Message	
	[Bam.Net.Data.Column(Name="Message", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Message
	{
		get
		{
			return GetStringValue("Message");
		}
		set
		{
			SetValue("Message", value);
		}
	}

﻿	// property:BuildJobId, columnName:BuildJobId	
	[Bam.Net.Data.Column(Name="BuildJobId", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public long? BuildJobId
	{
		get
		{
			return GetLongValue("BuildJobId");
		}
		set
		{
			SetValue("BuildJobId", value);
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
				var colFilter = new RunningJobColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the RunningJob table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static RunningJobCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<RunningJob>();
			Database db = database ?? Db.For<RunningJob>();
			var results = new RunningJobCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static RunningJob GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static RunningJob GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static RunningJob GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => c.Uuid == uuid, database);
		}

		public static RunningJobCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static RunningJobCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<RunningJobColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a RunningJobColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RunningJobCollection Where(Func<RunningJobColumns, QueryFilter<RunningJobColumns>> where, OrderBy<RunningJobColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<RunningJob>();
			return new RunningJobCollection(database.GetQuery<RunningJobColumns, RunningJob>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RunningJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RunningJobCollection Where(WhereDelegate<RunningJobColumns> where, Database database = null)
		{		
			database = database ?? Db.For<RunningJob>();
			var results = new RunningJobCollection(database, database.GetQuery<RunningJobColumns, RunningJob>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RunningJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static RunningJobCollection Where(WhereDelegate<RunningJobColumns> where, OrderBy<RunningJobColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<RunningJob>();
			var results = new RunningJobCollection(database, database.GetQuery<RunningJobColumns, RunningJob>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RunningJobColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static RunningJobCollection Where(QiQuery where, Database database = null)
		{
			var results = new RunningJobCollection(database, Select<RunningJobColumns>.From<RunningJob>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static RunningJob GetOneWhere(QueryFilter where, Database database = null)
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
		public static RunningJob OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<RunningJobColumns> whereDelegate = (c) => where;
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
		public static RunningJob GetOneWhere(WhereDelegate<RunningJobColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				RunningJobColumns c = new RunningJobColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single RunningJob instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RunningJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static RunningJob OneWhere(WhereDelegate<RunningJobColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RunningJobColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static RunningJob OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RunningJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static RunningJob FirstOneWhere(WhereDelegate<RunningJobColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a RunningJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static RunningJob FirstOneWhere(WhereDelegate<RunningJobColumns> where, OrderBy<RunningJobColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a RunningJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static RunningJob FirstOneWhere(QueryFilter where, OrderBy<RunningJobColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<RunningJobColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a RunningJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static RunningJobCollection Top(int count, WhereDelegate<RunningJobColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a RunningJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static RunningJobCollection Top(int count, WhereDelegate<RunningJobColumns> where, OrderBy<RunningJobColumns> orderBy, Database database = null)
		{
			RunningJobColumns c = new RunningJobColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<RunningJob>();
			QuerySet query = GetQuerySet(db); 
			query.Top<RunningJob>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<RunningJobColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<RunningJobCollection>(0);
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
		public static RunningJobCollection Top(int count, QueryFilter where, OrderBy<RunningJobColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<RunningJob>();
			QuerySet query = GetQuerySet(db);
			query.Top<RunningJob>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<RunningJobColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<RunningJobCollection>(0);
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
		public static RunningJobCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<RunningJob>();
			QuerySet query = GetQuerySet(db);
			query.Top<RunningJob>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<RunningJobCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RunningJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RunningJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<RunningJobColumns> where, Database database = null)
		{
			RunningJobColumns c = new RunningJobColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<RunningJob>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<RunningJob>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static RunningJob CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<RunningJob>();			
			var dao = new RunningJob();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static RunningJob OneOrThrow(RunningJobCollection c)
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
