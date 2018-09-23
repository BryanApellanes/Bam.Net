using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class PasswordResetColumns: QueryFilter<PasswordResetColumns>, IFilterToken
    {
        public PasswordResetColumns() { }
        public PasswordResetColumns(string columnName)
            : base(columnName)
        { }
		
		public PasswordResetColumns KeyColumn
		{
			get
			{
				return new PasswordResetColumns("Id");
			}
		}	

				
        public PasswordResetColumns Id
        {
            get
            {
                return new PasswordResetColumns("Id");
            }
        }
        public PasswordResetColumns Uuid
        {
            get
            {
                return new PasswordResetColumns("Uuid");
            }
        }
        public PasswordResetColumns Cuid
        {
            get
            {
                return new PasswordResetColumns("Cuid");
            }
        }
        public PasswordResetColumns Token
        {
            get
            {
                return new PasswordResetColumns("Token");
            }
        }
        public PasswordResetColumns DateTime
        {
            get
            {
                return new PasswordResetColumns("DateTime");
            }
        }
        public PasswordResetColumns WasReset
        {
            get
            {
                return new PasswordResetColumns("WasReset");
            }
        }
        public PasswordResetColumns ExpiresInMinutes
        {
            get
            {
                return new PasswordResetColumns("ExpiresInMinutes");
            }
        }

        public PasswordResetColumns UserId
        {
            get
            {
                return new PasswordResetColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PasswordReset);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}