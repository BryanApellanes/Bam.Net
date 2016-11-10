/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ApplicationInstanceQuery: Query<ApplicationInstanceColumns, ApplicationInstance>
    { 
		public ApplicationInstanceQuery(){}
		public ApplicationInstanceQuery(WhereDelegate<ApplicationInstanceColumns> where, OrderBy<ApplicationInstanceColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ApplicationInstanceQuery(Func<ApplicationInstanceColumns, QueryFilter<ApplicationInstanceColumns>> where, OrderBy<ApplicationInstanceColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ApplicationInstanceQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ApplicationInstanceQuery Where(WhereDelegate<ApplicationInstanceColumns> where)
        {
            return Where(where, null, null);
        }

        public static ApplicationInstanceQuery Where(WhereDelegate<ApplicationInstanceColumns> where, OrderBy<ApplicationInstanceColumns> orderBy = null, Database db = null)
        {
            return new ApplicationInstanceQuery(where, orderBy, db);
        }

		public ApplicationInstanceCollection Execute()
		{
			return new ApplicationInstanceCollection(this, true);
		}
    }
}