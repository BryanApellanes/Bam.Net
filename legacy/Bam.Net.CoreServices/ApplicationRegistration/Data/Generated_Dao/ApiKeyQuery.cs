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
    public class ApiKeyQuery: Query<ApiKeyColumns, ApiKey>
    { 
		public ApiKeyQuery(){}
		public ApiKeyQuery(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ApiKeyQuery(Func<ApiKeyColumns, QueryFilter<ApiKeyColumns>> where, OrderBy<ApiKeyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ApiKeyQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ApiKeyQuery Where(WhereDelegate<ApiKeyColumns> where)
        {
            return Where(where, null, null);
        }

        public static ApiKeyQuery Where(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null, Database db = null)
        {
            return new ApiKeyQuery(where, orderBy, db);
        }

		public ApiKeyCollection Execute()
		{
			return new ApiKeyCollection(this, true);
		}
    }
}