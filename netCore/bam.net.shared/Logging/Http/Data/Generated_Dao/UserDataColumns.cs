using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class UserDataColumns: QueryFilter<UserDataColumns>, IFilterToken
    {
        public UserDataColumns() { }
        public UserDataColumns(string columnName)
            : base(columnName)
        { }
		
		public UserDataColumns KeyColumn
		{
			get
			{
				return new UserDataColumns("Id");
			}
		}	

				
        public UserDataColumns Id
        {
            get
            {
                return new UserDataColumns("Id");
            }
        }
        public UserDataColumns Uuid
        {
            get
            {
                return new UserDataColumns("Uuid");
            }
        }
        public UserDataColumns Cuid
        {
            get
            {
                return new UserDataColumns("Cuid");
            }
        }
        public UserDataColumns UserNameHash
        {
            get
            {
                return new UserDataColumns("UserNameHash");
            }
        }
        public UserDataColumns RequestCuid
        {
            get
            {
                return new UserDataColumns("RequestCuid");
            }
        }
        public UserDataColumns Created
        {
            get
            {
                return new UserDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UserData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}