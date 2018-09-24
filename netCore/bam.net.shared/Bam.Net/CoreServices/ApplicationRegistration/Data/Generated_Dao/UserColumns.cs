using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class UserColumns: QueryFilter<UserColumns>, IFilterToken
    {
        public UserColumns() { }
        public UserColumns(string columnName)
            : base(columnName)
        { }
		
		public UserColumns KeyColumn
		{
			get
			{
				return new UserColumns("Id");
			}
		}	

				
        public UserColumns Id
        {
            get
            {
                return new UserColumns("Id");
            }
        }
        public UserColumns Uuid
        {
            get
            {
                return new UserColumns("Uuid");
            }
        }
        public UserColumns Cuid
        {
            get
            {
                return new UserColumns("Cuid");
            }
        }
        public UserColumns Email
        {
            get
            {
                return new UserColumns("Email");
            }
        }
        public UserColumns UserName
        {
            get
            {
                return new UserColumns("UserName");
            }
        }
        public UserColumns CreatedBy
        {
            get
            {
                return new UserColumns("CreatedBy");
            }
        }
        public UserColumns ModifiedBy
        {
            get
            {
                return new UserColumns("ModifiedBy");
            }
        }
        public UserColumns Modified
        {
            get
            {
                return new UserColumns("Modified");
            }
        }
        public UserColumns Deleted
        {
            get
            {
                return new UserColumns("Deleted");
            }
        }
        public UserColumns Created
        {
            get
            {
                return new UserColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(User);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}