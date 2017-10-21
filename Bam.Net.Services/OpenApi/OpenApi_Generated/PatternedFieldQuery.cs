/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.OpenApi
{
    public class PatternedFieldQuery: Query<PatternedFieldColumns, PatternedField>
    { 
		public PatternedFieldQuery(){}
		public PatternedFieldQuery(WhereDelegate<PatternedFieldColumns> where, OrderBy<PatternedFieldColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PatternedFieldQuery(Func<PatternedFieldColumns, QueryFilter<PatternedFieldColumns>> where, OrderBy<PatternedFieldColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PatternedFieldQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static PatternedFieldQuery Where(WhereDelegate<PatternedFieldColumns> where)
        {
            return Where(where, null, null);
        }

        public static PatternedFieldQuery Where(WhereDelegate<PatternedFieldColumns> where, OrderBy<PatternedFieldColumns> orderBy = null, Database db = null)
        {
            return new PatternedFieldQuery(where, orderBy, db);
        }

		public PatternedFieldCollection Execute()
		{
			return new PatternedFieldCollection(this, true);
		}
    }
}