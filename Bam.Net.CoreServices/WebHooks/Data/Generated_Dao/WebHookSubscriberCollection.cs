/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.WebHooks.Data.Dao
{
    public class WebHookSubscriberCollection: DaoCollection<WebHookSubscriberColumns, WebHookSubscriber>
    { 
		public WebHookSubscriberCollection(){}
		public WebHookSubscriberCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public WebHookSubscriberCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WebHookSubscriberCollection(Query<WebHookSubscriberColumns, WebHookSubscriber> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WebHookSubscriberCollection(Database db, Query<WebHookSubscriberColumns, WebHookSubscriber> q, bool load) : base(db, q, load) { }
		public WebHookSubscriberCollection(Query<WebHookSubscriberColumns, WebHookSubscriber> q, bool load) : base(q, load) { }
    }
}