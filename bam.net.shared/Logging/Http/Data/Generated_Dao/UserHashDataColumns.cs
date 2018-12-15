using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class UserHashDataColumns: QueryFilter<UserHashDataColumns>, IFilterToken
    {
        public UserHashDataColumns() { }
        public UserHashDataColumns(string columnName)
            : base(columnName)
        { }
		
		public UserHashDataColumns KeyColumn
		{
			get
			{
				return new UserHashDataColumns("Id");
			}
		}	

				
        public UserHashDataColumns Id
        {
            get
            {
                return new UserHashDataColumns("Id");
            }
        }
        public UserHashDataColumns Uuid
        {
            get
            {
                return new UserHashDataColumns("Uuid");
            }
        }
        public UserHashDataColumns Cuid
        {
            get
            {
                return new UserHashDataColumns("Cuid");
            }
        }
        public UserHashDataColumns UserNameHash
        {
            get
            {
                return new UserHashDataColumns("UserNameHash");
            }
        }
        public UserHashDataColumns UserName
        {
            get
            {
                return new UserHashDataColumns("UserName");
            }
        }
        public UserHashDataColumns Created
        {
            get
            {
                return new UserHashDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UserHashData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}