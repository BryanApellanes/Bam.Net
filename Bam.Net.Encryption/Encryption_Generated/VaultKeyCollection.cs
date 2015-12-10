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
    public class VaultKeyCollection: DaoCollection<VaultKeyColumns, VaultKey>
    { 
		public VaultKeyCollection(){}
		public VaultKeyCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public VaultKeyCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public VaultKeyCollection(Query<VaultKeyColumns, VaultKey> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public VaultKeyCollection(Database db, Query<VaultKeyColumns, VaultKey> q, bool load) : base(db, q, load) { }
		public VaultKeyCollection(Query<VaultKeyColumns, VaultKey> q, bool load) : base(q, load) { }
    }
}