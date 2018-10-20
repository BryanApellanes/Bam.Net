/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.Testing.Data.Dao
{
    public class NotificationSubscriptionCollection: DaoCollection<NotificationSubscriptionColumns, NotificationSubscription>
    { 
		public NotificationSubscriptionCollection(){}
		public NotificationSubscriptionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public NotificationSubscriptionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public NotificationSubscriptionCollection(Query<NotificationSubscriptionColumns, NotificationSubscription> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public NotificationSubscriptionCollection(Database db, Query<NotificationSubscriptionColumns, NotificationSubscription> q, bool load) : base(db, q, load) { }
		public NotificationSubscriptionCollection(Query<NotificationSubscriptionColumns, NotificationSubscription> q, bool load) : base(q, load) { }
    }
}