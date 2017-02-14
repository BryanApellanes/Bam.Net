using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class MethodTimerColumns: QueryFilter<MethodTimerColumns>, IFilterToken
    {
        public MethodTimerColumns() { }
        public MethodTimerColumns(string columnName)
            : base(columnName)
        { }
		
		public MethodTimerColumns KeyColumn
		{
			get
			{
				return new MethodTimerColumns("Id");
			}
		}	

				
        public MethodTimerColumns Id
        {
            get
            {
                return new MethodTimerColumns("Id");
            }
        }
        public MethodTimerColumns Uuid
        {
            get
            {
                return new MethodTimerColumns("Uuid");
            }
        }
        public MethodTimerColumns Cuid
        {
            get
            {
                return new MethodTimerColumns("Cuid");
            }
        }
        public MethodTimerColumns MethodName
        {
            get
            {
                return new MethodTimerColumns("MethodName");
            }
        }

        public MethodTimerColumns TimerId
        {
            get
            {
                return new MethodTimerColumns("TimerId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(MethodTimer);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}