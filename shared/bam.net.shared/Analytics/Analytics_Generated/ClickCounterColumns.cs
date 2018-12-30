using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class ClickCounterColumns: QueryFilter<ClickCounterColumns>, IFilterToken
    {
        public ClickCounterColumns() { }
        public ClickCounterColumns(string columnName)
            : base(columnName)
        { }
		
		public ClickCounterColumns KeyColumn
		{
			get
			{
				return new ClickCounterColumns("Id");
			}
		}	

				
        public ClickCounterColumns Id
        {
            get
            {
                return new ClickCounterColumns("Id");
            }
        }
        public ClickCounterColumns Uuid
        {
            get
            {
                return new ClickCounterColumns("Uuid");
            }
        }
        public ClickCounterColumns Cuid
        {
            get
            {
                return new ClickCounterColumns("Cuid");
            }
        }
        public ClickCounterColumns UrlId
        {
            get
            {
                return new ClickCounterColumns("UrlId");
            }
        }

        public ClickCounterColumns CounterId
        {
            get
            {
                return new ClickCounterColumns("CounterId");
            }
        }
        public ClickCounterColumns UserIdentifierId
        {
            get
            {
                return new ClickCounterColumns("UserIdentifierId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ClickCounter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}