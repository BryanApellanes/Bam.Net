using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class LeftOfManyItemColumns: QueryFilter<LeftOfManyItemColumns>, IFilterToken
    {
        public LeftOfManyItemColumns() { }
        public LeftOfManyItemColumns(string columnName)
            : base(columnName)
        { }
		
		public LeftOfManyItemColumns KeyColumn
		{
			get
			{
				return new LeftOfManyItemColumns("Id");
			}
		}	

				
        public LeftOfManyItemColumns Id
        {
            get
            {
                return new LeftOfManyItemColumns("Id");
            }
        }
        public LeftOfManyItemColumns Uuid
        {
            get
            {
                return new LeftOfManyItemColumns("Uuid");
            }
        }
        public LeftOfManyItemColumns Name
        {
            get
            {
                return new LeftOfManyItemColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(LeftOfManyItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}