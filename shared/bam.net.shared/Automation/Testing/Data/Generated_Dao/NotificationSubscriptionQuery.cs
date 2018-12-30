/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.Testing.Data.Dao
{
    public class NotificationSubscriptionQuery: Query<NotificationSubscriptionColumns, NotificationSubscription>
    { 
		public NotificationSubscriptionQuery(){}
		public NotificationSubscriptionQuery(WhereDelegate<NotificationSubscriptionColumns> where, OrderBy<NotificationSubscriptionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public NotificationSubscriptionQuery(Func<NotificationSubscriptionColumns, QueryFilter<NotificationSubscriptionColumns>> where, OrderBy<NotificationSubscriptionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public NotificationSubscriptionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static NotificationSubscriptionQuery Where(WhereDelegate<NotificationSubscriptionColumns> where)
        {
            return Where(where, null, null);
        }

        public static NotificationSubscriptionQuery Where(WhereDelegate<NotificationSubscriptionColumns> where, OrderBy<NotificationSubscriptionColumns> orderBy = null, Database db = null)
        {
            return new NotificationSubscriptionQuery(where, orderBy, db);
        }

		public NotificationSubscriptionCollection Execute()
		{
			return new NotificationSubscriptionCollection(this, true);
		}
    }
}