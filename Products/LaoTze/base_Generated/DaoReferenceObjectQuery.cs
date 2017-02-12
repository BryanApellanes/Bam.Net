/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class DaoReferenceObjectQuery: Query<DaoReferenceObjectColumns, DaoReferenceObject>
    { 
		public DaoReferenceObjectQuery(){}
		public DaoReferenceObjectQuery(WhereDelegate<DaoReferenceObjectColumns> where, OrderBy<DaoReferenceObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DaoReferenceObjectQuery(Func<DaoReferenceObjectColumns, QueryFilter<DaoReferenceObjectColumns>> where, OrderBy<DaoReferenceObjectColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DaoReferenceObjectQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DaoReferenceObjectQuery Where(WhereDelegate<DaoReferenceObjectColumns> where)
        {
            return Where(where, null, null);
        }

        public static DaoReferenceObjectQuery Where(WhereDelegate<DaoReferenceObjectColumns> where, OrderBy<DaoReferenceObjectColumns> orderBy = null, Database db = null)
        {
            return new DaoReferenceObjectQuery(where, orderBy, db);
        }

		public DaoReferenceObjectCollection Execute()
		{
			return new DaoReferenceObjectCollection(this, true);
		}
    }
}