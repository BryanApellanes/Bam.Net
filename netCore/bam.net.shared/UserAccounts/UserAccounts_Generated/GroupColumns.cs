using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class GroupColumns: QueryFilter<GroupColumns>, IFilterToken
    {
        public GroupColumns() { }
        public GroupColumns(string columnName)
            : base(columnName)
        { }
		
		public GroupColumns KeyColumn
		{
			get
			{
				return new GroupColumns("Id");
			}
		}	

				
        public GroupColumns Id
        {
            get
            {
                return new GroupColumns("Id");
            }
        }
        public GroupColumns Uuid
        {
            get
            {
                return new GroupColumns("Uuid");
            }
        }
        public GroupColumns Cuid
        {
            get
            {
                return new GroupColumns("Cuid");
            }
        }
        public GroupColumns Name
        {
            get
            {
                return new GroupColumns("Name");
            }
        }

        public GroupColumns RoleId
        {
            get
            {
                return new GroupColumns("RoleId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Group);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}