using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
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
        public ConfigurationColumns Key
        {
            get
            {
                return new ConfigurationColumns("Key");
            }
        }
        public ConfigurationColumns Value
        {
            get
            {
                return new ConfigurationColumns("Value");
            }
        }
        public ConfigurationColumns Created
        {
            get
            {
                return new ConfigurationColumns("Created");
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