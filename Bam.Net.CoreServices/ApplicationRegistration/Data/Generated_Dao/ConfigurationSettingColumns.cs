using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ConfigurationSettingColumns: QueryFilter<ConfigurationSettingColumns>, IFilterToken
    {
        public ConfigurationSettingColumns() { }
        public ConfigurationSettingColumns(string columnName)
            : base(columnName)
        { }
		
		public ConfigurationSettingColumns KeyColumn
		{
			get
			{
				return new ConfigurationSettingColumns("Id");
			}
		}	

				
        public ConfigurationSettingColumns Id
        {
            get
            {
                return new ConfigurationSettingColumns("Id");
            }
        }
        public ConfigurationSettingColumns Uuid
        {
            get
            {
                return new ConfigurationSettingColumns("Uuid");
            }
        }
        public ConfigurationSettingColumns Cuid
        {
            get
            {
                return new ConfigurationSettingColumns("Cuid");
            }
        }
        public ConfigurationSettingColumns Key
        {
            get
            {
                return new ConfigurationSettingColumns("Key");
            }
        }
        public ConfigurationSettingColumns Value
        {
            get
            {
                return new ConfigurationSettingColumns("Value");
            }
        }
        public ConfigurationSettingColumns CreatedBy
        {
            get
            {
                return new ConfigurationSettingColumns("CreatedBy");
            }
        }
        public ConfigurationSettingColumns ModifiedBy
        {
            get
            {
                return new ConfigurationSettingColumns("ModifiedBy");
            }
        }
        public ConfigurationSettingColumns Modified
        {
            get
            {
                return new ConfigurationSettingColumns("Modified");
            }
        }
        public ConfigurationSettingColumns Deleted
        {
            get
            {
                return new ConfigurationSettingColumns("Deleted");
            }
        }
        public ConfigurationSettingColumns Created
        {
            get
            {
                return new ConfigurationSettingColumns("Created");
            }
        }

        public ConfigurationSettingColumns ConfigurationId
        {
            get
            {
                return new ConfigurationSettingColumns("ConfigurationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ConfigurationSetting);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}