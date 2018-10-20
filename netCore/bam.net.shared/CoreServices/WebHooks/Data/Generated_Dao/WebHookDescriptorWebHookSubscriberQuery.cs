/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.WebHooks.Data.Dao
{
    public class WebHookDescriptorWebHookSubscriberQuery: Query<WebHookDescriptorWebHookSubscriberColumns, WebHookDescriptorWebHookSubscriber>
    { 
		public WebHookDescriptorWebHookSubscriberQuery(){}
		public WebHookDescriptorWebHookSubscriberQuery(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public WebHookDescriptorWebHookSubscriberQuery(Func<WebHookDescriptorWebHookSubscriberColumns, QueryFilter<WebHookDescriptorWebHookSubscriberColumns>> where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public WebHookDescriptorWebHookSubscriberQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static WebHookDescriptorWebHookSubscriberQuery Where(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where)
        {
            return Where(where, null, null);
        }

        public static WebHookDescriptorWebHookSubscriberQuery Where(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy = null, Database db = null)
        {
            return new WebHookDescriptorWebHookSubscriberQuery(where, orderBy, db);
        }

		public WebHookDescriptorWebHookSubscriberCollection Execute()
		{
			return new WebHookDescriptorWebHookSubscriberCollection(this, true);
		}
    }
}