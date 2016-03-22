using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class LeftOfManyItemRightOfManyItemColumns: QueryFilter<LeftOfManyItemRightOfManyItemColumns>, IFilterToken
    {
        public LeftOfManyItemRightOfManyItemColumns() { }
        public LeftOfManyItemRightOfManyItemColumns(string columnName)
            : base(columnName)
        { }
		
		public LeftOfManyItemRightOfManyItemColumns KeyColumn
		{
			get
			{
				return new LeftOfManyItemRightOfManyItemColumns("Id");
			}
		}	

				
        public LeftOfManyItemRightOfManyItemColumns Id
        {
            get
            {
                return new LeftOfManyItemRightOfManyItemColumns("Id");
            }
        }
        public LeftOfManyItemRightOfManyItemColumns Uuid
        {
            get
            {
                return new LeftOfManyItemRightOfManyItemColumns("Uuid");
            }
        }

        public LeftOfManyItemRightOfManyItemColumns LeftOfManyItemId
        {
            get
            {
                return new LeftOfManyItemRightOfManyItemColumns("LeftOfManyItemId");
            }
        }
        public LeftOfManyItemRightOfManyItemColumns RightOfManyItemId
        {
            get
            {
                return new LeftOfManyItemRightOfManyItemColumns("RightOfManyItemId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(LeftOfManyItemRightOfManyItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}