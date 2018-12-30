using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class CounterColumns: QueryFilter<CounterColumns>, IFilterToken
    {
        public CounterColumns() { }
        public CounterColumns(string columnName)
            : base(columnName)
        { }
		
		public CounterColumns KeyColumn
		{
			get
			{
				return new CounterColumns("Id");
			}
		}	

				
        public CounterColumns Id
        {
            get
            {
                return new CounterColumns("Id");
            }
        }
        public CounterColumns Uuid
        {
            get
            {
                return new CounterColumns("Uuid");
            }
        }
        public CounterColumns Cuid
        {
            get
            {
                return new CounterColumns("Cuid");
            }
        }
        public CounterColumns Value
        {
            get
            {
                return new CounterColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Counter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}