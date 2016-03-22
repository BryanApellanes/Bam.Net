using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class UserGroupColumns: QueryFilter<UserGroupColumns>, IFilterToken
    {
        public UserGroupColumns() { }
        public UserGroupColumns(string columnName)
            : base(columnName)
        { }
		
		public UserGroupColumns KeyColumn
		{
			get
			{
				return new UserGroupColumns("Id");
			}
		}	

				
        public UserGroupColumns Id
        {
            get
            {
                return new UserGroupColumns("Id");
            }
        }
        public UserGroupColumns Uuid
        {
            get
            {
                return new UserGroupColumns("Uuid");
            }
        }

        public UserGroupColumns UserId
        {
            get
            {
                return new UserGroupColumns("UserId");
            }
        }
        public UserGroupColumns GroupId
        {
            get
            {
                return new UserGroupColumns("GroupId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(UserGroup);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}