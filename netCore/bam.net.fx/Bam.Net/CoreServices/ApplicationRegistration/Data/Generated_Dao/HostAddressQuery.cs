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
    public class HostAddressQuery: Query<HostAddressColumns, HostAddress>
    { 
		public HostAddressQuery(){}
		public HostAddressQuery(WhereDelegate<HostAddressColumns> where, OrderBy<HostAddressColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public HostAddressQuery(Func<HostAddressColumns, QueryFilter<HostAddressColumns>> where, OrderBy<HostAddressColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public HostAddressQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static HostAddressQuery Where(WhereDelegate<HostAddressColumns> where)
        {
            return Where(where, null, null);
        }

        public static HostAddressQuery Where(WhereDelegate<HostAddressColumns> where, OrderBy<HostAddressColumns> orderBy = null, Database db = null)
        {
            return new HostAddressQuery(where, orderBy, db);
        }

		public HostAddressCollection Execute()
		{
			return new HostAddressCollection(this, true);
		}
    }
}