/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class SourceNameQuery: Query<SourceNameColumns, SourceName>
    { 
		public SourceNameQuery(){}
		public SourceNameQuery(WhereDelegate<SourceNameColumns> where, OrderBy<SourceNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SourceNameQuery(Func<SourceNameColumns, QueryFilter<SourceNameColumns>> where, OrderBy<SourceNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SourceNameQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SourceNameQuery Where(WhereDelegate<SourceNameColumns> where)
        {
            return Where(where, null, null);
        }

        public static SourceNameQuery Where(WhereDelegate<SourceNameColumns> where, OrderBy<SourceNameColumns> orderBy = null, Database db = null)
        {
            return new SourceNameQuery(where, orderBy, db);
        }

		public SourceNameCollection Execute()
		{
			return new SourceNameCollection(this, true);
		}
    }
}