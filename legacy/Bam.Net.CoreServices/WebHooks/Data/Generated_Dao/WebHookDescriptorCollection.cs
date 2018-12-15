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
    public class WebHookDescriptorCollection: DaoCollection<WebHookDescriptorColumns, WebHookDescriptor>
    { 
		public WebHookDescriptorCollection(){}
		public WebHookDescriptorCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public WebHookDescriptorCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WebHookDescriptorCollection(Query<WebHookDescriptorColumns, WebHookDescriptor> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WebHookDescriptorCollection(Database db, Query<WebHookDescriptorColumns, WebHookDescriptor> q, bool load) : base(db, q, load) { }
		public WebHookDescriptorCollection(Query<WebHookDescriptorColumns, WebHookDescriptor> q, bool load) : base(q, load) { }
    }
}