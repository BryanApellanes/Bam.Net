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
    public class HostDomainApplicationQuery: Query<HostDomainApplicationColumns, HostDomainApplication>
    { 
		public HostDomainApplicationQuery(){}
		public HostDomainApplicationQuery(WhereDelegate<HostDomainApplicationColumns> where, OrderBy<HostDomainApplicationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public HostDomainApplicationQuery(Func<HostDomainApplicationColumns, QueryFilter<HostDomainApplicationColumns>> where, OrderBy<HostDomainApplicationColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public HostDomainApplicationQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static HostDomainApplicationQuery Where(WhereDelegate<HostDomainApplicationColumns> where)
        {
            return Where(where, null, null);
        }

        public static HostDomainApplicationQuery Where(WhereDelegate<HostDomainApplicationColumns> where, OrderBy<HostDomainApplicationColumns> orderBy = null, Database db = null)
        {
            return new HostDomainApplicationQuery(where, orderBy, db);
        }

		public HostDomainApplicationCollection Execute()
		{
			return new HostDomainApplicationCollection(this, true);
		}
    }
}