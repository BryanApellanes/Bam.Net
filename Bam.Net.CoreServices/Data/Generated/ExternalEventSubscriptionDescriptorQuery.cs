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
    public class ExternalEventSubscriptionDescriptorQuery: Query<ExternalEventSubscriptionDescriptorColumns, ExternalEventSubscriptionDescriptor>
    { 
		public ExternalEventSubscriptionDescriptorQuery(){}
		public ExternalEventSubscriptionDescriptorQuery(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, OrderBy<ExternalEventSubscriptionDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ExternalEventSubscriptionDescriptorQuery(Func<ExternalEventSubscriptionDescriptorColumns, QueryFilter<ExternalEventSubscriptionDescriptorColumns>> where, OrderBy<ExternalEventSubscriptionDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ExternalEventSubscriptionDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ExternalEventSubscriptionDescriptorQuery Where(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static ExternalEventSubscriptionDescriptorQuery Where(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, OrderBy<ExternalEventSubscriptionDescriptorColumns> orderBy = null, Database db = null)
        {
            return new ExternalEventSubscriptionDescriptorQuery(where, orderBy, db);
        }

		public ExternalEventSubscriptionDescriptorCollection Execute()
		{
			return new ExternalEventSubscriptionDescriptorCollection(this, true);
		}
    }
}