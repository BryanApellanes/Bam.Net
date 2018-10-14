/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ActiveApiKeyIndexQuery: Query<ActiveApiKeyIndexColumns, ActiveApiKeyIndex>
    { 
		public ActiveApiKeyIndexQuery(){}
		public ActiveApiKeyIndexQuery(WhereDelegate<ActiveApiKeyIndexColumns> where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ActiveApiKeyIndexQuery(Func<ActiveApiKeyIndexColumns, QueryFilter<ActiveApiKeyIndexColumns>> where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ActiveApiKeyIndexQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ActiveApiKeyIndexQuery Where(WhereDelegate<ActiveApiKeyIndexColumns> where)
        {
            return Where(where, null, null);
        }

        public static ActiveApiKeyIndexQuery Where(WhereDelegate<ActiveApiKeyIndexColumns> where, OrderBy<ActiveApiKeyIndexColumns> orderBy = null, Database db = null)
        {
            return new ActiveApiKeyIndexQuery(where, orderBy, db);
        }

		public ActiveApiKeyIndexCollection Execute()
		{
			return new ActiveApiKeyIndexCollection(this, true);
		}
    }
}