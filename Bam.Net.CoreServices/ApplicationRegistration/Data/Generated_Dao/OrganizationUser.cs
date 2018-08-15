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

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
	// schema = ApplicationRegistration
	// connection Name = ApplicationRegistration
	[Serializable]
	[Bam.Net.Data.Table("OrganizationUser", "ApplicationRegistration")]
	public partial class OrganizationUser: Bam.Net.Data.Dao
	{
		public OrganizationUser():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public OrganizationUser(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public OrganizationUser(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public OrganizationUser(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator OrganizationUser(DataRow data)
		{
			return new OrganizationUser(data);
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



	// start OrganizationId -> OrganizationId
	[Bam.Net.Data.ForeignKey(
        Table="OrganizationUser",
		Name="OrganizationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Organization",
		Suffix="1")]
	public ulong? OrganizationId
	{
		get
		{
			return GetULongValue("OrganizationId");
		}
		set
		{
			SetValue("OrganizationId", value);
		}
	}

	Organization _organizationOfOrganizationId;
	public Organization OrganizationOfOrganizationId
	{
		get
		{
			if(_organizationOfOrganizationId == null)
			{
				_organizationOfOrganizationId = Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Organization.OneWhere(c => c.KeyColumn == this.OrganizationId, this.Database);
			}
			return _organizationOfOrganizationId;
		}
	}
	
	// start UserId -> UserId
	[Bam.Net.Data.ForeignKey(
        Table="OrganizationUser",
		Name="UserId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="2")]
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
				_userOfUserId = Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.User.OneWhere(c => c.KeyColumn == this.UserId, this.Database);
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
				var colFilter = new OrganizationUserColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the OrganizationUser table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static OrganizationUserCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<OrganizationUser>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<OrganizationUser>();
			var results = new OrganizationUserCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<OrganizationUser>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				OrganizationUserColumns columns = new OrganizationUserColumns();
				var orderBy = Bam.Net.Data.Order.By<OrganizationUserColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<OrganizationUser>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<OrganizationUserColumns> where, Action<IEnumerable<OrganizationUser>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				OrganizationUserColumns columns = new OrganizationUserColumns();
				var orderBy = Bam.Net.Data.Order.By<OrganizationUserColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (OrganizationUserColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<OrganizationUser>> batchProcessor, Bam.Net.Data.OrderBy<OrganizationUserColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<OrganizationUserColumns> where, Action<IEnumerable<OrganizationUser>> batchProcessor, Bam.Net.Data.OrderBy<OrganizationUserColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				OrganizationUserColumns columns = new OrganizationUserColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (OrganizationUserColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static OrganizationUser GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static OrganizationUser GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static OrganizationUser GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static OrganizationUser GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static OrganizationUser GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static OrganizationUser GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static OrganizationUserCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static OrganizationUserCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<OrganizationUserColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a OrganizationUserColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static OrganizationUserCollection Where(Func<OrganizationUserColumns, QueryFilter<OrganizationUserColumns>> where, OrderBy<OrganizationUserColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<OrganizationUser>();
			return new OrganizationUserCollection(database.GetQuery<OrganizationUserColumns, OrganizationUser>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationUserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static OrganizationUserCollection Where(WhereDelegate<OrganizationUserColumns> where, Database database = null)
		{		
			database = database ?? Db.For<OrganizationUser>();
			var results = new OrganizationUserCollection(database, database.GetQuery<OrganizationUserColumns, OrganizationUser>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationUserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OrganizationUserCollection Where(WhereDelegate<OrganizationUserColumns> where, OrderBy<OrganizationUserColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<OrganizationUser>();
			var results = new OrganizationUserCollection(database, database.GetQuery<OrganizationUserColumns, OrganizationUser>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;OrganizationUserColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static OrganizationUserCollection Where(QiQuery where, Database database = null)
		{
			var results = new OrganizationUserCollection(database, Select<OrganizationUserColumns>.From<OrganizationUser>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static OrganizationUser GetOneWhere(QueryFilter where, Database database = null)
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
		public static OrganizationUser OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<OrganizationUserColumns> whereDelegate = (c) => where;
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
		public static OrganizationUser GetOneWhere(WhereDelegate<OrganizationUserColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				OrganizationUserColumns c = new OrganizationUserColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single OrganizationUser instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationUserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OrganizationUser OneWhere(WhereDelegate<OrganizationUserColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<OrganizationUserColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static OrganizationUser OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationUserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OrganizationUser FirstOneWhere(WhereDelegate<OrganizationUserColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OrganizationUserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OrganizationUser FirstOneWhere(WhereDelegate<OrganizationUserColumns> where, OrderBy<OrganizationUserColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OrganizationUserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OrganizationUser FirstOneWhere(QueryFilter where, OrderBy<OrganizationUserColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<OrganizationUserColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a OrganizationUserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OrganizationUserCollection Top(int count, WhereDelegate<OrganizationUserColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OrganizationUserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static OrganizationUserCollection Top(int count, WhereDelegate<OrganizationUserColumns> where, OrderBy<OrganizationUserColumns> orderBy, Database database = null)
		{
			OrganizationUserColumns c = new OrganizationUserColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<OrganizationUser>();
			QuerySet query = GetQuerySet(db); 
			query.Top<OrganizationUser>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<OrganizationUserColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<OrganizationUserCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static OrganizationUserCollection Top(int count, QueryFilter where, Database database)
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
		public static OrganizationUserCollection Top(int count, QueryFilter where, OrderBy<OrganizationUserColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<OrganizationUser>();
			QuerySet query = GetQuerySet(db);
			query.Top<OrganizationUser>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<OrganizationUserColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<OrganizationUserCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static OrganizationUserCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<OrganizationUser>();
			QuerySet query = GetQuerySet(db);
			query.Top<OrganizationUser>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<OrganizationUserCollection>(0);
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
		public static OrganizationUserCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<OrganizationUser>();
			QuerySet query = GetQuerySet(db);
			query.Top<OrganizationUser>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<OrganizationUserCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of OrganizationUsers
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<OrganizationUser>();
            QuerySet query = GetQuerySet(db);
            query.Count<OrganizationUser>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationUserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationUserColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<OrganizationUserColumns> where, Database database = null)
		{
			OrganizationUserColumns c = new OrganizationUserColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<OrganizationUser>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<OrganizationUser>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<OrganizationUser>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<OrganizationUser>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static OrganizationUser CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<OrganizationUser>();			
			var dao = new OrganizationUser();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static OrganizationUser OneOrThrow(OrganizationUserCollection c)
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
