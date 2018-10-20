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
    public class SubscriptionCollection: DaoCollection<SubscriptionColumns, Subscription>
    { 
		public SubscriptionCollection(){}
		public SubscriptionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SubscriptionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SubscriptionCollection(Query<SubscriptionColumns, Subscription> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SubscriptionCollection(Database db, Query<SubscriptionColumns, Subscription> q, bool load) : base(db, q, load) { }
		public SubscriptionCollection(Query<SubscriptionColumns, Subscription> q, bool load) : base(q, load) { }
    }
}