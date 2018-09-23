using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class ServiceRegistryLoaderDescriptorColumns: QueryFilter<ServiceRegistryLoaderDescriptorColumns>, IFilterToken
    {
        public ServiceRegistryLoaderDescriptorColumns() { }
        public ServiceRegistryLoaderDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public ServiceRegistryLoaderDescriptorColumns KeyColumn
		{
			get
			{
				return new ServiceRegistryLoaderDescriptorColumns("Id");
			}
		}	

				
        public ServiceRegistryLoaderDescriptorColumns Id
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("Id");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns Uuid
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("Uuid");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns Cuid
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("Cuid");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns Name
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("Name");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns Description
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("Description");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns LoaderType
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("LoaderType");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns LoaderAssembly
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("LoaderAssembly");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns LoaderMethod
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("LoaderMethod");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns CreatedBy
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("CreatedBy");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns ModifiedBy
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("ModifiedBy");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns Modified
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("Modified");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns Deleted
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("Deleted");
            }
        }
        public ServiceRegistryLoaderDescriptorColumns Created
        {
            get
            {
                return new ServiceRegistryLoaderDescriptorColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ServiceRegistryLoaderDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}