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
	[Bam.Net.Data.Table("GroupPermission", "UserAccounts")]
	public partial class GroupPermission: Bam.Net.Data.Dao
	{
		public GroupPermission():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public GroupPermission(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public GroupPermission(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public GroupPermission(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator GroupPermission(DataRow data)
		{
			return new GroupPermission(data);
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



	// start GroupId -> GroupId
	[Bam.Net.Data.ForeignKey(
        Table="GroupPermission",
		Name="GroupId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Group",
		Suffix="1")]
	public ulong? GroupId
	{
		get
		{
			return GetULongValue("GroupId");
		}
		set
		{
			SetValue("GroupId", value);
		}
	}

	Group _groupOfGroupId;
	public Group GroupOfGroupId
	{
		get
		{
			if(_groupOfGroupId == null)
			{
				_groupOfGroupId = Bam.Net.UserAccounts.Data.Group.OneWhere(c => c.KeyColumn == this.GroupId, this.Database);
			}
			return _groupOfGroupId;
		}
	}
	
	// start PermissionId -> PermissionId
	[Bam.Net.Data.ForeignKey(
        Table="GroupPermission",
		Name="PermissionId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Permission",
		Suffix="2")]
	public ulong? PermissionId
	{
		get
		{
			return GetULongValue("PermissionId");
		}
		set
		{
			SetValue("PermissionId", value);
		}
	}

	Permission _permissionOfPermissionId;
	public Permission PermissionOfPermissionId
	{
		get
		{
			if(_permissionOfPermissionId == null)
			{
				_permissionOfPermissionId = Bam.Net.UserAccounts.Data.Permission.OneWhere(c => c.KeyColumn == this.PermissionId, this.Database);
			}
			return _permissionOfPermissionId;
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
				var colFilter = new GroupPermissionColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the GroupPermission table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static GroupPermissionCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<GroupPermission>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<GroupPermission>();
			var results = new GroupPermissionCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<GroupPermission>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				GroupPermissionColumns columns = new GroupPermissionColumns();
				var orderBy = Bam.Net.Data.Order.By<GroupPermissionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<GroupPermission>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<GroupPermissionColumns> where, Action<IEnumerable<GroupPermission>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				GroupPermissionColumns columns = new GroupPermissionColumns();
				var orderBy = Bam.Net.Data.Order.By<GroupPermissionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (GroupPermissionColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<GroupPermission>> batchProcessor, Bam.Net.Data.OrderBy<GroupPermissionColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<GroupPermissionColumns> where, Action<IEnumerable<GroupPermission>> batchProcessor, Bam.Net.Data.OrderBy<GroupPermissionColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				GroupPermissionColumns columns = new GroupPermissionColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (GroupPermissionColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static GroupPermission GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static GroupPermission GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static GroupPermission GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static GroupPermission GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static GroupPermission GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static GroupPermission GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static GroupPermissionCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static GroupPermissionCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<GroupPermissionColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a GroupPermissionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static GroupPermissionCollection Where(Func<GroupPermissionColumns, QueryFilter<GroupPermissionColumns>> where, OrderBy<GroupPermissionColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<GroupPermission>();
			return new GroupPermissionCollection(database.GetQuery<GroupPermissionColumns, GroupPermission>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a GroupPermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static GroupPermissionCollection Where(WhereDelegate<GroupPermissionColumns> where, Database database = null)
		{		
			database = database ?? Db.For<GroupPermission>();
			var results = new GroupPermissionCollection(database, database.GetQuery<GroupPermissionColumns, GroupPermission>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a GroupPermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static GroupPermissionCollection Where(WhereDelegate<GroupPermissionColumns> where, OrderBy<GroupPermissionColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<GroupPermission>();
			var results = new GroupPermissionCollection(database, database.GetQuery<GroupPermissionColumns, GroupPermission>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;GroupPermissionColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static GroupPermissionCollection Where(QiQuery where, Database database = null)
		{
			var results = new GroupPermissionCollection(database, Select<GroupPermissionColumns>.From<GroupPermission>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static GroupPermission GetOneWhere(QueryFilter where, Database database = null)
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
		public static GroupPermission OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<GroupPermissionColumns> whereDelegate = (c) => where;
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
		public static GroupPermission GetOneWhere(WhereDelegate<GroupPermissionColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				GroupPermissionColumns c = new GroupPermissionColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single GroupPermission instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a GroupPermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static GroupPermission OneWhere(WhereDelegate<GroupPermissionColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<GroupPermissionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static GroupPermission OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a GroupPermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static GroupPermission FirstOneWhere(WhereDelegate<GroupPermissionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a GroupPermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static GroupPermission FirstOneWhere(WhereDelegate<GroupPermissionColumns> where, OrderBy<GroupPermissionColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a GroupPermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static GroupPermission FirstOneWhere(QueryFilter where, OrderBy<GroupPermissionColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<GroupPermissionColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a GroupPermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static GroupPermissionCollection Top(int count, WhereDelegate<GroupPermissionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a GroupPermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static GroupPermissionCollection Top(int count, WhereDelegate<GroupPermissionColumns> where, OrderBy<GroupPermissionColumns> orderBy, Database database = null)
		{
			GroupPermissionColumns c = new GroupPermissionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<GroupPermission>();
			QuerySet query = GetQuerySet(db); 
			query.Top<GroupPermission>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<GroupPermissionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<GroupPermissionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static GroupPermissionCollection Top(int count, QueryFilter where, Database database)
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
		public static GroupPermissionCollection Top(int count, QueryFilter where, OrderBy<GroupPermissionColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<GroupPermission>();
			QuerySet query = GetQuerySet(db);
			query.Top<GroupPermission>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<GroupPermissionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<GroupPermissionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static GroupPermissionCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<GroupPermission>();
			QuerySet query = GetQuerySet(db);
			query.Top<GroupPermission>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<GroupPermissionCollection>(0);
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
		public static GroupPermissionCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<GroupPermission>();
			QuerySet query = GetQuerySet(db);
			query.Top<GroupPermission>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<GroupPermissionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of GroupPermissions
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<GroupPermission>();
            QuerySet query = GetQuerySet(db);
            query.Count<GroupPermission>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a GroupPermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between GroupPermissionColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<GroupPermissionColumns> where, Database database = null)
		{
			GroupPermissionColumns c = new GroupPermissionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<GroupPermission>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<GroupPermission>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<GroupPermission>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<GroupPermission>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static GroupPermission CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<GroupPermission>();			
			var dao = new GroupPermission();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static GroupPermission OneOrThrow(GroupPermissionCollection c)
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
