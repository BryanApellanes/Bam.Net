using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bryan.Common.Data
{
    public class CommonItemColumns: QueryFilter<CommonItemColumns>, IFilterToken
    {
        public CommonItemColumns() { }
        public CommonItemColumns(string columnName)
            : base(columnName)
        { }
		
		public CommonItemColumns KeyColumn
		{
			get
			{
				return new CommonItemColumns("Id");
			}
		}	

				
        public CommonItemColumns Id
        {
            get
            {
                return new CommonItemColumns("Id");
            }
        }
        public CommonItemColumns Uuid
        {
            get
            {
                return new CommonItemColumns("Uuid");
            }
        }
        public CommonItemColumns Name
        {
            get
            {
                return new CommonItemColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(CommonItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}