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
    public class ClientServerConnectionCollection: DaoCollection<ClientServerConnectionColumns, ClientServerConnection>
    { 
		public ClientServerConnectionCollection(){}
		public ClientServerConnectionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ClientServerConnectionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ClientServerConnectionCollection(Query<ClientServerConnectionColumns, ClientServerConnection> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ClientServerConnectionCollection(Database db, Query<ClientServerConnectionColumns, ClientServerConnection> q, bool load) : base(db, q, load) { }
		public ClientServerConnectionCollection(Query<ClientServerConnectionColumns, ClientServerConnection> q, bool load) : base(q, load) { }
    }
}