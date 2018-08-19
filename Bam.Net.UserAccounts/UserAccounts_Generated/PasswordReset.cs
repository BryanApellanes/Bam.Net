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
	[Bam.Net.Data.Table("PasswordReset", "UserAccounts")]
	public partial class PasswordReset: Bam.Net.Data.Dao
	{
		public PasswordReset():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public PasswordReset(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public PasswordReset(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public PasswordReset(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator PasswordReset(DataRow data)
		{
			return new PasswordReset(data);
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

	// property:Token, columnName:Token	
	[Bam.Net.Data.Column(Name="Token", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Token
	{
		get
		{
			return GetStringValue("Token");
		}
		set
		{
			SetValue("Token", value);
		}
	}

	// property:DateTime, columnName:DateTime	
	[Bam.Net.Data.Column(Name="DateTime", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime? DateTime
	{
		get
		{
			return GetDateTimeValue("DateTime");
		}
		set
		{
			SetValue("DateTime", value);
		}
	}

	// property:WasReset, columnName:WasReset	
	[Bam.Net.Data.Column(Name="WasReset", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? WasReset
	{
		get
		{
			return GetBooleanValue("WasReset");
		}
		set
		{
			SetValue("WasReset", value);
		}
	}

	// property:ExpiresInMinutes, columnName:ExpiresInMinutes	
	[Bam.Net.Data.Column(Name="ExpiresInMinutes", DbDataType="Int", MaxLength="10", AllowNull=false)]
	public int? ExpiresInMinutes
	{
		get
		{
			return GetIntValue("ExpiresInMinutes");
		}
		set
		{
			SetValue("ExpiresInMinutes", value);
		}
	}



	// start UserId -> UserId
	[Bam.Net.Data.ForeignKey(
        Table="PasswordReset",
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
				var colFilter = new PasswordResetColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the PasswordReset table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PasswordResetCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<PasswordReset>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<PasswordReset>();
			var results = new PasswordResetCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<PasswordReset>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				PasswordResetColumns columns = new PasswordResetColumns();
				var orderBy = Bam.Net.Data.Order.By<PasswordResetColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<PasswordReset>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<PasswordResetColumns> where, Action<IEnumerable<PasswordReset>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				PasswordResetColumns columns = new PasswordResetColumns();
				var orderBy = Bam.Net.Data.Order.By<PasswordResetColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (PasswordResetColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<PasswordReset>> batchProcessor, Bam.Net.Data.OrderBy<PasswordResetColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<PasswordResetColumns> where, Action<IEnumerable<PasswordReset>> batchProcessor, Bam.Net.Data.OrderBy<PasswordResetColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				PasswordResetColumns columns = new PasswordResetColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (PasswordResetColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static PasswordReset GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static PasswordReset GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static PasswordReset GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static PasswordReset GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static PasswordReset GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static PasswordReset GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static PasswordResetCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static PasswordResetCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<PasswordResetColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PasswordResetColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static PasswordResetCollection Where(Func<PasswordResetColumns, QueryFilter<PasswordResetColumns>> where, OrderBy<PasswordResetColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<PasswordReset>();
			return new PasswordResetCollection(database.GetQuery<PasswordResetColumns, PasswordReset>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static PasswordResetCollection Where(WhereDelegate<PasswordResetColumns> where, Database database = null)
		{		
			database = database ?? Db.For<PasswordReset>();
			var results = new PasswordResetCollection(database, database.GetQuery<PasswordResetColumns, PasswordReset>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PasswordResetCollection Where(WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<PasswordReset>();
			var results = new PasswordResetCollection(database, database.GetQuery<PasswordResetColumns, PasswordReset>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;PasswordResetColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static PasswordResetCollection Where(QiQuery where, Database database = null)
		{
			var results = new PasswordResetCollection(database, Select<PasswordResetColumns>.From<PasswordReset>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static PasswordReset GetOneWhere(QueryFilter where, Database database = null)
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
		public static PasswordReset OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<PasswordResetColumns> whereDelegate = (c) => where;
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
		public static PasswordReset GetOneWhere(WhereDelegate<PasswordResetColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				PasswordResetColumns c = new PasswordResetColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PasswordReset instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PasswordReset OneWhere(WhereDelegate<PasswordResetColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PasswordResetColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static PasswordReset OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PasswordReset FirstOneWhere(WhereDelegate<PasswordResetColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PasswordReset FirstOneWhere(WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PasswordReset FirstOneWhere(QueryFilter where, OrderBy<PasswordResetColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<PasswordResetColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PasswordResetCollection Top(int count, WhereDelegate<PasswordResetColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static PasswordResetCollection Top(int count, WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy, Database database = null)
		{
			PasswordResetColumns c = new PasswordResetColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<PasswordReset>();
			QuerySet query = GetQuerySet(db); 
			query.Top<PasswordReset>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PasswordResetColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PasswordResetCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static PasswordResetCollection Top(int count, QueryFilter where, Database database)
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
		public static PasswordResetCollection Top(int count, QueryFilter where, OrderBy<PasswordResetColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<PasswordReset>();
			QuerySet query = GetQuerySet(db);
			query.Top<PasswordReset>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<PasswordResetColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PasswordResetCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static PasswordResetCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<PasswordReset>();
			QuerySet query = GetQuerySet(db);
			query.Top<PasswordReset>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<PasswordResetCollection>(0);
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
		public static PasswordResetCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<PasswordReset>();
			QuerySet query = GetQuerySet(db);
			query.Top<PasswordReset>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<PasswordResetCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of PasswordResets
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<PasswordReset>();
            QuerySet query = GetQuerySet(db);
            query.Count<PasswordReset>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<PasswordResetColumns> where, Database database = null)
		{
			PasswordResetColumns c = new PasswordResetColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<PasswordReset>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<PasswordReset>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<PasswordReset>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<PasswordReset>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static PasswordReset CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<PasswordReset>();			
			var dao = new PasswordReset();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static PasswordReset OneOrThrow(PasswordResetCollection c)
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
