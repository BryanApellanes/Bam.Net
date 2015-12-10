/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Data.SqlClient;
using Naizari.Data.Access;

namespace Naizari.Roles
{
	[DaoTable("Application")]
	public class ApplicationBase: DaoObject
	{
		public ApplicationBase(): base()
		{
			this.idColumn = IdColumn;
			this.tableName = TableName;
			this.dataContextName = ContextName;
			propertyToColumnMap.Add("ApplicationId","application_id");
			columnSizes.Add("application_id", 4);
			propertyToColumnMap.Add("ApplicationBaseURL","applicationBaseURL");
			columnSizes.Add("applicationBaseURL", -1);
			propertyToColumnMap.Add("ApplicationName","applicationName");
			columnSizes.Add("applicationName", -1);
			propertyToColumnMap.Add("Description","description");
			columnSizes.Add("description", -1);
			primaryKeyColumns.Add("application_id");
		}

		public const string IdColumn = "application_id";
		public const string TableName = "Application";
		public const string ContextName = "Roles";


		[DaoPrimaryKeyColumn("application_id", 4)]
		[DaoIdColumn("application_id", 4)]
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

		[DaoColumn("applicationBaseURL", -1)]
		public string ApplicationBaseURL
		{
			get
			{
				return (string)GetValue("applicationBaseURL");
			}

			set
			{
				SetValue("applicationBaseURL", value);
			}

		}

		[DaoColumn("applicationName", -1)]
		public string ApplicationName
		{
			get
			{
				return (string)GetValue("applicationName");
			}

			set
			{
				SetValue("applicationName", value);
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

		Role[] _rolelist;
		public Role[] RoleList
		{
			get
			{
				if( _rolelist == null )
				{
					_rolelist = Role.SelectListWhere<Role>("application_id", (int)this.GetValue("application_id"));
				}
				return _rolelist;

			}

		}

		User[] _userlist;
		public User[] UserList
		{
			get
			{
				if( _userlist == null )
				{
					_userlist = User.SelectListWhere<User>("application_id", (int)this.GetValue("application_id"));
				}
				return _userlist;

			}

		}

		public static Application SelectById(int id)
		{
			return Application.SelectById<Application>(id);
		}

		public static Application[] SelectListWhere(ApplicationFields fieldName, object value)
		{
			return Application.SelectListWhere<Application>(fieldName.ToString(), value);
		}

		public static Application[] SelectListWhere(params SqlSelectParameter[] sqlSelectParameters)
		{
			return Application.SelectListWhere<Application>(sqlSelectParameters);
		}

		public static Application[] Select()
		{
			return Application.Select<Application>();
		}

		public static Application[] Select(string whereClause, params SqlParameter[] parameters)
		{
			return Application.Select<Application>(whereClause, parameters);
		}

		public static Application SelectTop(OrderBy orderBy)
		{
			return Application.SelectTop<Application>(orderBy);
		}

		public static Application[] SelectTop(OrderBy orderBy, int count)
		{
			return Application.SelectTop<Application>(orderBy, count);
		}

		public static Application New()
		{
			return Application.New<Application>();
		}

		public UpdateResult Update(bool useTransaction)
		{
			return useTransaction ? this.TransactionUpdate<Application>(): this.Update();
		}

		public UpdateResult Update(params SqlSelectParameter[] filters)
		{
			return filters.Length == 0 ? base.Update(): this.Update<Application>(filters);
		}

		public override UpdateResult UpdateByPrimaryKey()
		{
			return this.UpdateByPrimaryKey<Application>();
		}

		public override UpdateResult UpdateByValues()
		{
			return this.UpdateByValues<Application>();
		}

		public static Application[] SelectAll()
		{
			return Application.SelectAll<Application>();
		}

		public bool RecordIsAltered()
		{
			return this.RecordIsAltered<Application>();
		}

	}
}

