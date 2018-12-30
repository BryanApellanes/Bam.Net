/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Translation
{
    public class LanguageQuery: Query<LanguageColumns, Language>
    { 
		public LanguageQuery(){}
		public LanguageQuery(WhereDelegate<LanguageColumns> where, OrderBy<LanguageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public LanguageQuery(Func<LanguageColumns, QueryFilter<LanguageColumns>> where, OrderBy<LanguageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public LanguageQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static LanguageQuery Where(WhereDelegate<LanguageColumns> where)
        {
            return Where(where, null, null);
        }

        public static LanguageQuery Where(WhereDelegate<LanguageColumns> where, OrderBy<LanguageColumns> orderBy = null, Database db = null)
        {
            return new LanguageQuery(where, orderBy, db);
        }

		public LanguageCollection Execute()
		{
			return new LanguageCollection(this, true);
		}
    }
}