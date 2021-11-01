using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class LeftRightColumns: QueryFilter<LeftRightColumns>, IFilterToken
    {
        public LeftRightColumns() { }
        public LeftRightColumns(string columnName)
            : base(columnName)
        { }
		
		public LeftRightColumns KeyColumn
		{
			get
			{
				return new LeftRightColumns("Id");
			}
		}	

        public LeftRightColumns Id
        {
            get
            {
                return new LeftRightColumns("Id");
            }
        }
        public LeftRightColumns Uuid
        {
            get
            {
                return new LeftRightColumns("Uuid");
            }
        }


        public LeftRightColumns LeftId
        {
            get
            {
                return new LeftRightColumns("LeftId");
            }
        }
        public LeftRightColumns RightId
        {
            get
            {
                return new LeftRightColumns("RightId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(LeftRight);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}