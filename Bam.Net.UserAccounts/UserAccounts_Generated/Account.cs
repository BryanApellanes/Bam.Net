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
	[Bam.Net.Data.Table("Account", "UserAccounts")]
	public partial class Account: Bam.Net.Data.Dao
	{
		public Account():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Account(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Account(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Account(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Account(DataRow data)
		{
			return new Account(data);
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

	// property:Provider, columnName:Provider	
	[Bam.Net.Data.Column(Name="Provider", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Provider
	{
		get
		{
			return GetStringValue("Provider");
		}
		set
		{
			SetValue("Provider", value);
		}
	}

	// property:ProviderUserId, columnName:ProviderUserId	
	[Bam.Net.Data.Column(Name="ProviderUserId", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string ProviderUserId
	{
		get
		{
			return GetStringValue("ProviderUserId");
		}
		set
		{
			SetValue("ProviderUserId", value);
		}
	}

	// property:Comment, columnName:Comment	
	[Bam.Net.Data.Column(Name="Comment", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Comment
	{
		get
		{
			return GetStringValue("Comment");
		}
		set
		{
			SetValue("Comment", value);
		}
	}

	// property:CreationDate, columnName:CreationDate	
	[Bam.Net.Data.Column(Name="CreationDate", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
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

	// property:IsConfirmed, columnName:IsConfirmed	
	[Bam.Net.Data.Column(Name="IsConfirmed", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsConfirmed
	{
		get
		{
			return GetBooleanValue("IsConfirmed");
		}
		set
		{
			SetValue("IsConfirmed", value);
		}
	}

	// property:ConfirmationDate, columnName:ConfirmationDate	
	[Bam.Net.Data.Column(Name="ConfirmationDate", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? ConfirmationDate
	{
		get
		{
			return GetDateTimeValue("ConfirmationDate");
		}
		set
		{
			SetValue("ConfirmationDate", value);
		}
	}



	// start UserId -> UserId
	[Bam.Net.Data.ForeignKey(
        Table="Account",
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
				var colFilter = new AccountColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Account table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AccountCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Account>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Account>();
			var results = new AccountCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Account>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AccountColumns columns = new AccountColumns();
				var orderBy = Bam.Net.Data.Order.By<AccountColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Account>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<AccountColumns> where, Action<IEnumerable<Account>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AccountColumns columns = new AccountColumns();
				var orderBy = Bam.Net.Data.Order.By<AccountColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (AccountColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Account>> batchProcessor, Bam.Net.Data.OrderBy<AccountColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<AccountColumns> where, Action<IEnumerable<Account>> batchProcessor, Bam.Net.Data.OrderBy<AccountColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AccountColumns columns = new AccountColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (AccountColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Account GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Account GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Account GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Account GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Account GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Account GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static AccountCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static AccountCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<AccountColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AccountColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AccountCollection Where(Func<AccountColumns, QueryFilter<AccountColumns>> where, OrderBy<AccountColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Account>();
			return new AccountCollection(database.GetQuery<AccountColumns, Account>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AccountCollection Where(WhereDelegate<AccountColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Account>();
			var results = new AccountCollection(database, database.GetQuery<AccountColumns, Account>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AccountCollection Where(WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Account>();
			var results = new AccountCollection(database, database.GetQuery<AccountColumns, Account>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;AccountColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AccountCollection Where(QiQuery where, Database database = null)
		{
			var results = new AccountCollection(database, Select<AccountColumns>.From<Account>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Account GetOneWhere(QueryFilter where, Database database = null)
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
		public static Account OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<AccountColumns> whereDelegate = (c) => where;
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
		public static Account GetOneWhere(WhereDelegate<AccountColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				AccountColumns c = new AccountColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Account instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Account OneWhere(WhereDelegate<AccountColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AccountColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Account OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Account FirstOneWhere(WhereDelegate<AccountColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Account FirstOneWhere(WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Account FirstOneWhere(QueryFilter where, OrderBy<AccountColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<AccountColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AccountCollection Top(int count, WhereDelegate<AccountColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static AccountCollection Top(int count, WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy, Database database = null)
		{
			AccountColumns c = new AccountColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Account>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Account>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AccountColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AccountCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AccountCollection Top(int count, QueryFilter where, Database database)
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
		public static AccountCollection Top(int count, QueryFilter where, OrderBy<AccountColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Account>();
			QuerySet query = GetQuerySet(db);
			query.Top<Account>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<AccountColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AccountCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AccountCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Account>();
			QuerySet query = GetQuerySet(db);
			query.Top<Account>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<AccountCollection>(0);
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
		public static AccountCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Account>();
			QuerySet query = GetQuerySet(db);
			query.Top<Account>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<AccountCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Accounts
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Account>();
            QuerySet query = GetQuerySet(db);
            query.Count<Account>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<AccountColumns> where, Database database = null)
		{
			AccountColumns c = new AccountColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Account>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Account>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Account>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Account>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Account CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Account>();			
			var dao = new Account();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Account OneOrThrow(AccountCollection c)
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
