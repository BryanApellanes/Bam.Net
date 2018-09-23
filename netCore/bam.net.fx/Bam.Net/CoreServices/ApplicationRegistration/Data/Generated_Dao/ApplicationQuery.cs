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
    public class ApplicationQuery: Query<ApplicationColumns, Application>
    { 
		public ApplicationQuery(){}
		public ApplicationQuery(WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ApplicationQuery(Func<ApplicationColumns, QueryFilter<ApplicationColumns>> where, OrderBy<ApplicationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ApplicationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ApplicationQuery Where(WhereDelegate<ApplicationColumns> where)
        {
            return Where(where, null, null);
        }

        public static ApplicationQuery Where(WhereDelegate<ApplicationColumns> where, OrderBy<ApplicationColumns> orderBy = null, Database db = null)
        {
            return new ApplicationQuery(where, orderBy, db);
        }

		public ApplicationCollection Execute()
		{
			return new ApplicationCollection(this, true);
		}
    }
}