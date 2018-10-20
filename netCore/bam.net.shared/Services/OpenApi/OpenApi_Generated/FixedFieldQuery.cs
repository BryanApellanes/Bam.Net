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
    public class FixedFieldQuery: Query<FixedFieldColumns, FixedField>
    { 
		public FixedFieldQuery(){}
		public FixedFieldQuery(WhereDelegate<FixedFieldColumns> where, OrderBy<FixedFieldColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public FixedFieldQuery(Func<FixedFieldColumns, QueryFilter<FixedFieldColumns>> where, OrderBy<FixedFieldColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public FixedFieldQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static FixedFieldQuery Where(WhereDelegate<FixedFieldColumns> where)
        {
            return Where(where, null, null);
        }

        public static FixedFieldQuery Where(WhereDelegate<FixedFieldColumns> where, OrderBy<FixedFieldColumns> orderBy = null, Database db = null)
        {
            return new FixedFieldQuery(where, orderBy, db);
        }

		public FixedFieldCollection Execute()
		{
			return new FixedFieldCollection(this, true);
		}
    }
}