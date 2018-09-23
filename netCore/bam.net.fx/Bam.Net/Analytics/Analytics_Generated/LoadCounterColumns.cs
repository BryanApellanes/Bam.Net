using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class LoadCounterColumns: QueryFilter<LoadCounterColumns>, IFilterToken
    {
        public LoadCounterColumns() { }
        public LoadCounterColumns(string columnName)
            : base(columnName)
        { }
		
		public LoadCounterColumns KeyColumn
		{
			get
			{
				return new LoadCounterColumns("Id");
			}
		}	

				
        public LoadCounterColumns Id
        {
            get
            {
                return new LoadCounterColumns("Id");
            }
        }
        public LoadCounterColumns Uuid
        {
            get
            {
                return new LoadCounterColumns("Uuid");
            }
        }
        public LoadCounterColumns Cuid
        {
            get
            {
                return new LoadCounterColumns("Cuid");
            }
        }
        public LoadCounterColumns UrlUuid
        {
            get
            {
                return new LoadCounterColumns("UrlUuid");
            }
        }

        public LoadCounterColumns CounterId
        {
            get
            {
                return new LoadCounterColumns("CounterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(LoadCounter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}