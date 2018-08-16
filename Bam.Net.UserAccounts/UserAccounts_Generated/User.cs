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
	[Bam.Net.Data.Table("User", "UserAccounts")]
	public partial class User: Bam.Net.Data.Dao
	{
		public User():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public User(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public User(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public User(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator User(DataRow data)
		{
			return new User(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("Account_UserId", new AccountCollection(Database.GetQuery<AccountColumns, Account>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("Password_UserId", new PasswordCollection(Database.GetQuery<PasswordColumns, Password>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("PasswordReset_UserId", new PasswordResetCollection(Database.GetQuery<PasswordResetColumns, PasswordReset>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("PasswordFailure_UserId", new PasswordFailureCollection(Database.GetQuery<PasswordFailureColumns, PasswordFailure>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("LockOut_UserId", new LockOutCollection(Database.GetQuery<LockOutColumns, LockOut>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("Login_UserId", new LoginCollection(Database.GetQuery<LoginColumns, Login>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("PasswordQuestion_UserId", new PasswordQuestionCollection(Database.GetQuery<PasswordQuestionColumns, PasswordQuestion>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("Setting_UserId", new SettingCollection(Database.GetQuery<SettingColumns, Setting>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("Session_UserId", new SessionCollection(Database.GetQuery<SessionColumns, Session>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("UserRole_UserId", new UserRoleCollection(Database.GetQuery<UserRoleColumns, UserRole>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("UserGroup_UserId", new UserGroupCollection(Database.GetQuery<UserGroupColumns, UserGroup>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("UserPermission_UserId", new UserPermissionCollection(Database.GetQuery<UserPermissionColumns, UserPermission>((c) => c.UserId == GetULongValue("Id")), this, "UserId"));				
			}			
            this.ChildCollections.Add("User_UserRole_Role",  new XrefDaoCollection<UserRole, Role>(this, false));
				
            this.ChildCollections.Add("User_UserGroup_Group",  new XrefDaoCollection<UserGroup, Group>(this, false));
				
            this.ChildCollections.Add("User_UserPermission_Permission",  new XrefDaoCollection<UserPermission, Permission>(this, false));
							
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

	// property:IsDeleted, columnName:IsDeleted	
	[Bam.Net.Data.Column(Name="IsDeleted", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsDeleted
	{
		get
		{
			return GetBooleanValue("IsDeleted");
		}
		set
		{
			SetValue("IsDeleted", value);
		}
	}

	// property:UserName, columnName:UserName	
	[Bam.Net.Data.Column(Name="UserName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string UserName
	{
		get
		{
			return GetStringValue("UserName");
		}
		set
		{
			SetValue("UserName", value);
		}
	}

	// property:IsApproved, columnName:IsApproved	
	[Bam.Net.Data.Column(Name="IsApproved", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsApproved
	{
		get
		{
			return GetBooleanValue("IsApproved");
		}
		set
		{
			SetValue("IsApproved", value);
		}
	}

	// property:Email, columnName:Email	
	[Bam.Net.Data.Column(Name="Email", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Email
	{
		get
		{
			return GetStringValue("Email");
		}
		set
		{
			SetValue("Email", value);
		}
	}



				

	[Bam.Net.Exclude]	
	public AccountCollection AccountsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Account_UserId"))
			{
				SetChildren();
			}

			var c = (AccountCollection)this.ChildCollections["Account_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public PasswordCollection PasswordsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Password_UserId"))
			{
				SetChildren();
			}

			var c = (PasswordCollection)this.ChildCollections["Password_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public PasswordResetCollection PasswordResetsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("PasswordReset_UserId"))
			{
				SetChildren();
			}

			var c = (PasswordResetCollection)this.ChildCollections["PasswordReset_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public PasswordFailureCollection PasswordFailuresByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("PasswordFailure_UserId"))
			{
				SetChildren();
			}

			var c = (PasswordFailureCollection)this.ChildCollections["PasswordFailure_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public LockOutCollection LockOutsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("LockOut_UserId"))
			{
				SetChildren();
			}

			var c = (LockOutCollection)this.ChildCollections["LockOut_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public LoginCollection LoginsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Login_UserId"))
			{
				SetChildren();
			}

			var c = (LoginCollection)this.ChildCollections["Login_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public PasswordQuestionCollection PasswordQuestionsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("PasswordQuestion_UserId"))
			{
				SetChildren();
			}

			var c = (PasswordQuestionCollection)this.ChildCollections["PasswordQuestion_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public SettingCollection SettingsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Setting_UserId"))
			{
				SetChildren();
			}

			var c = (SettingCollection)this.ChildCollections["Setting_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public SessionCollection SessionsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Session_UserId"))
			{
				SetChildren();
			}

			var c = (SessionCollection)this.ChildCollections["Session_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public UserRoleCollection UserRolesByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("UserRole_UserId"))
			{
				SetChildren();
			}

			var c = (UserRoleCollection)this.ChildCollections["UserRole_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public UserGroupCollection UserGroupsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("UserGroup_UserId"))
			{
				SetChildren();
			}

			var c = (UserGroupCollection)this.ChildCollections["UserGroup_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public UserPermissionCollection UserPermissionsByUserId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("UserPermission_UserId"))
			{
				SetChildren();
			}

			var c = (UserPermissionCollection)this.ChildCollections["UserPermission_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<UserRole, Role> Roles
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("User_UserRole_Role"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<UserRole, Role>)this.ChildCollections["User_UserRole_Role"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<UserGroup, Group> Groups
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("User_UserGroup_Group"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<UserGroup, Group>)this.ChildCollections["User_UserGroup_Group"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<UserPermission, Permission> Permissions
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("User_UserPermission_Permission"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<UserPermission, Permission>)this.ChildCollections["User_UserPermission_Permission"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
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
				var colFilter = new UserColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the User table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static UserCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<User>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<User>();
			var results = new UserCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<User>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				UserColumns columns = new UserColumns();
				var orderBy = Bam.Net.Data.Order.By<UserColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<User>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<UserColumns> where, Action<IEnumerable<User>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				UserColumns columns = new UserColumns();
				var orderBy = Bam.Net.Data.Order.By<UserColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (UserColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<User>> batchProcessor, Bam.Net.Data.OrderBy<UserColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<UserColumns> where, Action<IEnumerable<User>> batchProcessor, Bam.Net.Data.OrderBy<UserColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				UserColumns columns = new UserColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (UserColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static User GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static User GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static User GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static User GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static User GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static User GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static UserCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static UserCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<UserColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UserColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static UserCollection Where(Func<UserColumns, QueryFilter<UserColumns>> where, OrderBy<UserColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<User>();
			return new UserCollection(database.GetQuery<UserColumns, User>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static UserCollection Where(WhereDelegate<UserColumns> where, Database database = null)
		{		
			database = database ?? Db.For<User>();
			var results = new UserCollection(database, database.GetQuery<UserColumns, User>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UserCollection Where(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<User>();
			var results = new UserCollection(database, database.GetQuery<UserColumns, User>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;UserColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static UserCollection Where(QiQuery where, Database database = null)
		{
			var results = new UserCollection(database, Select<UserColumns>.From<User>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static User GetOneWhere(QueryFilter where, Database database = null)
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
		public static User OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<UserColumns> whereDelegate = (c) => where;
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
		public static User GetOneWhere(WhereDelegate<UserColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				UserColumns c = new UserColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single User instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static User OneWhere(WhereDelegate<UserColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static User OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static User FirstOneWhere(WhereDelegate<UserColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static User FirstOneWhere(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static User FirstOneWhere(QueryFilter where, OrderBy<UserColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<UserColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static UserCollection Top(int count, WhereDelegate<UserColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static UserCollection Top(int count, WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy, Database database = null)
		{
			UserColumns c = new UserColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<User>();
			QuerySet query = GetQuerySet(db); 
			query.Top<User>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UserCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static UserCollection Top(int count, QueryFilter where, Database database)
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
		public static UserCollection Top(int count, QueryFilter where, OrderBy<UserColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<User>();
			QuerySet query = GetQuerySet(db);
			query.Top<User>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<UserColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UserCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static UserCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<User>();
			QuerySet query = GetQuerySet(db);
			query.Top<User>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<UserCollection>(0);
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
		public static UserCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<User>();
			QuerySet query = GetQuerySet(db);
			query.Top<User>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<UserCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Users
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<User>();
            QuerySet query = GetQuerySet(db);
            query.Count<User>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<UserColumns> where, Database database = null)
		{
			UserColumns c = new UserColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<User>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<User>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<User>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<User>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static User CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<User>();			
			var dao = new User();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static User OneOrThrow(UserCollection c)
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
