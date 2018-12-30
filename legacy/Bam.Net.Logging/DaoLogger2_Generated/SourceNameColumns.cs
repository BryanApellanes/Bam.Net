using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class SourceNameColumns: QueryFilter<SourceNameColumns>, IFilterToken
    {
        public SourceNameColumns() { }
        public SourceNameColumns(string columnName)
            : base(columnName)
        { }
		
		public SourceNameColumns KeyColumn
		{
			get
			{
				return new SourceNameColumns("Id");
			}
		}	

				
        public SourceNameColumns Id
        {
            get
            {
                return new SourceNameColumns("Id");
            }
        }
        public SourceNameColumns Uuid
        {
            get
            {
                return new SourceNameColumns("Uuid");
            }
        }
        public SourceNameColumns Cuid
        {
            get
            {
                return new SourceNameColumns("Cuid");
            }
        }
        public SourceNameColumns Value
        {
            get
            {
                return new SourceNameColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(SourceName);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}