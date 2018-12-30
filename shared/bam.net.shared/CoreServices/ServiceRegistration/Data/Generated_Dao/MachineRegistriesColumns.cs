using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class MachineRegistriesColumns: QueryFilter<MachineRegistriesColumns>, IFilterToken
    {
        public MachineRegistriesColumns() { }
        public MachineRegistriesColumns(string columnName)
            : base(columnName)
        { }
		
		public MachineRegistriesColumns KeyColumn
		{
			get
			{
				return new MachineRegistriesColumns("Id");
			}
		}	

				
        public MachineRegistriesColumns Id
        {
            get
            {
                return new MachineRegistriesColumns("Id");
            }
        }
        public MachineRegistriesColumns Uuid
        {
            get
            {
                return new MachineRegistriesColumns("Uuid");
            }
        }
        public MachineRegistriesColumns Cuid
        {
            get
            {
                return new MachineRegistriesColumns("Cuid");
            }
        }
        public MachineRegistriesColumns Name
        {
            get
            {
                return new MachineRegistriesColumns("Name");
            }
        }
        public MachineRegistriesColumns DnsName
        {
            get
            {
                return new MachineRegistriesColumns("DnsName");
            }
        }
        public MachineRegistriesColumns RegistryNames
        {
            get
            {
                return new MachineRegistriesColumns("RegistryNames");
            }
        }
        public MachineRegistriesColumns CreatedBy
        {
            get
            {
                return new MachineRegistriesColumns("CreatedBy");
            }
        }
        public MachineRegistriesColumns ModifiedBy
        {
            get
            {
                return new MachineRegistriesColumns("ModifiedBy");
            }
        }
        public MachineRegistriesColumns Modified
        {
            get
            {
                return new MachineRegistriesColumns("Modified");
            }
        }
        public MachineRegistriesColumns Deleted
        {
            get
            {
                return new MachineRegistriesColumns("Deleted");
            }
        }
        public MachineRegistriesColumns Created
        {
            get
            {
                return new MachineRegistriesColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(MachineRegistries);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}