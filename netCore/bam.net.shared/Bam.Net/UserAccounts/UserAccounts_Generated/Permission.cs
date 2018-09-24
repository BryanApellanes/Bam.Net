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
	[Bam.Net.Data.Table("Permission", "UserAccounts")]
	public partial class Permission: Bam.Net.Data.Dao
	{
		public Permission():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Permission(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Permission(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Permission(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Permission(DataRow data)
		{
			return new Permission(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("GroupPermission_PermissionId", new GroupPermissionCollection(Database.GetQuery<GroupPermissionColumns, GroupPermission>((c) => c.PermissionId == GetULongValue("Id")), this, "PermissionId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("UserPermission_PermissionId", new UserPermissionCollection(Database.GetQuery<UserPermissionColumns, UserPermission>((c) => c.PermissionId == GetULongValue("Id")), this, "PermissionId"));				
			}						
            this.ChildCollections.Add("Permission_GroupPermission_Group",  new XrefDaoCollection<GroupPermission, Group>(this, false));
				
            this.ChildCollections.Add("Permission_UserPermission_User",  new XrefDaoCollection<UserPermission, User>(this, false));
				
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



	// start TreeNodeId -> TreeNodeId
	[Bam.Net.Data.ForeignKey(
        Table="Permission",
		Name="TreeNodeId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="TreeNode",
		Suffix="1")]
	public ulong? TreeNodeId
	{
		get
		{
			return GetULongValue("TreeNodeId");
		}
		set
		{
			SetValue("TreeNodeId", value);
		}
	}

	TreeNode _treeNodeOfTreeNodeId;
	public TreeNode TreeNodeOfTreeNodeId
	{
		get
		{
			if(_treeNodeOfTreeNodeId == null)
			{
				_treeNodeOfTreeNodeId = Bam.Net.UserAccounts.Data.TreeNode.OneWhere(c => c.KeyColumn == this.TreeNodeId, this.Database);
			}
			return _treeNodeOfTreeNodeId;
		}
	}
	
				

	[Bam.Net.Exclude]	
	public GroupPermissionCollection GroupPermissionsByPermissionId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("GroupPermission_PermissionId"))
			{
				SetChildren();
			}

			var c = (GroupPermissionCollection)this.ChildCollections["GroupPermission_PermissionId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public UserPermissionCollection UserPermissionsByPermissionId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("UserPermission_PermissionId"))
			{
				SetChildren();
			}

			var c = (UserPermissionCollection)this.ChildCollections["UserPermission_PermissionId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<GroupPermission, Group> Groups
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Permission_GroupPermission_Group"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<GroupPermission, Group>)this.ChildCollections["Permission_GroupPermission_Group"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<UserPermission, User> Users
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Permission_UserPermission_User"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<UserPermission, User>)this.ChildCollections["Permission_UserPermission_User"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }		/// <summary>
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
				var colFilter = new PermissionColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Permission table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PermissionCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Permission>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Permission>();
			var results = new PermissionCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Permission>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				PermissionColumns columns = new PermissionColumns();
				var orderBy = Bam.Net.Data.Order.By<PermissionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Permission>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<PermissionColumns> where, Action<IEnumerable<Permission>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				PermissionColumns columns = new PermissionColumns();
				var orderBy = Bam.Net.Data.Order.By<PermissionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (PermissionColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Permission>> batchProcessor, Bam.Net.Data.OrderBy<PermissionColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<PermissionColumns> where, Action<IEnumerable<Permission>> batchProcessor, Bam.Net.Data.OrderBy<PermissionColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				PermissionColumns columns = new PermissionColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (PermissionColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Permission GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Permission GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Permission GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Permission GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Permission GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Permission GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static PermissionCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static PermissionCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<PermissionColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PermissionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static PermissionCollection Where(Func<PermissionColumns, QueryFilter<PermissionColumns>> where, OrderBy<PermissionColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Permission>();
			return new PermissionCollection(database.GetQuery<PermissionColumns, Permission>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static PermissionCollection Where(WhereDelegate<PermissionColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Permission>();
			var results = new PermissionCollection(database, database.GetQuery<PermissionColumns, Permission>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PermissionCollection Where(WhereDelegate<PermissionColumns> where, OrderBy<PermissionColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Permission>();
			var results = new PermissionCollection(database, database.GetQuery<PermissionColumns, Permission>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;PermissionColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static PermissionCollection Where(QiQuery where, Database database = null)
		{
			var results = new PermissionCollection(database, Select<PermissionColumns>.From<Permission>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Permission GetOneWhere(QueryFilter where, Database database = null)
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
		public static Permission OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<PermissionColumns> whereDelegate = (c) => where;
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
		public static Permission GetOneWhere(WhereDelegate<PermissionColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				PermissionColumns c = new PermissionColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Permission instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Permission OneWhere(WhereDelegate<PermissionColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PermissionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Permission OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Permission FirstOneWhere(WhereDelegate<PermissionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Permission FirstOneWhere(WhereDelegate<PermissionColumns> where, OrderBy<PermissionColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Permission FirstOneWhere(QueryFilter where, OrderBy<PermissionColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<PermissionColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a PermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PermissionCollection Top(int count, WhereDelegate<PermissionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static PermissionCollection Top(int count, WhereDelegate<PermissionColumns> where, OrderBy<PermissionColumns> orderBy, Database database = null)
		{
			PermissionColumns c = new PermissionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Permission>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Permission>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PermissionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PermissionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static PermissionCollection Top(int count, QueryFilter where, Database database)
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
		public static PermissionCollection Top(int count, QueryFilter where, OrderBy<PermissionColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Permission>();
			QuerySet query = GetQuerySet(db);
			query.Top<Permission>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<PermissionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PermissionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static PermissionCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Permission>();
			QuerySet query = GetQuerySet(db);
			query.Top<Permission>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<PermissionCollection>(0);
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
		public static PermissionCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Permission>();
			QuerySet query = GetQuerySet(db);
			query.Top<Permission>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<PermissionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Permissions
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Permission>();
            QuerySet query = GetQuerySet(db);
            query.Count<Permission>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PermissionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PermissionColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<PermissionColumns> where, Database database = null)
		{
			PermissionColumns c = new PermissionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Permission>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Permission>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Permission>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Permission>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Permission CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Permission>();			
			var dao = new Permission();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Permission OneOrThrow(PermissionCollection c)
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
