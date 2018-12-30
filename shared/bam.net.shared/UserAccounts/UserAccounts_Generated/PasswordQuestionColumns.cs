using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class PasswordQuestionColumns: QueryFilter<PasswordQuestionColumns>, IFilterToken
    {
        public PasswordQuestionColumns() { }
        public PasswordQuestionColumns(string columnName)
            : base(columnName)
        { }
		
		public PasswordQuestionColumns KeyColumn
		{
			get
			{
				return new PasswordQuestionColumns("Id");
			}
		}	

				
        public PasswordQuestionColumns Id
        {
            get
            {
                return new PasswordQuestionColumns("Id");
            }
        }
        public PasswordQuestionColumns Uuid
        {
            get
            {
                return new PasswordQuestionColumns("Uuid");
            }
        }
        public PasswordQuestionColumns Cuid
        {
            get
            {
                return new PasswordQuestionColumns("Cuid");
            }
        }
        public PasswordQuestionColumns Value
        {
            get
            {
                return new PasswordQuestionColumns("Value");
            }
        }
        public PasswordQuestionColumns Answer
        {
            get
            {
                return new PasswordQuestionColumns("Answer");
            }
        }

        public PasswordQuestionColumns UserId
        {
            get
            {
                return new PasswordQuestionColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PasswordQuestion);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}