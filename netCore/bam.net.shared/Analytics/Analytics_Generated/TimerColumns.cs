using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class TimerColumns: QueryFilter<TimerColumns>, IFilterToken
    {
        public TimerColumns() { }
        public TimerColumns(string columnName)
            : base(columnName)
        { }
		
		public TimerColumns KeyColumn
		{
			get
			{
				return new TimerColumns("Id");
			}
		}	

				
        public TimerColumns Id
        {
            get
            {
                return new TimerColumns("Id");
            }
        }
        public TimerColumns Uuid
        {
            get
            {
                return new TimerColumns("Uuid");
            }
        }
        public TimerColumns Cuid
        {
            get
            {
                return new TimerColumns("Cuid");
            }
        }
        public TimerColumns Value
        {
            get
            {
                return new TimerColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Timer);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}