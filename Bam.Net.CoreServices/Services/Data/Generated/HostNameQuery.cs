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
    public class HostNameQuery: Query<HostNameColumns, HostName>
    { 
		public HostNameQuery(){}
		public HostNameQuery(WhereDelegate<HostNameColumns> where, OrderBy<HostNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public HostNameQuery(Func<HostNameColumns, QueryFilter<HostNameColumns>> where, OrderBy<HostNameColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public HostNameQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static HostNameQuery Where(WhereDelegate<HostNameColumns> where)
        {
            return Where(where, null, null);
        }

        public static HostNameQuery Where(WhereDelegate<HostNameColumns> where, OrderBy<HostNameColumns> orderBy = null, Database db = null)
        {
            return new HostNameQuery(where, orderBy, db);
        }

		public HostNameCollection Execute()
		{
			return new HostNameCollection(this, true);
		}
    }
}