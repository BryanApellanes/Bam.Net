using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class MachineColumns: QueryFilter<MachineColumns>, IFilterToken
    {
        public MachineColumns() { }
        public MachineColumns(string columnName)
            : base(columnName)
        { }
		
		public MachineColumns KeyColumn
		{
			get
			{
				return new MachineColumns("Id");
			}
		}	

				
        public MachineColumns Id
        {
            get
            {
                return new MachineColumns("Id");
            }
        }
        public MachineColumns Uuid
        {
            get
            {
                return new MachineColumns("Uuid");
            }
        }
        public MachineColumns Cuid
        {
            get
            {
                return new MachineColumns("Cuid");
            }
        }
        public MachineColumns Name
        {
            get
            {
                return new MachineColumns("Name");
            }
        }
        public MachineColumns DnsName
        {
            get
            {
                return new MachineColumns("DnsName");
            }
        }
        public MachineColumns CreatedBy
        {
            get
            {
                return new MachineColumns("CreatedBy");
            }
        }
        public MachineColumns ModifiedBy
        {
            get
            {
                return new MachineColumns("ModifiedBy");
            }
        }
        public MachineColumns Modified
        {
            get
            {
                return new MachineColumns("Modified");
            }
        }
        public MachineColumns Deleted
        {
            get
            {
                return new MachineColumns("Deleted");
            }
        }
        public MachineColumns Created
        {
            get
            {
                return new MachineColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Machine);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}