using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class UserIdentifierColumns: QueryFilter<UserIdentifierColumns>, IFilterToken
    {
        public UserIdentifierColumns() { }
        public UserIdentifierColumns(string columnName)
            : base(columnName)
        { }
		
		public UserIdentifierColumns KeyColumn
		{
			get
			{
				return new UserIdentifierColumns("Id");
			}
		}	

				
        public UserIdentifierColumns Id
        {
            get
            {
                return new UserIdentifierColumns("Id");
            }
        }
        public UserIdentifierColumns Uuid
        {
            get
            {
                return new UserIdentifierColumns("Uuid");
            }
        }
        public UserIdentifierColumns Cuid
        {
            get
            {
                return new UserIdentifierColumns("Cuid");
            }
        }
        public UserIdentifierColumns Value
        {
            get
            {
                return new UserIdentifierColumns("Value");
            }
        }
        public UserIdentifierColumns Name
        {
            get
            {
                return new UserIdentifierColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UserIdentifier);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}