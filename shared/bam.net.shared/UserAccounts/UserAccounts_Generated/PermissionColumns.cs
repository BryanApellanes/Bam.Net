using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class PermissionColumns: QueryFilter<PermissionColumns>, IFilterToken
    {
        public PermissionColumns() { }
        public PermissionColumns(string columnName)
            : base(columnName)
        { }
		
		public PermissionColumns KeyColumn
		{
			get
			{
				return new PermissionColumns("Id");
			}
		}	

				
        public PermissionColumns Id
        {
            get
            {
                return new PermissionColumns("Id");
            }
        }
        public PermissionColumns Uuid
        {
            get
            {
                return new PermissionColumns("Uuid");
            }
        }
        public PermissionColumns Cuid
        {
            get
            {
                return new PermissionColumns("Cuid");
            }
        }
        public PermissionColumns Name
        {
            get
            {
                return new PermissionColumns("Name");
            }
        }
        public PermissionColumns Value
        {
            get
            {
                return new PermissionColumns("Value");
            }
        }

        public PermissionColumns TreeNodeId
        {
            get
            {
                return new PermissionColumns("TreeNodeId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Permission);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}