/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Instructions
{
    public class SectionQuery: Query<SectionColumns, Section>
    { 
		public SectionQuery(){}
		public SectionQuery(WhereDelegate<SectionColumns> where, OrderBy<SectionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SectionQuery(Func<SectionColumns, QueryFilter<SectionColumns>> where, OrderBy<SectionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SectionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SectionQuery Where(WhereDelegate<SectionColumns> where)
        {
            return Where(where, null, null);
        }

        public static SectionQuery Where(WhereDelegate<SectionColumns> where, OrderBy<SectionColumns> orderBy = null, Database db = null)
        {
            return new SectionQuery(where, orderBy, db);
        }

		public SectionCollection Execute()
		{
			return new SectionCollection(this, true);
		}
    }
}