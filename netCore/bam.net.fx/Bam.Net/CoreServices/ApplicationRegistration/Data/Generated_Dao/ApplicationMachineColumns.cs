using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ApplicationMachineColumns: QueryFilter<ApplicationMachineColumns>, IFilterToken
    {
        public ApplicationMachineColumns() { }
        public ApplicationMachineColumns(string columnName)
            : base(columnName)
        { }
		
		public ApplicationMachineColumns KeyColumn
		{
			get
			{
				return new ApplicationMachineColumns("Id");
			}
		}	

				
        public ApplicationMachineColumns Id
        {
            get
            {
                return new ApplicationMachineColumns("Id");
            }
        }
        public ApplicationMachineColumns Uuid
        {
            get
            {
                return new ApplicationMachineColumns("Uuid");
            }
        }

        public ApplicationMachineColumns ApplicationId
        {
            get
            {
                return new ApplicationMachineColumns("ApplicationId");
            }
        }
        public ApplicationMachineColumns MachineId
        {
            get
            {
                return new ApplicationMachineColumns("MachineId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ApplicationMachine);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}