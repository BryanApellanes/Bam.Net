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
    public class PasswordQuestionQuery: Query<PasswordQuestionColumns, PasswordQuestion>
    { 
		public PasswordQuestionQuery(){}
		public PasswordQuestionQuery(WhereDelegate<PasswordQuestionColumns> where, OrderBy<PasswordQuestionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PasswordQuestionQuery(Func<PasswordQuestionColumns, QueryFilter<PasswordQuestionColumns>> where, OrderBy<PasswordQuestionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PasswordQuestionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PasswordQuestionQuery Where(WhereDelegate<PasswordQuestionColumns> where)
        {
            return Where(where, null, null);
        }

        public static PasswordQuestionQuery Where(WhereDelegate<PasswordQuestionColumns> where, OrderBy<PasswordQuestionColumns> orderBy = null, Database db = null)
        {
            return new PasswordQuestionQuery(where, orderBy, db);
        }

		public PasswordQuestionCollection Execute()
		{
			return new PasswordQuestionCollection(this, true);
		}
    }
}