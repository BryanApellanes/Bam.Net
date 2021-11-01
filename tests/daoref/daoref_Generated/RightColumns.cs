using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class RightColumns: QueryFilter<RightColumns>, IFilterToken
    {
        public RightColumns() { }
        public RightColumns(string columnName)
            : base(columnName)
        { }
		
		public RightColumns KeyColumn
		{
			get
			{
				return new RightColumns("Id");
			}
		}	

        public RightColumns Id
        {
            get
            {
                return new RightColumns("Id");
            }
        }
        public RightColumns Uuid
        {
            get
            {
                return new RightColumns("Uuid");
            }
        }
        public RightColumns Cuid
        {
            get
            {
                return new RightColumns("Cuid");
            }
        }
        public RightColumns Name
        {
            get
            {
                return new RightColumns("Name");
            }
        }



		protected internal Type TableType
		{
			get
			{
				return typeof(Right);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}