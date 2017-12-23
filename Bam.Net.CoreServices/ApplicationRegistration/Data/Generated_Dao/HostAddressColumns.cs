using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class HostAddressColumns: QueryFilter<HostAddressColumns>, IFilterToken
    {
        public HostAddressColumns() { }
        public HostAddressColumns(string columnName)
            : base(columnName)
        { }
		
		public HostAddressColumns KeyColumn
		{
			get
			{
				return new HostAddressColumns("Id");
			}
		}	

				
        public HostAddressColumns Id
        {
            get
            {
                return new HostAddressColumns("Id");
            }
        }
        public HostAddressColumns Uuid
        {
            get
            {
                return new HostAddressColumns("Uuid");
            }
        }
        public HostAddressColumns Cuid
        {
            get
            {
                return new HostAddressColumns("Cuid");
            }
        }
        public HostAddressColumns IpAddress
        {
            get
            {
                return new HostAddressColumns("IpAddress");
            }
        }
        public HostAddressColumns AddressFamily
        {
            get
            {
                return new HostAddressColumns("AddressFamily");
            }
        }
        public HostAddressColumns HostName
        {
            get
            {
                return new HostAddressColumns("HostName");
            }
        }
        public HostAddressColumns Created
        {
            get
            {
                return new HostAddressColumns("Created");
            }
        }

        public HostAddressColumns MachineId
        {
            get
            {
                return new HostAddressColumns("MachineId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(HostAddress);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}