/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class RootDocumentQuery: Query<RootDocumentColumns, RootDocument>
    { 
		public RootDocumentQuery(){}
		public RootDocumentQuery(WhereDelegate<RootDocumentColumns> where, OrderBy<RootDocumentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public RootDocumentQuery(Func<RootDocumentColumns, QueryFilter<RootDocumentColumns>> where, OrderBy<RootDocumentColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public RootDocumentQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static RootDocumentQuery Where(WhereDelegate<RootDocumentColumns> where)
        {
            return Where(where, null, null);
        }

        public static RootDocumentQuery Where(WhereDelegate<RootDocumentColumns> where, OrderBy<RootDocumentColumns> orderBy = null, Database db = null)
        {
            return new RootDocumentQuery(where, orderBy, db);
        }

		public RootDocumentCollection Execute()
		{
			return new RootDocumentCollection(this, true);
		}
    }
}