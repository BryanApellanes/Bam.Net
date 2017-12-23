/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class SubscriptionQuery: Query<SubscriptionColumns, Subscription>
    { 
		public SubscriptionQuery(){}
		public SubscriptionQuery(WhereDelegate<SubscriptionColumns> where, OrderBy<SubscriptionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SubscriptionQuery(Func<SubscriptionColumns, QueryFilter<SubscriptionColumns>> where, OrderBy<SubscriptionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SubscriptionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SubscriptionQuery Where(WhereDelegate<SubscriptionColumns> where)
        {
            return Where(where, null, null);
        }

        public static SubscriptionQuery Where(WhereDelegate<SubscriptionColumns> where, OrderBy<SubscriptionColumns> orderBy = null, Database db = null)
        {
            return new SubscriptionQuery(where, orderBy, db);
        }

		public SubscriptionCollection Execute()
		{
			return new SubscriptionCollection(this, true);
		}
    }
}