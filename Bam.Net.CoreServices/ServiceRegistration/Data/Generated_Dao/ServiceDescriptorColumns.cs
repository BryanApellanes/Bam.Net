using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class ServiceDescriptorColumns: QueryFilter<ServiceDescriptorColumns>, IFilterToken
    {
        public ServiceDescriptorColumns() { }
        public ServiceDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public ServiceDescriptorColumns KeyColumn
		{
			get
			{
				return new ServiceDescriptorColumns("Id");
			}
		}	

				
        public ServiceDescriptorColumns Id
        {
            get
            {
                return new ServiceDescriptorColumns("Id");
            }
        }
        public ServiceDescriptorColumns Uuid
        {
            get
            {
                return new ServiceDescriptorColumns("Uuid");
            }
        }
        public ServiceDescriptorColumns Cuid
        {
            get
            {
                return new ServiceDescriptorColumns("Cuid");
            }
        }
        public ServiceDescriptorColumns SequenceNumber
        {
            get
            {
                return new ServiceDescriptorColumns("SequenceNumber");
            }
        }
        public ServiceDescriptorColumns ForTypeDurableHash
        {
            get
            {
                return new ServiceDescriptorColumns("ForTypeDurableHash");
            }
        }
        public ServiceDescriptorColumns ForTypeDurableSecondaryHash
        {
            get
            {
                return new ServiceDescriptorColumns("ForTypeDurableSecondaryHash");
            }
        }
        public ServiceDescriptorColumns UseTypeDurableHash
        {
            get
            {
                return new ServiceDescriptorColumns("UseTypeDurableHash");
            }
        }
        public ServiceDescriptorColumns UseTypeDurableSecondaryHash
        {
            get
            {
                return new ServiceDescriptorColumns("UseTypeDurableSecondaryHash");
            }
        }
        public ServiceDescriptorColumns CreatedBy
        {
            get
            {
                return new ServiceDescriptorColumns("CreatedBy");
            }
        }
        public ServiceDescriptorColumns ModifiedBy
        {
            get
            {
                return new ServiceDescriptorColumns("ModifiedBy");
            }
        }
        public ServiceDescriptorColumns Modified
        {
            get
            {
                return new ServiceDescriptorColumns("Modified");
            }
        }
        public ServiceDescriptorColumns Deleted
        {
            get
            {
                return new ServiceDescriptorColumns("Deleted");
            }
        }
        public ServiceDescriptorColumns Created
        {
            get
            {
                return new ServiceDescriptorColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ServiceDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}