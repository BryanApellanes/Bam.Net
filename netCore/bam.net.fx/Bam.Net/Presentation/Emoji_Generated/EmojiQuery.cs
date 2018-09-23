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
    public class EmojiQuery: Query<EmojiColumns, Emoji>
    { 
		public EmojiQuery(){}
		public EmojiQuery(WhereDelegate<EmojiColumns> where, OrderBy<EmojiColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public EmojiQuery(Func<EmojiColumns, QueryFilter<EmojiColumns>> where, OrderBy<EmojiColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public EmojiQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static EmojiQuery Where(WhereDelegate<EmojiColumns> where)
        {
            return Where(where, null, null);
        }

        public static EmojiQuery Where(WhereDelegate<EmojiColumns> where, OrderBy<EmojiColumns> orderBy = null, Database db = null)
        {
            return new EmojiQuery(where, orderBy, db);
        }

		public EmojiCollection Execute()
		{
			return new EmojiCollection(this, true);
		}
    }
}