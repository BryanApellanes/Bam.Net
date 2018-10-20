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
    public class OtherNameQuery: Query<OtherNameColumns, OtherName>
    { 
		public OtherNameQuery(){}
		public OtherNameQuery(WhereDelegate<OtherNameColumns> where, OrderBy<OtherNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public OtherNameQuery(Func<OtherNameColumns, QueryFilter<OtherNameColumns>> where, OrderBy<OtherNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public OtherNameQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static OtherNameQuery Where(WhereDelegate<OtherNameColumns> where)
        {
            return Where(where, null, null);
        }

        public static OtherNameQuery Where(WhereDelegate<OtherNameColumns> where, OrderBy<OtherNameColumns> orderBy = null, Database db = null)
        {
            return new OtherNameQuery(where, orderBy, db);
        }

		public OtherNameCollection Execute()
		{
			return new OtherNameCollection(this, true);
		}
    }
}