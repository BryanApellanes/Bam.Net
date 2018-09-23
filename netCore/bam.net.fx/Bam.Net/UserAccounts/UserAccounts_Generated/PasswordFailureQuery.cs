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
    public class PasswordFailureQuery: Query<PasswordFailureColumns, PasswordFailure>
    { 
		public PasswordFailureQuery(){}
		public PasswordFailureQuery(WhereDelegate<PasswordFailureColumns> where, OrderBy<PasswordFailureColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PasswordFailureQuery(Func<PasswordFailureColumns, QueryFilter<PasswordFailureColumns>> where, OrderBy<PasswordFailureColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PasswordFailureQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PasswordFailureQuery Where(WhereDelegate<PasswordFailureColumns> where)
        {
            return Where(where, null, null);
        }

        public static PasswordFailureQuery Where(WhereDelegate<PasswordFailureColumns> where, OrderBy<PasswordFailureColumns> orderBy = null, Database db = null)
        {
            return new PasswordFailureQuery(where, orderBy, db);
        }

		public PasswordFailureCollection Execute()
		{
			return new PasswordFailureCollection(this, true);
		}
    }
}