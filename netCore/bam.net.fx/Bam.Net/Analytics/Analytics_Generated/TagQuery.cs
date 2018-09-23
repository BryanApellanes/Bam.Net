/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class TagQuery: Query<TagColumns, Tag>
    { 
		public TagQuery(){}
		public TagQuery(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public TagQuery(Func<TagColumns, QueryFilter<TagColumns>> where, OrderBy<TagColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public TagQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static TagQuery Where(WhereDelegate<TagColumns> where)
        {
            return Where(where, null, null);
        }

        public static TagQuery Where(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy = null, Database db = null)
        {
            return new TagQuery(where, orderBy, db);
        }

		public TagCollection Execute()
		{
			return new TagCollection(this, true);
		}
    }
}