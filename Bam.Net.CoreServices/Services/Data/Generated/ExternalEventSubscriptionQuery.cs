/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ExternalEventSubscriptionQuery: Query<ExternalEventSubscriptionColumns, ExternalEventSubscription>
    { 
		public ExternalEventSubscriptionQuery(){}
		public ExternalEventSubscriptionQuery(WhereDelegate<ExternalEventSubscriptionColumns> where, OrderBy<ExternalEventSubscriptionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ExternalEventSubscriptionQuery(Func<ExternalEventSubscriptionColumns, QueryFilter<ExternalEventSubscriptionColumns>> where, OrderBy<ExternalEventSubscriptionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ExternalEventSubscriptionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ExternalEventSubscriptionQuery Where(WhereDelegate<ExternalEventSubscriptionColumns> where)
        {
            return Where(where, null, null);
        }

        public static ExternalEventSubscriptionQuery Where(WhereDelegate<ExternalEventSubscriptionColumns> where, OrderBy<ExternalEventSubscriptionColumns> orderBy = null, Database db = null)
        {
            return new ExternalEventSubscriptionQuery(where, orderBy, db);
        }

		public ExternalEventSubscriptionCollection Execute()
		{
			return new ExternalEventSubscriptionCollection(this, true);
		}
    }
}