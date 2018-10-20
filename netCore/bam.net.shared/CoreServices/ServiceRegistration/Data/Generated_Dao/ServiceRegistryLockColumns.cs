using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class ServiceRegistryLockColumns: QueryFilter<ServiceRegistryLockColumns>, IFilterToken
    {
        public ServiceRegistryLockColumns() { }
        public ServiceRegistryLockColumns(string columnName)
            : base(columnName)
        { }
		
		public ServiceRegistryLockColumns KeyColumn
		{
			get
			{
				return new ServiceRegistryLockColumns("Id");
			}
		}	

				
        public ServiceRegistryLockColumns Id
        {
            get
            {
                return new ServiceRegistryLockColumns("Id");
            }
        }
        public ServiceRegistryLockColumns Uuid
        {
            get
            {
                return new ServiceRegistryLockColumns("Uuid");
            }
        }
        public ServiceRegistryLockColumns Cuid
        {
            get
            {
                return new ServiceRegistryLockColumns("Cuid");
            }
        }
        public ServiceRegistryLockColumns Name
        {
            get
            {
                return new ServiceRegistryLockColumns("Name");
            }
        }
        public ServiceRegistryLockColumns CreatedBy
        {
            get
            {
                return new ServiceRegistryLockColumns("CreatedBy");
            }
        }
        public ServiceRegistryLockColumns ModifiedBy
        {
            get
            {
                return new ServiceRegistryLockColumns("ModifiedBy");
            }
        }
        public ServiceRegistryLockColumns Modified
        {
            get
            {
                return new ServiceRegistryLockColumns("Modified");
            }
        }
        public ServiceRegistryLockColumns Deleted
        {
            get
            {
                return new ServiceRegistryLockColumns("Deleted");
            }
        }
        public ServiceRegistryLockColumns Created
        {
            get
            {
                return new ServiceRegistryLockColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ServiceRegistryLock);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}