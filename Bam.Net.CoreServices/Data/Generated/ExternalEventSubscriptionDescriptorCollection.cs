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
    public class ExternalEventSubscriptionDescriptorCollection: DaoCollection<ExternalEventSubscriptionDescriptorColumns, ExternalEventSubscriptionDescriptor>
    { 
		public ExternalEventSubscriptionDescriptorCollection(){}
		public ExternalEventSubscriptionDescriptorCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ExternalEventSubscriptionDescriptorCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ExternalEventSubscriptionDescriptorCollection(Query<ExternalEventSubscriptionDescriptorColumns, ExternalEventSubscriptionDescriptor> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ExternalEventSubscriptionDescriptorCollection(Database db, Query<ExternalEventSubscriptionDescriptorColumns, ExternalEventSubscriptionDescriptor> q, bool load) : base(db, q, load) { }
		public ExternalEventSubscriptionDescriptorCollection(Query<ExternalEventSubscriptionDescriptorColumns, ExternalEventSubscriptionDescriptor> q, bool load) : base(q, load) { }
    }
}