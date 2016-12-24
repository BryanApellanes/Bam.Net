using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class MachineApplicationColumns: QueryFilter<MachineApplicationColumns>, IFilterToken
    {
        public MachineApplicationColumns() { }
        public MachineApplicationColumns(string columnName)
            : base(columnName)
        { }
		
		public MachineApplicationColumns KeyColumn
		{
			get
			{
				return new MachineApplicationColumns("Id");
			}
		}	

				
        public MachineApplicationColumns Id
        {
            get
            {
                return new MachineApplicationColumns("Id");
            }
        }
        public MachineApplicationColumns Uuid
        {
            get
            {
                return new MachineApplicationColumns("Uuid");
            }
        }

        public MachineApplicationColumns MachineId
        {
            get
            {
                return new MachineApplicationColumns("MachineId");
            }
        }
        public MachineApplicationColumns ApplicationId
        {
            get
            {
                return new MachineApplicationColumns("ApplicationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(MachineApplication);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}