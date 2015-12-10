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

namespace Bam.Net.Analytics
{
	// schema = Analytics
	// connection Name = Analytics
	[Serializable]
	[Bam.Net.Data.Table("CustomTimer", "Analytics")]
	public partial class CustomTimer: Dao
	{
		public CustomTimer():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CustomTimer(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CustomTimer(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CustomTimer(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator CustomTimer(DataRow data)
		{
			return new CustomTimer(data);
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

﻿	// property:Description, columnName:Description	
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



﻿	// start TimerId -> TimerId
	[Bam.Net.Data.ForeignKey(
        Table="CustomTimer",
		Name="TimerId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Timer",
		Suffix="1")]
	public long? TimerId
	{
		get
		{
			return GetLongValue("TimerId");
		}
		set
		{
			SetValue("TimerId", value);
		}
	}

	Timer _timerOfTimerId;
	public Timer TimerOfTimerId
	{
		get
		{
			if(_timerOfTimerId == null)
			{
				_timerOfTimerId = Bam.Net.Analytics.Timer.OneWhere(c => c.KeyColumn == this.TimerId, this.Database);
			}
			return _timerOfTimerId;
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
				var colFilter = new CustomTimerColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the CustomTimer table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CustomTimerCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<CustomTimer>();
			Database db = database ?? Db.For<CustomTimer>();
			var results = new CustomTimerCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static CustomTimer GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static CustomTimer GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static CustomTimer GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static CustomTimer GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static CustomTimerCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static CustomTimerCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<CustomTimerColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CustomTimerColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CustomTimerCollection Where(Func<CustomTimerColumns, QueryFilter<CustomTimerColumns>> where, OrderBy<CustomTimerColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<CustomTimer>();
			return new CustomTimerCollection(database.GetQuery<CustomTimerColumns, CustomTimer>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CustomTimerCollection Where(WhereDelegate<CustomTimerColumns> where, Database database = null)
		{		
			database = database ?? Db.For<CustomTimer>();
			var results = new CustomTimerCollection(database, database.GetQuery<CustomTimerColumns, CustomTimer>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static CustomTimerCollection Where(WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<CustomTimer>();
			var results = new CustomTimerCollection(database, database.GetQuery<CustomTimerColumns, CustomTimer>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CustomTimerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CustomTimerCollection Where(QiQuery where, Database database = null)
		{
			var results = new CustomTimerCollection(database, Select<CustomTimerColumns>.From<CustomTimer>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static CustomTimer GetOneWhere(QueryFilter where, Database database = null)
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
		public static CustomTimer OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<CustomTimerColumns> whereDelegate = (c) => where;
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
		public static CustomTimer GetOneWhere(WhereDelegate<CustomTimerColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				CustomTimerColumns c = new CustomTimerColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single CustomTimer instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CustomTimer OneWhere(WhereDelegate<CustomTimerColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CustomTimerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CustomTimer OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CustomTimer FirstOneWhere(WhereDelegate<CustomTimerColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CustomTimer FirstOneWhere(WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CustomTimer FirstOneWhere(QueryFilter where, OrderBy<CustomTimerColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<CustomTimerColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CustomTimerCollection Top(int count, WhereDelegate<CustomTimerColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static CustomTimerCollection Top(int count, WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy, Database database = null)
		{
			CustomTimerColumns c = new CustomTimerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<CustomTimer>();
			QuerySet query = GetQuerySet(db); 
			query.Top<CustomTimer>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CustomTimerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CustomTimerCollection>(0);
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
		public static CustomTimerCollection Top(int count, QueryFilter where, OrderBy<CustomTimerColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<CustomTimer>();
			QuerySet query = GetQuerySet(db);
			query.Top<CustomTimer>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<CustomTimerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CustomTimerCollection>(0);
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
		public static CustomTimerCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<CustomTimer>();
			QuerySet query = GetQuerySet(db);
			query.Top<CustomTimer>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<CustomTimerCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CustomTimerColumns> where, Database database = null)
		{
			CustomTimerColumns c = new CustomTimerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<CustomTimer>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<CustomTimer>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static CustomTimer CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<CustomTimer>();			
			var dao = new CustomTimer();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static CustomTimer OneOrThrow(CustomTimerCollection c)
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
