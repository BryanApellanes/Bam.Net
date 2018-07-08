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
    public class WebHookCallCollection: DaoCollection<WebHookCallColumns, WebHookCall>
    { 
		public WebHookCallCollection(){}
		public WebHookCallCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public WebHookCallCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WebHookCallCollection(Query<WebHookCallColumns, WebHookCall> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WebHookCallCollection(Database db, Query<WebHookCallColumns, WebHookCall> q, bool load) : base(db, q, load) { }
		public WebHookCallCollection(Query<WebHookCallColumns, WebHookCall> q, bool load) : base(q, load) { }
    }
}