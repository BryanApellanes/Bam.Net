using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class UserSettingColumns: QueryFilter<UserSettingColumns>, IFilterToken
    {
        public UserSettingColumns() { }
        public UserSettingColumns(string columnName)
            : base(columnName)
        { }
		
		public UserSettingColumns KeyColumn
		{
			get
			{
				return new UserSettingColumns("Id");
			}
		}	

				
        public UserSettingColumns Id
        {
            get
            {
                return new UserSettingColumns("Id");
            }
        }
        public UserSettingColumns Uuid
        {
            get
            {
                return new UserSettingColumns("Uuid");
            }
        }
        public UserSettingColumns Cuid
        {
            get
            {
                return new UserSettingColumns("Cuid");
            }
        }
        public UserSettingColumns Email
        {
            get
            {
                return new UserSettingColumns("Email");
            }
        }
        public UserSettingColumns Name
        {
            get
            {
                return new UserSettingColumns("Name");
            }
        }
        public UserSettingColumns Value
        {
            get
            {
                return new UserSettingColumns("Value");
            }
        }
        public UserSettingColumns CreatedBy
        {
            get
            {
                return new UserSettingColumns("CreatedBy");
            }
        }
        public UserSettingColumns ModifiedBy
        {
            get
            {
                return new UserSettingColumns("ModifiedBy");
            }
        }
        public UserSettingColumns Modified
        {
            get
            {
                return new UserSettingColumns("Modified");
            }
        }
        public UserSettingColumns Deleted
        {
            get
            {
                return new UserSettingColumns("Deleted");
            }
        }
        public UserSettingColumns Created
        {
            get
            {
                return new UserSettingColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UserSetting);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}