using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Encryption
{
    public class VaultColumns: QueryFilter<VaultColumns>, IFilterToken
    {
        public VaultColumns() { }
        public VaultColumns(string columnName)
            : base(columnName)
        { }
		
		public VaultColumns KeyColumn
		{
			get
			{
				return new VaultColumns("Id");
			}
		}	

				
        public VaultColumns Id
        {
            get
            {
                return new VaultColumns("Id");
            }
        }
        public VaultColumns Uuid
        {
            get
            {
                return new VaultColumns("Uuid");
            }
        }
        public VaultColumns Cuid
        {
            get
            {
                return new VaultColumns("Cuid");
            }
        }
        public VaultColumns Name
        {
            get
            {
                return new VaultColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Vault);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}