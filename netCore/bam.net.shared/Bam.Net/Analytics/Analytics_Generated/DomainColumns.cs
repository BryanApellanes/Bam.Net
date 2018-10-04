using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class DomainColumns: QueryFilter<DomainColumns>, IFilterToken
    {
        public DomainColumns() { }
        public DomainColumns(string columnName)
            : base(columnName)
        { }
		
		public DomainColumns KeyColumn
		{
			get
			{
				return new DomainColumns("Id");
			}
		}	

				
        public DomainColumns Id
        {
            get
            {
                return new DomainColumns("Id");
            }
        }
        public DomainColumns Uuid
        {
            get
            {
                return new DomainColumns("Uuid");
            }
        }
        public DomainColumns Cuid
        {
            get
            {
                return new DomainColumns("Cuid");
            }
        }
        public DomainColumns Value
        {
            get
            {
                return new DomainColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Domain);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}