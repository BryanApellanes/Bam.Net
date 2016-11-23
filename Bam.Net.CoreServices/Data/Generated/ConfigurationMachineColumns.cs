using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ConfigurationMachineColumns: QueryFilter<ConfigurationMachineColumns>, IFilterToken
    {
        public ConfigurationMachineColumns() { }
        public ConfigurationMachineColumns(string columnName)
            : base(columnName)
        { }
		
		public ConfigurationMachineColumns KeyColumn
		{
			get
			{
				return new ConfigurationMachineColumns("Id");
			}
		}	

				
        public ConfigurationMachineColumns Id
        {
            get
            {
                return new ConfigurationMachineColumns("Id");
            }
        }
        public ConfigurationMachineColumns Uuid
        {
            get
            {
                return new ConfigurationMachineColumns("Uuid");
            }
        }

        public ConfigurationMachineColumns ConfigurationId
        {
            get
            {
                return new ConfigurationMachineColumns("ConfigurationId");
            }
        }
        public ConfigurationMachineColumns MachineId
        {
            get
            {
                return new ConfigurationMachineColumns("MachineId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ConfigurationMachine);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}