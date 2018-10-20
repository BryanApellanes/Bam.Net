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
    public class WebHookSubscriberPagedQuery: PagedQuery<WebHookSubscriberColumns, WebHookSubscriber>
    { 
		public WebHookSubscriberPagedQuery(WebHookSubscriberColumns orderByColumn, WebHookSubscriberQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}