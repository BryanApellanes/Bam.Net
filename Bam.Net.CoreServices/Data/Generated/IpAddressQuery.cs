/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class IpAddressQuery: Query<IpAddressColumns, IpAddress>
    { 
		public IpAddressQuery(){}
		public IpAddressQuery(WhereDelegate<IpAddressColumns> where, OrderBy<IpAddressColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public IpAddressQuery(Func<IpAddressColumns, QueryFilter<IpAddressColumns>> where, OrderBy<IpAddressColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public IpAddressQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static IpAddressQuery Where(WhereDelegate<IpAddressColumns> where)
        {
            return Where(where, null, null);
        }

        public static IpAddressQuery Where(WhereDelegate<IpAddressColumns> where, OrderBy<IpAddressColumns> orderBy = null, Database db = null)
        {
            return new IpAddressQuery(where, orderBy, db);
        }

		public IpAddressCollection Execute()
		{
			return new IpAddressCollection(this, true);
		}
    }
}