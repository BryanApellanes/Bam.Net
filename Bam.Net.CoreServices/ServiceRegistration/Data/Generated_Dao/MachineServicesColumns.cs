using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class MachineServicesColumns: QueryFilter<MachineServicesColumns>, IFilterToken
    {
        public MachineServicesColumns() { }
        public MachineServicesColumns(string columnName)
            : base(columnName)
        { }
		
		public MachineServicesColumns KeyColumn
		{
			get
			{
				return new MachineServicesColumns("Id");
			}
		}	

				
        public MachineServicesColumns Id
        {
            get
            {
                return new MachineServicesColumns("Id");
            }
        }
        public MachineServicesColumns Uuid
        {
            get
            {
                return new MachineServicesColumns("Uuid");
            }
        }
        public MachineServicesColumns Cuid
        {
            get
            {
                return new MachineServicesColumns("Cuid");
            }
        }
        public MachineServicesColumns Name
        {
            get
            {
                return new MachineServicesColumns("Name");
            }
        }
        public MachineServicesColumns DnsName
        {
            get
            {
                return new MachineServicesColumns("DnsName");
            }
        }
        public MachineServicesColumns ServiceRegistries
        {
            get
            {
                return new MachineServicesColumns("ServiceRegistries");
            }
        }
        public MachineServicesColumns CreatedBy
        {
            get
            {
                return new MachineServicesColumns("CreatedBy");
            }
        }
        public MachineServicesColumns ModifiedBy
        {
            get
            {
                return new MachineServicesColumns("ModifiedBy");
            }
        }
        public MachineServicesColumns Modified
        {
            get
            {
                return new MachineServicesColumns("Modified");
            }
        }
        public MachineServicesColumns Deleted
        {
            get
            {
                return new MachineServicesColumns("Deleted");
            }
        }
        public MachineServicesColumns Created
        {
            get
            {
                return new MachineServicesColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(MachineServices);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}