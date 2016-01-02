/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Data.SqlClient;
using Naizari.Data.Access;

namespace Naizari.Roles
{
	[DaoTable("Role")]
	public class RoleBase: DaoObject
	{
		public RoleBase(): base()
		{
			this.idColumn = IdColumn;
			this.tableName = TableName;
			this.dataContextName = ContextName;
			propertyToColumnMap.Add("RoleId","role_id");
			columnSizes.Add("role_id", 4);
			propertyToColumnMap.Add("ApplicationId","application_id");
			columnSizes.Add("application_id", 4);
			propertyToColumnMap.Add("RoleName","role_name");
			columnSizes.Add("role_name", -1);
			propertyToColumnMap.Add("Description","description");
			columnSizes.Add("description", -1);
			primaryKeyColumns.Add("role_id");
		}

		public const string IdColumn = "role_id";
		public const string TableName = "Role";
		public const string ContextName = "Roles";


		[DaoPrimaryKeyColumn("role_id", 4)]
		[DaoIdColumn("role_id", 4)]
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

		[DaoColumn("role_name", -1)]
		public string RoleName
		{
			get
			{
				return (string)GetValue("role_name");
			}

			set
			{
				SetValue("role_name", value);
			}

		}

		[DaoColumn("description", -1)]
		public string Description
		{
			get
			{
				return (string)GetValue("description");
			}

			set
			{
				SetValue("description", value);
			}

		}

		Application _application;
		[DaoForeignKeyColumn("application_id", 4, "FK_Role_Application", "application_id", "Application")]
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

		User[] _userlist;
		public User[] UserList
		{
			get
			{
				if( _userlist == null )
				{
					_userlist = User.SelectListWhere<User>("role_id", (int)this.GetValue("role_id"));
				}
				return _userlist;

			}

		}

		public static Role SelectById(int id)
		{
			return Role.SelectById<Role>(id);
		}

		public static Role[] SelectListWhere(RoleFields fieldName, object value)
		{
			return Role.SelectListWhere<Role>(fieldName.ToString(), value);
		}

		public static Role[] SelectListWhere(params SqlSelectParameter[] sqlSelectParameters)
		{
			return Role.SelectListWhere<Role>(sqlSelectParameters);
		}

		public static Role[] Select()
		{
			return Role.Select<Role>();
		}

		public static Role[] Select(string whereClause, params SqlParameter[] parameters)
		{
			return Role.Select<Role>(whereClause, parameters);
		}

		public static Role SelectTop(OrderBy orderBy)
		{
			return Role.SelectTop<Role>(orderBy);
		}

		public static Role[] SelectTop(OrderBy orderBy, int count)
		{
			return Role.SelectTop<Role>(orderBy, count);
		}

		public static Role New()
		{
			return Role.New<Role>();
		}

		public UpdateResult Update(bool useTransaction)
		{
			return useTransaction ? this.TransactionUpdate<Role>(): this.Update();
		}

		public UpdateResult Update(params SqlSelectParameter[] filters)
		{
			return filters.Length == 0 ? base.Update(): this.Update<Role>(filters);
		}

		public override UpdateResult UpdateByPrimaryKey()
		{
			return this.UpdateByPrimaryKey<Role>();
		}

		public override UpdateResult UpdateByValues()
		{
			return this.UpdateByValues<Role>();
		}

		public static Role[] SelectAll()
		{
			return Role.SelectAll<Role>();
		}

		public bool RecordIsAltered()
		{
			return this.RecordIsAltered<Role>();
		}

	}
}

