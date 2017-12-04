using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class HostDomainApplicationColumns: QueryFilter<HostDomainApplicationColumns>, IFilterToken
    {
        public HostDomainApplicationColumns() { }
        public HostDomainApplicationColumns(string columnName)
            : base(columnName)
        { }
		
		public HostDomainApplicationColumns KeyColumn
		{
			get
			{
				return new HostDomainApplicationColumns("Id");
			}
		}	

				
        public HostDomainApplicationColumns Id
        {
            get
            {
                return new HostDomainApplicationColumns("Id");
            }
        }
        public HostDomainApplicationColumns Uuid
        {
            get
            {
                return new HostDomainApplicationColumns("Uuid");
            }
        }

        public HostDomainApplicationColumns HostDomainId
        {
            get
            {
                return new HostDomainApplicationColumns("HostDomainId");
            }
        }
        public HostDomainApplicationColumns ApplicationId
        {
            get
            {
                return new HostDomainApplicationColumns("ApplicationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(HostDomainApplication);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}