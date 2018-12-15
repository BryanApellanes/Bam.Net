/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Encryption
{
    public class VaultQuery: Query<VaultColumns, Vault>
    { 
		public VaultQuery(){}
		public VaultQuery(WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public VaultQuery(Func<VaultColumns, QueryFilter<VaultColumns>> where, OrderBy<VaultColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public VaultQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static VaultQuery Where(WhereDelegate<VaultColumns> where)
        {
            return Where(where, null, null);
        }

        public static VaultQuery Where(WhereDelegate<VaultColumns> where, OrderBy<VaultColumns> orderBy = null, Database db = null)
        {
            return new VaultQuery(where, orderBy, db);
        }

		public VaultCollection Execute()
		{
			return new VaultCollection(this, true);
		}
    }
}