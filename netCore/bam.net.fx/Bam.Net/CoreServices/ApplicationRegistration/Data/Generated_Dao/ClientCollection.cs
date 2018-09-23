/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ClientCollection: DaoCollection<ClientColumns, Client>
    { 
		public ClientCollection(){}
		public ClientCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ClientCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ClientCollection(Query<ClientColumns, Client> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ClientCollection(Database db, Query<ClientColumns, Client> q, bool load) : base(db, q, load) { }
		public ClientCollection(Query<ClientColumns, Client> q, bool load) : base(q, load) { }
    }
}