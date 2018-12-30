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
    public class HostAddressCollection: DaoCollection<HostAddressColumns, HostAddress>
    { 
		public HostAddressCollection(){}
		public HostAddressCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public HostAddressCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HostAddressCollection(Query<HostAddressColumns, HostAddress> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HostAddressCollection(Database db, Query<HostAddressColumns, HostAddress> q, bool load) : base(db, q, load) { }
		public HostAddressCollection(Query<HostAddressColumns, HostAddress> q, bool load) : base(q, load) { }
    }
}