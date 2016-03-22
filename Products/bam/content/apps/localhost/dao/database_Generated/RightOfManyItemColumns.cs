using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class RightOfManyItemColumns: QueryFilter<RightOfManyItemColumns>, IFilterToken
    {
        public RightOfManyItemColumns() { }
        public RightOfManyItemColumns(string columnName)
            : base(columnName)
        { }
		
		public RightOfManyItemColumns KeyColumn
		{
			get
			{
				return new RightOfManyItemColumns("Id");
			}
		}	

				
        public RightOfManyItemColumns Id
        {
            get
            {
                return new RightOfManyItemColumns("Id");
            }
        }
        public RightOfManyItemColumns Uuid
        {
            get
            {
                return new RightOfManyItemColumns("Uuid");
            }
        }
        public RightOfManyItemColumns Name
        {
            get
            {
                return new RightOfManyItemColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(RightOfManyItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}