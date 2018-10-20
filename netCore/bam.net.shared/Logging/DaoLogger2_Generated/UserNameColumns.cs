using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class UserNameColumns: QueryFilter<UserNameColumns>, IFilterToken
    {
        public UserNameColumns() { }
        public UserNameColumns(string columnName)
            : base(columnName)
        { }
		
		public UserNameColumns KeyColumn
		{
			get
			{
				return new UserNameColumns("Id");
			}
		}	

				
        public UserNameColumns Id
        {
            get
            {
                return new UserNameColumns("Id");
            }
        }
        public UserNameColumns Uuid
        {
            get
            {
                return new UserNameColumns("Uuid");
            }
        }
        public UserNameColumns Cuid
        {
            get
            {
                return new UserNameColumns("Cuid");
            }
        }
        public UserNameColumns Value
        {
            get
            {
                return new UserNameColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UserName);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}