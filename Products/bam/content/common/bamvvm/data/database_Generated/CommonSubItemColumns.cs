using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bryan.Common.Data
{
    public class CommonSubItemColumns: QueryFilter<CommonSubItemColumns>, IFilterToken
    {
        public CommonSubItemColumns() { }
        public CommonSubItemColumns(string columnName)
            : base(columnName)
        { }
		
		public CommonSubItemColumns KeyColumn
		{
			get
			{
				return new CommonSubItemColumns("Id");
			}
		}	

				
        public CommonSubItemColumns Id
        {
            get
            {
                return new CommonSubItemColumns("Id");
            }
        }
        public CommonSubItemColumns Uuid
        {
            get
            {
                return new CommonSubItemColumns("Uuid");
            }
        }
        public CommonSubItemColumns Name
        {
            get
            {
                return new CommonSubItemColumns("Name");
            }
        }
        public CommonSubItemColumns Created
        {
            get
            {
                return new CommonSubItemColumns("Created");
            }
        }

        public CommonSubItemColumns CommonItemId
        {
            get
            {
                return new CommonSubItemColumns("CommonItemId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(CommonSubItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}