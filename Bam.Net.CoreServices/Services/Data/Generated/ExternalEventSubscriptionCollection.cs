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
    public class ExternalEventSubscriptionCollection: DaoCollection<ExternalEventSubscriptionColumns, ExternalEventSubscription>
    { 
		public ExternalEventSubscriptionCollection(){}
		public ExternalEventSubscriptionCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ExternalEventSubscriptionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ExternalEventSubscriptionCollection(Query<ExternalEventSubscriptionColumns, ExternalEventSubscription> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ExternalEventSubscriptionCollection(Database db, Query<ExternalEventSubscriptionColumns, ExternalEventSubscription> q, bool load) : base(db, q, load) { }
		public ExternalEventSubscriptionCollection(Query<ExternalEventSubscriptionColumns, ExternalEventSubscription> q, bool load) : base(q, load) { }
    }
}