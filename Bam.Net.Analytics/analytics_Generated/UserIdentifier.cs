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
	[Bam.Net.Data.Table("UserIdentifier", "Analytics")]
	public partial class UserIdentifier: Dao
	{
		public UserIdentifier():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UserIdentifier(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UserIdentifier(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UserIdentifier(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator UserIdentifier(DataRow data)
		{
			return new UserIdentifier(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("ClickCounter_UserIdentifierId", new ClickCounterCollection(Database.GetQuery<ClickCounterColumns, ClickCounter>((c) => c.UserIdentifierId == GetLongValue("Id")), this, "UserIdentifierId"));	
            this.ChildCollections.Add("LoginCounter_UserIdentifierId", new LoginCounterCollection(Database.GetQuery<LoginCounterColumns, LoginCounter>((c) => c.UserIdentifierId == GetLongValue("Id")), this, "UserIdentifierId"));							
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

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
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



				

	[Exclude]	
	public ClickCounterCollection ClickCountersByUserIdentifierId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ClickCounter_UserIdentifierId"))
			{
				SetChildren();
			}

			var c = (ClickCounterCollection)this.ChildCollections["ClickCounter_UserIdentifierId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public LoginCounterCollection LoginCountersByUserIdentifierId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("LoginCounter_UserIdentifierId"))
			{
				SetChildren();
			}

			var c = (LoginCounterCollection)this.ChildCollections["LoginCounter_UserIdentifierId"];
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
				var colFilter = new UserIdentifierColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the UserIdentifier table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static UserIdentifierCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<UserIdentifier>();
			Database db = database ?? Db.For<UserIdentifier>();
			var results = new UserIdentifierCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Action<IEnumerable<UserIdentifier>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				UserIdentifierColumns columns = new UserIdentifierColumns();
				var orderBy = Order.By<UserIdentifierColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<UserIdentifier>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<UserIdentifierColumns> where, Action<IEnumerable<UserIdentifier>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				UserIdentifierColumns columns = new UserIdentifierColumns();
				var orderBy = Order.By<UserIdentifierColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (UserIdentifierColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static UserIdentifier GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static UserIdentifier GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static UserIdentifier GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static UserIdentifier GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static UserIdentifierCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static UserIdentifierCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<UserIdentifierColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UserIdentifierColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserIdentifierCollection Where(Func<UserIdentifierColumns, QueryFilter<UserIdentifierColumns>> where, OrderBy<UserIdentifierColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<UserIdentifier>();
			return new UserIdentifierCollection(database.GetQuery<UserIdentifierColumns, UserIdentifier>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, Database database = null)
		{		
			database = database ?? Db.For<UserIdentifier>();
			var results = new UserIdentifierCollection(database, database.GetQuery<UserIdentifierColumns, UserIdentifier>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<UserIdentifier>();
			var results = new UserIdentifierCollection(database, database.GetQuery<UserIdentifierColumns, UserIdentifier>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;UserIdentifierColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static UserIdentifierCollection Where(QiQuery where, Database database = null)
		{
			var results = new UserIdentifierCollection(database, Select<UserIdentifierColumns>.From<UserIdentifier>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static UserIdentifier GetOneWhere(QueryFilter where, Database database = null)
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
		public static UserIdentifier OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<UserIdentifierColumns> whereDelegate = (c) => where;
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
		public static UserIdentifier GetOneWhere(WhereDelegate<UserIdentifierColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				UserIdentifierColumns c = new UserIdentifierColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UserIdentifier instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UserIdentifier OneWhere(WhereDelegate<UserIdentifierColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserIdentifierColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static UserIdentifier OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UserIdentifier FirstOneWhere(WhereDelegate<UserIdentifierColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UserIdentifier FirstOneWhere(WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UserIdentifier FirstOneWhere(QueryFilter where, OrderBy<UserIdentifierColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<UserIdentifierColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy, Database database = null)
		{
			UserIdentifierColumns c = new UserIdentifierColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<UserIdentifier>();
			QuerySet query = GetQuerySet(db); 
			query.Top<UserIdentifier>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserIdentifierColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UserIdentifierCollection>(0);
			results.Database = db;
			return results;
		}

		public static UserIdentifierCollection Top(int count, QueryFilter where, Database database)
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
		public static UserIdentifierCollection Top(int count, QueryFilter where, OrderBy<UserIdentifierColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<UserIdentifier>();
			QuerySet query = GetQuerySet(db);
			query.Top<UserIdentifier>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<UserIdentifierColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UserIdentifierCollection>(0);
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
		public static UserIdentifierCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<UserIdentifier>();
			QuerySet query = GetQuerySet(db);
			query.Top<UserIdentifier>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<UserIdentifierCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of UserIdentifiers
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<UserIdentifier>();
            QuerySet query = GetQuerySet(db);
            query.Count<UserIdentifier>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<UserIdentifierColumns> where, Database database = null)
		{
			UserIdentifierColumns c = new UserIdentifierColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<UserIdentifier>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserIdentifier>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static UserIdentifier CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<UserIdentifier>();			
			var dao = new UserIdentifier();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static UserIdentifier OneOrThrow(UserIdentifierCollection c)
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
