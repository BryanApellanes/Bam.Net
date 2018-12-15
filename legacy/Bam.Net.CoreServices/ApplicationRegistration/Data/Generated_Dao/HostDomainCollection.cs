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
    public class HostDomainCollection: DaoCollection<HostDomainColumns, HostDomain>
    { 
		public HostDomainCollection(){}
		public HostDomainCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public HostDomainCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HostDomainCollection(Query<HostDomainColumns, HostDomain> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HostDomainCollection(Database db, Query<HostDomainColumns, HostDomain> q, bool load) : base(db, q, load) { }
		public HostDomainCollection(Query<HostDomainColumns, HostDomain> q, bool load) : base(q, load) { }
    }
}