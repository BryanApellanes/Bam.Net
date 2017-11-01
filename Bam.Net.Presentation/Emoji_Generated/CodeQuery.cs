/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Presentation.Unicode
{
    public class CodeQuery: Query<CodeColumns, Code>
    { 
		public CodeQuery(){}
		public CodeQuery(WhereDelegate<CodeColumns> where, OrderBy<CodeColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public CodeQuery(Func<CodeColumns, QueryFilter<CodeColumns>> where, OrderBy<CodeColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public CodeQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static CodeQuery Where(WhereDelegate<CodeColumns> where)
        {
            return Where(where, null, null);
        }

        public static CodeQuery Where(WhereDelegate<CodeColumns> where, OrderBy<CodeColumns> orderBy = null, Database db = null)
        {
            return new CodeQuery(where, orderBy, db);
        }

		public CodeCollection Execute()
		{
			return new CodeCollection(this, true);
		}
    }
}