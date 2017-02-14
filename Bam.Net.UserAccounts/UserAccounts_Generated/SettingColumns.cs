using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class SettingColumns: QueryFilter<SettingColumns>, IFilterToken
    {
        public SettingColumns() { }
        public SettingColumns(string columnName)
            : base(columnName)
        { }
		
		public SettingColumns KeyColumn
		{
			get
			{
				return new SettingColumns("Id");
			}
		}	

				
        public SettingColumns Id
        {
            get
            {
                return new SettingColumns("Id");
            }
        }
        public SettingColumns Uuid
        {
            get
            {
                return new SettingColumns("Uuid");
            }
        }
        public SettingColumns Cuid
        {
            get
            {
                return new SettingColumns("Cuid");
            }
        }
        public SettingColumns Key
        {
            get
            {
                return new SettingColumns("Key");
            }
        }
        public SettingColumns ValueType
        {
            get
            {
                return new SettingColumns("ValueType");
            }
        }
        public SettingColumns Value
        {
            get
            {
                return new SettingColumns("Value");
            }
        }

        public SettingColumns UserId
        {
            get
            {
                return new SettingColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Setting);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}