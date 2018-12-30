using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class UserRoleColumns: QueryFilter<UserRoleColumns>, IFilterToken
    {
        public UserRoleColumns() { }
        public UserRoleColumns(string columnName)
            : base(columnName)
        { }
		
		public UserRoleColumns KeyColumn
		{
			get
			{
				return new UserRoleColumns("Id");
			}
		}	

				
        public UserRoleColumns Id
        {
            get
            {
                return new UserRoleColumns("Id");
            }
        }
        public UserRoleColumns Uuid
        {
            get
            {
                return new UserRoleColumns("Uuid");
            }
        }

        public UserRoleColumns UserId
        {
            get
            {
                return new UserRoleColumns("UserId");
            }
        }
        public UserRoleColumns RoleId
        {
            get
            {
                return new UserRoleColumns("RoleId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(UserRole);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}