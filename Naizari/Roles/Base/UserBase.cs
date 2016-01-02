/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Data.SqlClient;
using Naizari.Data.Access;

namespace Naizari.Roles
{
	[DaoTable("User")]
	public class UserBase: DaoObject
	{
		public UserBase(): base()
		{
			this.idColumn = IdColumn;
			this.tableName = TableName;
			this.dataContextName = ContextName;
			propertyToColumnMap.Add("Id","id");
			columnSizes.Add("id", 4);
			propertyToColumnMap.Add("ApplicationId","application_id");
			columnSizes.Add("application_id", 4);
			propertyToColumnMap.Add("RoleId","role_id");
			columnSizes.Add("role_id", 4);
			propertyToColumnMap.Add("UserId","user_id");
			columnSizes.Add("user_id", 20);
			propertyToColumnMap.Add("Domain","domain");
			columnSizes.Add("domain", 20);
			propertyToColumnMap.Add("FirstName","first_name");
			columnSizes.Add("first_name", 20);
			propertyToColumnMap.Add("LastName","last_name");
			columnSizes.Add("last_name", 20);
			primaryKeyColumns.Add("id");
		}

		public const string IdColumn = "id";
		public const string TableName = "User";
		public const string ContextName = "Roles";


		[DaoPrimaryKeyColumn("id", 4)]
		[DaoIdColumn("id", 4)]
		public int Id
		{
			get
			{
				return GetIntValue("id");
			}

			set
			{
				SetValue("id", value);
			}

		}

		[DaoColumn("application_id", 4)]
		public int ApplicationId
		{
			get
			{
				return GetIntValue("application_id");
			}

			set
			{
				SetValue("application_id", value);
			}

		}

		[DaoColumn("role_id", 4)]
		public int RoleId
		{
			get
			{
				return GetIntValue("role_id");
			}

			set
			{
				SetValue("role_id", value);
			}

		}

		[DaoColumn("user_id", 20)]
		public string UserId
		{
			get
			{
				return (string)GetValue("user_id");
			}

			set
			{
				SetValue("user_id", value);
			}

		}

		[DaoColumn("domain", 20)]
		public string Domain
		{
			get
			{
				return (string)GetValue("domain");
			}

			set
			{
				SetValue("domain", value);
			}

		}

		[DaoColumn("first_name", 20)]
		public string FirstName
		{
			get
			{
				return (string)GetValue("first_name");
			}

			set
			{
				SetValue("first_name", value);
			}

		}

		[DaoColumn("last_name", 20)]
		public string LastName
		{
			get
			{
				return (string)GetValue("last_name");
			}

			set
			{
				SetValue("last_name", value);
			}

		}

		Application _application;
		[DaoForeignKeyColumn("application_id", 4, "FK_User_Application", "application_id", "Application")]
		public Application Application
		{
			get
			{
				if( _application == null )
				{
					_application = Application.SelectById<Application>((int)this.GetValue("application_id"));
				}
				return _application;

			}

		}

		Role _role;
		[DaoForeignKeyColumn("role_id", 4, "FK_User_Role", "role_id", "Role")]
		public Role Role
		{
			get
			{
				if( _role == null )
				{
					_role = Role.SelectById<Role>((int)this.GetValue("role_id"));
				}
				return _role;

			}

		}

		public static User SelectById(int id)
		{
			return User.SelectById<User>(id);
		}

		public static User[] SelectListWhere(UserFields fieldName, object value)
		{
			return User.SelectListWhere<User>(fieldName.ToString(), value);
		}

		public static User[] SelectListWhere(params SqlSelectParameter[] sqlSelectParameters)
		{
			return User.SelectListWhere<User>(sqlSelectParameters);
		}

		public static User[] Select()
		{
			return User.Select<User>();
		}

		public static User[] Select(string whereClause, params SqlParameter[] parameters)
		{
			return User.Select<User>(whereClause, parameters);
		}

		public static User SelectTop(OrderBy orderBy)
		{
			return User.SelectTop<User>(orderBy);
		}

		public static User[] SelectTop(OrderBy orderBy, int count)
		{
			return User.SelectTop<User>(orderBy, count);
		}

		public static User New()
		{
			return User.New<User>();
		}

		public UpdateResult Update(bool useTransaction)
		{
			return useTransaction ? this.TransactionUpdate<User>(): this.Update();
		}

		public UpdateResult Update(params SqlSelectParameter[] filters)
		{
			return filters.Length == 0 ? base.Update(): this.Update<User>(filters);
		}

		public override UpdateResult UpdateByPrimaryKey()
		{
			return this.UpdateByPrimaryKey<User>();
		}

		public override UpdateResult UpdateByValues()
		{
			return this.UpdateByValues<User>();
		}

		public static User[] SelectAll()
		{
			return User.SelectAll<User>();
		}

		public bool RecordIsAltered()
		{
			return this.RecordIsAltered<User>();
		}

	}
}

