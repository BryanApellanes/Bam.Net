using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Encryption
{
    public class VaultKeyColumns: QueryFilter<VaultKeyColumns>, IFilterToken
    {
        public VaultKeyColumns() { }
        public VaultKeyColumns(string columnName)
            : base(columnName)
        { }
		
		public VaultKeyColumns KeyColumn
		{
			get
			{
				return new VaultKeyColumns("Id");
			}
		}	

				
        public VaultKeyColumns Id
        {
            get
            {
                return new VaultKeyColumns("Id");
            }
        }
        public VaultKeyColumns Uuid
        {
            get
            {
                return new VaultKeyColumns("Uuid");
            }
        }
        public VaultKeyColumns Cuid
        {
            get
            {
                return new VaultKeyColumns("Cuid");
            }
        }
        public VaultKeyColumns RsaKey
        {
            get
            {
                return new VaultKeyColumns("RsaKey");
            }
        }
        public VaultKeyColumns Password
        {
            get
            {
                return new VaultKeyColumns("Password");
            }
        }

        public VaultKeyColumns VaultId
        {
            get
            {
                return new VaultKeyColumns("VaultId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(VaultKey);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}