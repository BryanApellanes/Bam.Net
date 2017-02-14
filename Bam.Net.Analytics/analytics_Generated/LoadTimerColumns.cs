using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class LoadTimerColumns: QueryFilter<LoadTimerColumns>, IFilterToken
    {
        public LoadTimerColumns() { }
        public LoadTimerColumns(string columnName)
            : base(columnName)
        { }
		
		public LoadTimerColumns KeyColumn
		{
			get
			{
				return new LoadTimerColumns("Id");
			}
		}	

				
        public LoadTimerColumns Id
        {
            get
            {
                return new LoadTimerColumns("Id");
            }
        }
        public LoadTimerColumns Uuid
        {
            get
            {
                return new LoadTimerColumns("Uuid");
            }
        }
        public LoadTimerColumns Cuid
        {
            get
            {
                return new LoadTimerColumns("Cuid");
            }
        }
        public LoadTimerColumns UrlId
        {
            get
            {
                return new LoadTimerColumns("UrlId");
            }
        }

        public LoadTimerColumns TimerId
        {
            get
            {
                return new LoadTimerColumns("TimerId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(LoadTimer);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}