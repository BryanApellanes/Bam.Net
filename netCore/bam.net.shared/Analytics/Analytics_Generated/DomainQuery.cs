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
    public class DomainQuery: Query<DomainColumns, Domain>
    { 
		public DomainQuery(){}
		public DomainQuery(WhereDelegate<DomainColumns> where, OrderBy<DomainColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DomainQuery(Func<DomainColumns, QueryFilter<DomainColumns>> where, OrderBy<DomainColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DomainQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DomainQuery Where(WhereDelegate<DomainColumns> where)
        {
            return Where(where, null, null);
        }

        public static DomainQuery Where(WhereDelegate<DomainColumns> where, OrderBy<DomainColumns> orderBy = null, Database db = null)
        {
            return new DomainQuery(where, orderBy, db);
        }

		public DomainCollection Execute()
		{
			return new DomainCollection(this, true);
		}
    }
}