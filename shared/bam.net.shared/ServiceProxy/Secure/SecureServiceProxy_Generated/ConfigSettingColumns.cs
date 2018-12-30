using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ConfigSettingColumns: QueryFilter<ConfigSettingColumns>, IFilterToken
    {
        public ConfigSettingColumns() { }
        public ConfigSettingColumns(string columnName)
            : base(columnName)
        { }
		
		public ConfigSettingColumns KeyColumn
		{
			get
			{
				return new ConfigSettingColumns("Id");
			}
		}	

				
        public ConfigSettingColumns Id
        {
            get
            {
                return new ConfigSettingColumns("Id");
            }
        }
        public ConfigSettingColumns Uuid
        {
            get
            {
                return new ConfigSettingColumns("Uuid");
            }
        }
        public ConfigSettingColumns Cuid
        {
            get
            {
                return new ConfigSettingColumns("Cuid");
            }
        }
        public ConfigSettingColumns Key
        {
            get
            {
                return new ConfigSettingColumns("Key");
            }
        }
        public ConfigSettingColumns Value
        {
            get
            {
                return new ConfigSettingColumns("Value");
            }
        }

        public ConfigSettingColumns ConfigurationId
        {
            get
            {
                return new ConfigSettingColumns("ConfigurationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ConfigSetting);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}