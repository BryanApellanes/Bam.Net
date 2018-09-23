using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class TreeNodeColumns: QueryFilter<TreeNodeColumns>, IFilterToken
    {
        public TreeNodeColumns() { }
        public TreeNodeColumns(string columnName)
            : base(columnName)
        { }
		
		public TreeNodeColumns KeyColumn
		{
			get
			{
				return new TreeNodeColumns("Id");
			}
		}	

				
        public TreeNodeColumns Id
        {
            get
            {
                return new TreeNodeColumns("Id");
            }
        }
        public TreeNodeColumns Uuid
        {
            get
            {
                return new TreeNodeColumns("Uuid");
            }
        }
        public TreeNodeColumns Cuid
        {
            get
            {
                return new TreeNodeColumns("Cuid");
            }
        }
        public TreeNodeColumns Name
        {
            get
            {
                return new TreeNodeColumns("Name");
            }
        }
        public TreeNodeColumns Value
        {
            get
            {
                return new TreeNodeColumns("Value");
            }
        }

        public TreeNodeColumns ParentTreeNodeId
        {
            get
            {
                return new TreeNodeColumns("ParentTreeNodeId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(TreeNode);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}