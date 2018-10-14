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
    public class WebHookSubscriberQuery: Query<WebHookSubscriberColumns, WebHookSubscriber>
    { 
		public WebHookSubscriberQuery(){}
		public WebHookSubscriberQuery(WhereDelegate<WebHookSubscriberColumns> where, OrderBy<WebHookSubscriberColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public WebHookSubscriberQuery(Func<WebHookSubscriberColumns, QueryFilter<WebHookSubscriberColumns>> where, OrderBy<WebHookSubscriberColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public WebHookSubscriberQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static WebHookSubscriberQuery Where(WhereDelegate<WebHookSubscriberColumns> where)
        {
            return Where(where, null, null);
        }

        public static WebHookSubscriberQuery Where(WhereDelegate<WebHookSubscriberColumns> where, OrderBy<WebHookSubscriberColumns> orderBy = null, Database db = null)
        {
            return new WebHookSubscriberQuery(where, orderBy, db);
        }

		public WebHookSubscriberCollection Execute()
		{
			return new WebHookSubscriberCollection(this, true);
		}
    }
}