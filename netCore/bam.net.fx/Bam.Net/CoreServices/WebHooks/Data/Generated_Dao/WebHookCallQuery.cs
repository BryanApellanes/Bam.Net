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
    public class WebHookCallQuery: Query<WebHookCallColumns, WebHookCall>
    { 
		public WebHookCallQuery(){}
		public WebHookCallQuery(WhereDelegate<WebHookCallColumns> where, OrderBy<WebHookCallColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public WebHookCallQuery(Func<WebHookCallColumns, QueryFilter<WebHookCallColumns>> where, OrderBy<WebHookCallColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public WebHookCallQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static WebHookCallQuery Where(WhereDelegate<WebHookCallColumns> where)
        {
            return Where(where, null, null);
        }

        public static WebHookCallQuery Where(WhereDelegate<WebHookCallColumns> where, OrderBy<WebHookCallColumns> orderBy = null, Database db = null)
        {
            return new WebHookCallQuery(where, orderBy, db);
        }

		public WebHookCallCollection Execute()
		{
			return new WebHookCallCollection(this, true);
		}
    }
}