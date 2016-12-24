using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ConfigurationApplicationColumns: QueryFilter<ConfigurationApplicationColumns>, IFilterToken
    {
        public ConfigurationApplicationColumns() { }
        public ConfigurationApplicationColumns(string columnName)
            : base(columnName)
        { }
		
		public ConfigurationApplicationColumns KeyColumn
		{
			get
			{
				return new ConfigurationApplicationColumns("Id");
			}
		}	

				
        public ConfigurationApplicationColumns Id
        {
            get
            {
                return new ConfigurationApplicationColumns("Id");
            }
        }
        public ConfigurationApplicationColumns Uuid
        {
            get
            {
                return new ConfigurationApplicationColumns("Uuid");
            }
        }

        public ConfigurationApplicationColumns ConfigurationId
        {
            get
            {
                return new ConfigurationApplicationColumns("ConfigurationId");
            }
        }
        public ConfigurationApplicationColumns ApplicationId
        {
            get
            {
                return new ConfigurationApplicationColumns("ApplicationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ConfigurationApplication);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}