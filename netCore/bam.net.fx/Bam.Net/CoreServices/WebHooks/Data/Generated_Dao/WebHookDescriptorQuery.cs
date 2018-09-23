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
    public class WebHookDescriptorQuery: Query<WebHookDescriptorColumns, WebHookDescriptor>
    { 
		public WebHookDescriptorQuery(){}
		public WebHookDescriptorQuery(WhereDelegate<WebHookDescriptorColumns> where, OrderBy<WebHookDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public WebHookDescriptorQuery(Func<WebHookDescriptorColumns, QueryFilter<WebHookDescriptorColumns>> where, OrderBy<WebHookDescriptorColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public WebHookDescriptorQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static WebHookDescriptorQuery Where(WhereDelegate<WebHookDescriptorColumns> where)
        {
            return Where(where, null, null);
        }

        public static WebHookDescriptorQuery Where(WhereDelegate<WebHookDescriptorColumns> where, OrderBy<WebHookDescriptorColumns> orderBy = null, Database db = null)
        {
            return new WebHookDescriptorQuery(where, orderBy, db);
        }

		public WebHookDescriptorCollection Execute()
		{
			return new WebHookDescriptorCollection(this, true);
		}
    }
}