using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class LoginCounterColumns: QueryFilter<LoginCounterColumns>, IFilterToken
    {
        public LoginCounterColumns() { }
        public LoginCounterColumns(string columnName)
            : base(columnName)
        { }
		
		public LoginCounterColumns KeyColumn
		{
			get
			{
				return new LoginCounterColumns("Id");
			}
		}	

				
        public LoginCounterColumns Id
        {
            get
            {
                return new LoginCounterColumns("Id");
            }
        }
        public LoginCounterColumns Uuid
        {
            get
            {
                return new LoginCounterColumns("Uuid");
            }
        }
        public LoginCounterColumns Cuid
        {
            get
            {
                return new LoginCounterColumns("Cuid");
            }
        }

        public LoginCounterColumns CounterId
        {
            get
            {
                return new LoginCounterColumns("CounterId");
            }
        }
        public LoginCounterColumns UserIdentifierId
        {
            get
            {
                return new LoginCounterColumns("UserIdentifierId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(LoginCounter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}