using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class IpAddressColumns: QueryFilter<IpAddressColumns>, IFilterToken
    {
        public IpAddressColumns() { }
        public IpAddressColumns(string columnName)
            : base(columnName)
        { }
		
		public IpAddressColumns KeyColumn
		{
			get
			{
				return new IpAddressColumns("Id");
			}
		}	

				
        public IpAddressColumns Id
        {
            get
            {
                return new IpAddressColumns("Id");
            }
        }
        public IpAddressColumns Uuid
        {
            get
            {
                return new IpAddressColumns("Uuid");
            }
        }
        public IpAddressColumns Cuid
        {
            get
            {
                return new IpAddressColumns("Cuid");
            }
        }
        public IpAddressColumns AddressFamily
        {
            get
            {
                return new IpAddressColumns("AddressFamily");
            }
        }
        public IpAddressColumns Value
        {
            get
            {
                return new IpAddressColumns("Value");
            }
        }
        public IpAddressColumns CreatedBy
        {
            get
            {
                return new IpAddressColumns("CreatedBy");
            }
        }
        public IpAddressColumns Created
        {
            get
            {
                return new IpAddressColumns("Created");
            }
        }
        public IpAddressColumns ModifiedBy
        {
            get
            {
                return new IpAddressColumns("ModifiedBy");
            }
        }
        public IpAddressColumns Modified
        {
            get
            {
                return new IpAddressColumns("Modified");
            }
        }
        public IpAddressColumns Deleted
        {
            get
            {
                return new IpAddressColumns("Deleted");
            }
        }

        public IpAddressColumns MachineId
        {
            get
            {
                return new IpAddressColumns("MachineId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(IpAddress);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}