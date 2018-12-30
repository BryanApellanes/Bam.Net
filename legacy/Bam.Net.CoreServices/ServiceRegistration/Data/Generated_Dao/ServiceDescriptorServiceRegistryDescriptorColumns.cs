using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class ServiceDescriptorServiceRegistryDescriptorColumns: QueryFilter<ServiceDescriptorServiceRegistryDescriptorColumns>, IFilterToken
    {
        public ServiceDescriptorServiceRegistryDescriptorColumns() { }
        public ServiceDescriptorServiceRegistryDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public ServiceDescriptorServiceRegistryDescriptorColumns KeyColumn
		{
			get
			{
				return new ServiceDescriptorServiceRegistryDescriptorColumns("Id");
			}
		}	

				
        public ServiceDescriptorServiceRegistryDescriptorColumns Id
        {
            get
            {
                return new ServiceDescriptorServiceRegistryDescriptorColumns("Id");
            }
        }
        public ServiceDescriptorServiceRegistryDescriptorColumns Uuid
        {
            get
            {
                return new ServiceDescriptorServiceRegistryDescriptorColumns("Uuid");
            }
        }

        public ServiceDescriptorServiceRegistryDescriptorColumns ServiceDescriptorId
        {
            get
            {
                return new ServiceDescriptorServiceRegistryDescriptorColumns("ServiceDescriptorId");
            }
        }
        public ServiceDescriptorServiceRegistryDescriptorColumns ServiceRegistryDescriptorId
        {
            get
            {
                return new ServiceDescriptorServiceRegistryDescriptorColumns("ServiceRegistryDescriptorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ServiceDescriptorServiceRegistryDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}