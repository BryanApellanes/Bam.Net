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

namespace Bam.Net.UserAccounts.Data
{
	// schema = UserAccounts
	// connection Name = UserAccounts
	[Serializable]
	[Bam.Net.Data.Table("Session", "UserAccounts")]
	public partial class Session: Bam.Net.Data.Dao
	{
		public Session():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Session(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Session(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Session(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Session(DataRow data)
		{
			return new Session(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("SessionState_SessionId", new SessionStateCollection(Database.GetQuery<SessionStateColumns, SessionState>((c) => c.SessionId == GetULongValue("Id")), this, "SessionId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("UserBehavior_SessionId", new UserBehaviorCollection(Database.GetQuery<UserBehaviorColumns, UserBehavior>((c) => c.SessionId == GetULongValue("Id")), this, "SessionId"));				
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

	// property:Identifier, columnName:Identifier	
	[Bam.Net.Data.Column(Name="Identifier", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Identifier
	{
		get
		{
			return GetStringValue("Identifier");
		}
		set
		{
			SetValue("Identifier", value);
		}
	}

	// property:CreationDate, columnName:CreationDate	
	[Bam.Net.Data.Column(Name="CreationDate", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? CreationDate
	{
		get
		{
			return GetDateTimeValue("CreationDate");
		}
		set
		{
			SetValue("CreationDate", value);
		}
	}

	// property:LastActivity, columnName:LastActivity	
	[Bam.Net.Data.Column(Name="LastActivity", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? LastActivity
	{
		get
		{
			return GetDateTimeValue("LastActivity");
		}
		set
		{
			SetValue("LastActivity", value);
		}
	}

	// property:IsActive, columnName:IsActive	
	[Bam.Net.Data.Column(Name="IsActive", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsActive
	{
		get
		{
			return GetBooleanValue("IsActive");
		}
		set
		{
			SetValue("IsActive", value);
		}
	}



	// start UserId -> UserId
	[Bam.Net.Data.ForeignKey(
        Table="Session",
		Name="UserId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
	public ulong? UserId
	{
		get
		{
			return GetULongValue("UserId");
		}
		set
		{
			SetValue("UserId", value);
		}
	}

	User _userOfUserId;
	public User UserOfUserId
	{
		get
		{
			if(_userOfUserId == null)
			{
				_userOfUserId = Bam.Net.UserAccounts.Data.User.OneWhere(c => c.KeyColumn == this.UserId, this.Database);
			}
			return _userOfUserId;
		}
	}
	
				

	[Bam.Net.Exclude]	
	public SessionStateCollection SessionStatesBySessionId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("SessionState_SessionId"))
			{
				SetChildren();
			}

			var c = (SessionStateCollection)this.ChildCollections["SessionState_SessionId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public UserBehaviorCollection UserBehaviorsBySessionId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("UserBehavior_SessionId"))
			{
				SetChildren();
			}

			var c = (UserBehaviorCollection)this.ChildCollections["UserBehavior_SessionId"];
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
				var colFilter = new SessionColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Session table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SessionCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Session>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Session>();
			var results = new SessionCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Session>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				SessionColumns columns = new SessionColumns();
				var orderBy = Bam.Net.Data.Order.By<SessionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Session>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<SessionColumns> where, Action<IEnumerable<Session>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				SessionColumns columns = new SessionColumns();
				var orderBy = Bam.Net.Data.Order.By<SessionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (SessionColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Session>> batchProcessor, Bam.Net.Data.OrderBy<SessionColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<SessionColumns> where, Action<IEnumerable<Session>> batchProcessor, Bam.Net.Data.OrderBy<SessionColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				SessionColumns columns = new SessionColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (SessionColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Session GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Session GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Session GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Session GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Session GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Session GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static SessionCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static SessionCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<SessionColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SessionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static SessionCollection Where(Func<SessionColumns, QueryFilter<SessionColumns>> where, OrderBy<SessionColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Session>();
			return new SessionCollection(database.GetQuery<SessionColumns, Session>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static SessionCollection Where(WhereDelegate<SessionColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Session>();
			var results = new SessionCollection(database, database.GetQuery<SessionColumns, Session>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SessionCollection Where(WhereDelegate<SessionColumns> where, OrderBy<SessionColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Session>();
			var results = new SessionCollection(database, database.GetQuery<SessionColumns, Session>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;SessionColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SessionCollection Where(QiQuery where, Database database = null)
		{
			var results = new SessionCollection(database, Select<SessionColumns>.From<Session>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Session GetOneWhere(QueryFilter where, Database database = null)
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
		public static Session OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<SessionColumns> whereDelegate = (c) => where;
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
		public static Session GetOneWhere(WhereDelegate<SessionColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				SessionColumns c = new SessionColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Session instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Session OneWhere(WhereDelegate<SessionColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SessionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Session OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Session FirstOneWhere(WhereDelegate<SessionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Session FirstOneWhere(WhereDelegate<SessionColumns> where, OrderBy<SessionColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Session FirstOneWhere(QueryFilter where, OrderBy<SessionColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<SessionColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a SessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static SessionCollection Top(int count, WhereDelegate<SessionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static SessionCollection Top(int count, WhereDelegate<SessionColumns> where, OrderBy<SessionColumns> orderBy, Database database = null)
		{
			SessionColumns c = new SessionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Session>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Session>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SessionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SessionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static SessionCollection Top(int count, QueryFilter where, Database database)
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
		public static SessionCollection Top(int count, QueryFilter where, OrderBy<SessionColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Session>();
			QuerySet query = GetQuerySet(db);
			query.Top<Session>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<SessionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SessionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static SessionCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Session>();
			QuerySet query = GetQuerySet(db);
			query.Top<Session>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<SessionCollection>(0);
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
		public static SessionCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Session>();
			QuerySet query = GetQuerySet(db);
			query.Top<Session>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<SessionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Sessions
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Session>();
            QuerySet query = GetQuerySet(db);
            query.Count<Session>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<SessionColumns> where, Database database = null)
		{
			SessionColumns c = new SessionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Session>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Session>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Session>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Session>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Session CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Session>();			
			var dao = new Session();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Session OneOrThrow(SessionCollection c)
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
