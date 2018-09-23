using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class PasswordColumns: QueryFilter<PasswordColumns>, IFilterToken
    {
        public PasswordColumns() { }
        public PasswordColumns(string columnName)
            : base(columnName)
        { }
		
		public PasswordColumns KeyColumn
		{
			get
			{
				return new PasswordColumns("Id");
			}
		}	

				
        public PasswordColumns Id
        {
            get
            {
                return new PasswordColumns("Id");
            }
        }
        public PasswordColumns Uuid
        {
            get
            {
                return new PasswordColumns("Uuid");
            }
        }
        public PasswordColumns Cuid
        {
            get
            {
                return new PasswordColumns("Cuid");
            }
        }
        public PasswordColumns Value
        {
            get
            {
                return new PasswordColumns("Value");
            }
        }

        public PasswordColumns UserId
        {
            get
            {
                return new PasswordColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Password);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}