using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ConfigurationColumns: QueryFilter<ConfigurationColumns>, IFilterToken
    {
        public ConfigurationColumns() { }
        public ConfigurationColumns(string columnName)
            : base(columnName)
        { }
		
		public ConfigurationColumns KeyColumn
		{
			get
			{
				return new ConfigurationColumns("Id");
			}
		}	

				
        public ConfigurationColumns Id
        {
            get
            {
                return new ConfigurationColumns("Id");
            }
        }
        public ConfigurationColumns Uuid
        {
            get
            {
                return new ConfigurationColumns("Uuid");
            }
        }
        public ConfigurationColumns Cuid
        {
            get
            {
                return new ConfigurationColumns("Cuid");
            }
        }
        public ConfigurationColumns Name
        {
            get
            {
                return new ConfigurationColumns("Name");
            }
        }
        public ConfigurationColumns CreatedBy
        {
            get
            {
                return new ConfigurationColumns("CreatedBy");
            }
        }
        public ConfigurationColumns ModifiedBy
        {
            get
            {
                return new ConfigurationColumns("ModifiedBy");
            }
        }
        public ConfigurationColumns Modified
        {
            get
            {
                return new ConfigurationColumns("Modified");
            }
        }
        public ConfigurationColumns Deleted
        {
            get
            {
                return new ConfigurationColumns("Deleted");
            }
        }
        public ConfigurationColumns Created
        {
            get
            {
                return new ConfigurationColumns("Created");
            }
        }

        public ConfigurationColumns ApplicationId
        {
            get
            {
                return new ConfigurationColumns("ApplicationId");
            }
        }
        public ConfigurationColumns MachineId
        {
            get
            {
                return new ConfigurationColumns("MachineId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Configuration);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}