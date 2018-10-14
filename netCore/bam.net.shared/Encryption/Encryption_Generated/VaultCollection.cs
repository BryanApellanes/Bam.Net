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
    public class VaultCollection: DaoCollection<VaultColumns, Vault>
    { 
		public VaultCollection(){}
		public VaultCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public VaultCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public VaultCollection(Query<VaultColumns, Vault> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public VaultCollection(Database db, Query<VaultColumns, Vault> q, bool load) : base(db, q, load) { }
		public VaultCollection(Query<VaultColumns, Vault> q, bool load) : base(q, load) { }
    }
}