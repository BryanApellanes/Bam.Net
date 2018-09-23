/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Encryption
{
    public class VaultItemCollection: DaoCollection<VaultItemColumns, VaultItem>
    { 
		public VaultItemCollection(){}
		public VaultItemCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public VaultItemCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public VaultItemCollection(Query<VaultItemColumns, VaultItem> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public VaultItemCollection(Database db, Query<VaultItemColumns, VaultItem> q, bool load) : base(db, q, load) { }
		public VaultItemCollection(Query<VaultItemColumns, VaultItem> q, bool load) : base(q, load) { }
    }
}