using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class SecureSessionColumns: QueryFilter<SecureSessionColumns>, IFilterToken
    {
        public SecureSessionColumns() { }
        public SecureSessionColumns(string columnName)
            : base(columnName)
        { }
		
		public SecureSessionColumns KeyColumn
		{
			get
			{
				return new SecureSessionColumns("Id");
			}
		}	

				
        public SecureSessionColumns Id
        {
            get
            {
                return new SecureSessionColumns("Id");
            }
        }
        public SecureSessionColumns Uuid
        {
            get
            {
                return new SecureSessionColumns("Uuid");
            }
        }
        public SecureSessionColumns Cuid
        {
            get
            {
                return new SecureSessionColumns("Cuid");
            }
        }
        public SecureSessionColumns Identifier
        {
            get
            {
                return new SecureSessionColumns("Identifier");
            }
        }
        public SecureSessionColumns AsymmetricKey
        {
            get
            {
                return new SecureSessionColumns("AsymmetricKey");
            }
        }
        public SecureSessionColumns SymmetricKey
        {
            get
            {
                return new SecureSessionColumns("SymmetricKey");
            }
        }
        public SecureSessionColumns SymmetricIV
        {
            get
            {
                return new SecureSessionColumns("SymmetricIV");
            }
        }
        public SecureSessionColumns CreationDate
        {
            get
            {
                return new SecureSessionColumns("CreationDate");
            }
        }
        public SecureSessionColumns TimeOffset
        {
            get
            {
                return new SecureSessionColumns("TimeOffset");
            }
        }
        public SecureSessionColumns LastActivity
        {
            get
            {
                return new SecureSessionColumns("LastActivity");
            }
        }
        public SecureSessionColumns IsActive
        {
            get
            {
                return new SecureSessionColumns("IsActive");
            }
        }

        public SecureSessionColumns ApplicationId
        {
            get
            {
                return new SecureSessionColumns("ApplicationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(SecureSession);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}