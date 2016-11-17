using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ServiceProcessIdentifierColumns: QueryFilter<ServiceProcessIdentifierColumns>, IFilterToken
    {
        public ServiceProcessIdentifierColumns() { }
        public ServiceProcessIdentifierColumns(string columnName)
            : base(columnName)
        { }
		
		public ServiceProcessIdentifierColumns KeyColumn
		{
			get
			{
				return new ServiceProcessIdentifierColumns("Id");
			}
		}	

				
        public ServiceProcessIdentifierColumns Id
        {
            get
            {
                return new ServiceProcessIdentifierColumns("Id");
            }
        }
        public ServiceProcessIdentifierColumns Uuid
        {
            get
            {
                return new ServiceProcessIdentifierColumns("Uuid");
            }
        }
        public ServiceProcessIdentifierColumns Cuid
        {
            get
            {
                return new ServiceProcessIdentifierColumns("Cuid");
            }
        }
        public ServiceProcessIdentifierColumns MachineName
        {
            get
            {
                return new ServiceProcessIdentifierColumns("MachineName");
            }
        }
        public ServiceProcessIdentifierColumns ProcessId
        {
            get
            {
                return new ServiceProcessIdentifierColumns("ProcessId");
            }
        }
        public ServiceProcessIdentifierColumns FilePath
        {
            get
            {
                return new ServiceProcessIdentifierColumns("FilePath");
            }
        }
        public ServiceProcessIdentifierColumns CommandLine
        {
            get
            {
                return new ServiceProcessIdentifierColumns("CommandLine");
            }
        }
        public ServiceProcessIdentifierColumns IpAddresses
        {
            get
            {
                return new ServiceProcessIdentifierColumns("IpAddresses");
            }
        }
        public ServiceProcessIdentifierColumns CreatedBy
        {
            get
            {
                return new ServiceProcessIdentifierColumns("CreatedBy");
            }
        }
        public ServiceProcessIdentifierColumns Created
        {
            get
            {
                return new ServiceProcessIdentifierColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ServiceProcessIdentifier);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}