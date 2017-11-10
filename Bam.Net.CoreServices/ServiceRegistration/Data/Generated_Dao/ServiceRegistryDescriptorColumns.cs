using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class ServiceRegistryDescriptorColumns: QueryFilter<ServiceRegistryDescriptorColumns>, IFilterToken
    {
        public ServiceRegistryDescriptorColumns() { }
        public ServiceRegistryDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public ServiceRegistryDescriptorColumns KeyColumn
		{
			get
			{
				return new ServiceRegistryDescriptorColumns("Id");
			}
		}	

				
        public ServiceRegistryDescriptorColumns Id
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("Id");
            }
        }
        public ServiceRegistryDescriptorColumns Uuid
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("Uuid");
            }
        }
        public ServiceRegistryDescriptorColumns Cuid
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("Cuid");
            }
        }
        public ServiceRegistryDescriptorColumns Name
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("Name");
            }
        }
        public ServiceRegistryDescriptorColumns Description
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("Description");
            }
        }
        public ServiceRegistryDescriptorColumns CreatedBy
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("CreatedBy");
            }
        }
        public ServiceRegistryDescriptorColumns ModifiedBy
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("ModifiedBy");
            }
        }
        public ServiceRegistryDescriptorColumns Modified
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("Modified");
            }
        }
        public ServiceRegistryDescriptorColumns Deleted
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("Deleted");
            }
        }
        public ServiceRegistryDescriptorColumns Created
        {
            get
            {
                return new ServiceRegistryDescriptorColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ServiceRegistryDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}