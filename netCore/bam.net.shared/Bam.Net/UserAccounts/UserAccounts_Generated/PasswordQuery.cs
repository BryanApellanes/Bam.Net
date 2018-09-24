/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class PasswordQuery: Query<PasswordColumns, Password>
    { 
		public PasswordQuery(){}
		public PasswordQuery(WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PasswordQuery(Func<PasswordColumns, QueryFilter<PasswordColumns>> where, OrderBy<PasswordColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PasswordQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PasswordQuery Where(WhereDelegate<PasswordColumns> where)
        {
            return Where(where, null, null);
        }

        public static PasswordQuery Where(WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy = null, Database db = null)
        {
            return new PasswordQuery(where, orderBy, db);
        }

		public PasswordCollection Execute()
		{
			return new PasswordCollection(this, true);
		}
    }
}