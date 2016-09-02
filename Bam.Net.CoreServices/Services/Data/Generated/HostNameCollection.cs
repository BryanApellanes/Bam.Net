/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class HostNameCollection: DaoCollection<HostNameColumns, HostName>
    { 
		public HostNameCollection(){}
		public HostNameCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public HostNameCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HostNameCollection(Query<HostNameColumns, HostName> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HostNameCollection(Database db, Query<HostNameColumns, HostName> q, bool load) : base(db, q, load) { }
		public HostNameCollection(Query<HostNameColumns, HostName> q, bool load) : base(q, load) { }
    }
}