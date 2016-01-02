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
	[Bam.Net.Data.Table("DeferredJob", "Automation")]
	public partial class DeferredJob: Dao
	{
		public DeferredJob():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DeferredJob(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DeferredJob(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DeferredJob(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator DeferredJob(DataRow data)
		{
			return new DeferredJob(data);
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

﻿	// property:Name, columnName:Name	
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

﻿	// property:JobDirectory, columnName:JobDirectory	
	[Bam.Net.Data.Column(Name="JobDirectory", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string JobDirectory
	{
		get
		{
			return GetStringValue("JobDirectory");
		}
		set
		{
			SetValue("JobDirectory", value);
		}
	}

﻿	// property:HostName, columnName:HostName	
	[Bam.Net.Data.Column(Name="HostName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string HostName
	{
		get
		{
			return GetStringValue("HostName");
		}
		set
		{
			SetValue("HostName", value);
		}
	}

﻿	// property:LastStepNumber, columnName:LastStepNumber	
	[Bam.Net.Data.Column(Name="LastStepNumber", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string LastStepNumber
	{
		get
		{
			return GetStringValue("LastStepNumber");
		}
		set
		{
			SetValue("LastStepNumber", value);
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
				var colFilter = new DeferredJobColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the DeferredJob table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DeferredJobCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<DeferredJob>();
			Database db = database ?? Db.For<DeferredJob>();
			var results = new DeferredJobCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static DeferredJob GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DeferredJob GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DeferredJob GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => c.Uuid == uuid, database);
		}

		public static DeferredJobCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static DeferredJobCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DeferredJobColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DeferredJobColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DeferredJobCollection Where(Func<DeferredJobColumns, QueryFilter<DeferredJobColumns>> where, OrderBy<DeferredJobColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DeferredJob>();
			return new DeferredJobCollection(database.GetQuery<DeferredJobColumns, DeferredJob>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DeferredJobCollection Where(WhereDelegate<DeferredJobColumns> where, Database database = null)
		{		
			database = database ?? Db.For<DeferredJob>();
			var results = new DeferredJobCollection(database, database.GetQuery<DeferredJobColumns, DeferredJob>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static DeferredJobCollection Where(WhereDelegate<DeferredJobColumns> where, OrderBy<DeferredJobColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<DeferredJob>();
			var results = new DeferredJobCollection(database, database.GetQuery<DeferredJobColumns, DeferredJob>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DeferredJobColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DeferredJobCollection Where(QiQuery where, Database database = null)
		{
			var results = new DeferredJobCollection(database, Select<DeferredJobColumns>.From<DeferredJob>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static DeferredJob GetOneWhere(QueryFilter where, Database database = null)
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
		public static DeferredJob OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DeferredJobColumns> whereDelegate = (c) => where;
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
		public static DeferredJob GetOneWhere(WhereDelegate<DeferredJobColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DeferredJobColumns c = new DeferredJobColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DeferredJob instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DeferredJob OneWhere(WhereDelegate<DeferredJobColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DeferredJobColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DeferredJob OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DeferredJob FirstOneWhere(WhereDelegate<DeferredJobColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DeferredJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DeferredJob FirstOneWhere(WhereDelegate<DeferredJobColumns> where, OrderBy<DeferredJobColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DeferredJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DeferredJob FirstOneWhere(QueryFilter where, OrderBy<DeferredJobColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DeferredJobColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DeferredJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DeferredJobCollection Top(int count, WhereDelegate<DeferredJobColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DeferredJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static DeferredJobCollection Top(int count, WhereDelegate<DeferredJobColumns> where, OrderBy<DeferredJobColumns> orderBy, Database database = null)
		{
			DeferredJobColumns c = new DeferredJobColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<DeferredJob>();
			QuerySet query = GetQuerySet(db); 
			query.Top<DeferredJob>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DeferredJobColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DeferredJobCollection>(0);
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
		public static DeferredJobCollection Top(int count, QueryFilter where, OrderBy<DeferredJobColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DeferredJob>();
			QuerySet query = GetQuerySet(db);
			query.Top<DeferredJob>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DeferredJobColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DeferredJobCollection>(0);
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
		public static DeferredJobCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DeferredJob>();
			QuerySet query = GetQuerySet(db);
			query.Top<DeferredJob>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DeferredJobCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeferredJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeferredJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<DeferredJobColumns> where, Database database = null)
		{
			DeferredJobColumns c = new DeferredJobColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DeferredJob>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DeferredJob>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static DeferredJob CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DeferredJob>();			
			var dao = new DeferredJob();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static DeferredJob OneOrThrow(DeferredJobCollection c)
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
