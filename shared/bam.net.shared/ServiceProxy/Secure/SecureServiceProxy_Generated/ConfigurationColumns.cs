using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
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

        public ConfigurationColumns ApplicationId
        {
            get
            {
                return new ConfigurationColumns("ApplicationId");
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