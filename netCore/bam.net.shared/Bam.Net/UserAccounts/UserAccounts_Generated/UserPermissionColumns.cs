using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class UserPermissionColumns: QueryFilter<UserPermissionColumns>, IFilterToken
    {
        public UserPermissionColumns() { }
        public UserPermissionColumns(string columnName)
            : base(columnName)
        { }
		
		public UserPermissionColumns KeyColumn
		{
			get
			{
				return new UserPermissionColumns("Id");
			}
		}	

				
        public UserPermissionColumns Id
        {
            get
            {
                return new UserPermissionColumns("Id");
            }
        }
        public UserPermissionColumns Uuid
        {
            get
            {
                return new UserPermissionColumns("Uuid");
            }
        }

        public UserPermissionColumns UserId
        {
            get
            {
                return new UserPermissionColumns("UserId");
            }
        }
        public UserPermissionColumns PermissionId
        {
            get
            {
                return new UserPermissionColumns("PermissionId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(UserPermission);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}