using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class GroupPermissionColumns: QueryFilter<GroupPermissionColumns>, IFilterToken
    {
        public GroupPermissionColumns() { }
        public GroupPermissionColumns(string columnName)
            : base(columnName)
        { }
		
		public GroupPermissionColumns KeyColumn
		{
			get
			{
				return new GroupPermissionColumns("Id");
			}
		}	

				
        public GroupPermissionColumns Id
        {
            get
            {
                return new GroupPermissionColumns("Id");
            }
        }
        public GroupPermissionColumns Uuid
        {
            get
            {
                return new GroupPermissionColumns("Uuid");
            }
        }

        public GroupPermissionColumns GroupId
        {
            get
            {
                return new GroupPermissionColumns("GroupId");
            }
        }
        public GroupPermissionColumns PermissionId
        {
            get
            {
                return new GroupPermissionColumns("PermissionId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(GroupPermission);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}