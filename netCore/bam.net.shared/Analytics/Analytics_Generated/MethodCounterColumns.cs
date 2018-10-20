using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class MethodCounterColumns: QueryFilter<MethodCounterColumns>, IFilterToken
    {
        public MethodCounterColumns() { }
        public MethodCounterColumns(string columnName)
            : base(columnName)
        { }
		
		public MethodCounterColumns KeyColumn
		{
			get
			{
				return new MethodCounterColumns("Id");
			}
		}	

				
        public MethodCounterColumns Id
        {
            get
            {
                return new MethodCounterColumns("Id");
            }
        }
        public MethodCounterColumns Uuid
        {
            get
            {
                return new MethodCounterColumns("Uuid");
            }
        }
        public MethodCounterColumns Cuid
        {
            get
            {
                return new MethodCounterColumns("Cuid");
            }
        }
        public MethodCounterColumns MethodName
        {
            get
            {
                return new MethodCounterColumns("MethodName");
            }
        }

        public MethodCounterColumns CounterId
        {
            get
            {
                return new MethodCounterColumns("CounterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(MethodCounter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}