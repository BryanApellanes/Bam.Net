/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics.EnglishDictionary
{
    public class WordQuery: Query<WordColumns, Word>
    { 
		public WordQuery(){}
		public WordQuery(WhereDelegate<WordColumns> where, OrderBy<WordColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public WordQuery(Func<WordColumns, QueryFilter<WordColumns>> where, OrderBy<WordColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public WordQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static WordQuery Where(WhereDelegate<WordColumns> where)
        {
            return Where(where, null, null);
        }

        public static WordQuery Where(WhereDelegate<WordColumns> where, OrderBy<WordColumns> orderBy = null, Database db = null)
        {
            return new WordQuery(where, orderBy, db);
        }

		public WordCollection Execute()
		{
			return new WordCollection(this, true);
		}
    }
}