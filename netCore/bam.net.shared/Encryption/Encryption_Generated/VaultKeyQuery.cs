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
    public class VaultKeyQuery: Query<VaultKeyColumns, VaultKey>
    { 
		public VaultKeyQuery(){}
		public VaultKeyQuery(WhereDelegate<VaultKeyColumns> where, OrderBy<VaultKeyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public VaultKeyQuery(Func<VaultKeyColumns, QueryFilter<VaultKeyColumns>> where, OrderBy<VaultKeyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public VaultKeyQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static VaultKeyQuery Where(WhereDelegate<VaultKeyColumns> where)
        {
            return Where(where, null, null);
        }

        public static VaultKeyQuery Where(WhereDelegate<VaultKeyColumns> where, OrderBy<VaultKeyColumns> orderBy = null, Database db = null)
        {
            return new VaultKeyQuery(where, orderBy, db);
        }

		public VaultKeyCollection Execute()
		{
			return new VaultKeyCollection(this, true);
		}
    }
}