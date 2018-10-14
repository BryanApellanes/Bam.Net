/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class HostDomainQuery: Query<HostDomainColumns, HostDomain>
    { 
		public HostDomainQuery(){}
		public HostDomainQuery(WhereDelegate<HostDomainColumns> where, OrderBy<HostDomainColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public HostDomainQuery(Func<HostDomainColumns, QueryFilter<HostDomainColumns>> where, OrderBy<HostDomainColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public HostDomainQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static HostDomainQuery Where(WhereDelegate<HostDomainColumns> where)
        {
            return Where(where, null, null);
        }

        public static HostDomainQuery Where(WhereDelegate<HostDomainColumns> where, OrderBy<HostDomainColumns> orderBy = null, Database db = null)
        {
            return new HostDomainQuery(where, orderBy, db);
        }

		public HostDomainCollection Execute()
		{
			return new HostDomainCollection(this, true);
		}
    }
}