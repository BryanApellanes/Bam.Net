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
    public class VaultItemQuery: Query<VaultItemColumns, VaultItem>
    { 
		public VaultItemQuery(){}
		public VaultItemQuery(WhereDelegate<VaultItemColumns> where, OrderBy<VaultItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public VaultItemQuery(Func<VaultItemColumns, QueryFilter<VaultItemColumns>> where, OrderBy<VaultItemColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public VaultItemQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static VaultItemQuery Where(WhereDelegate<VaultItemColumns> where)
        {
            return Where(where, null, null);
        }

        public static VaultItemQuery Where(WhereDelegate<VaultItemColumns> where, OrderBy<VaultItemColumns> orderBy = null, Database db = null)
        {
            return new VaultItemQuery(where, orderBy, db);
        }

		public VaultItemCollection Execute()
		{
			return new VaultItemCollection(this, true);
		}
    }
}