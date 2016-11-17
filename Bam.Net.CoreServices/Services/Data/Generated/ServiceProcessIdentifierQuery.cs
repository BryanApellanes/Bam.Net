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
    public class ServiceProcessIdentifierQuery: Query<ServiceProcessIdentifierColumns, ServiceProcessIdentifier>
    { 
		public ServiceProcessIdentifierQuery(){}
		public ServiceProcessIdentifierQuery(WhereDelegate<ServiceProcessIdentifierColumns> where, OrderBy<ServiceProcessIdentifierColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ServiceProcessIdentifierQuery(Func<ServiceProcessIdentifierColumns, QueryFilter<ServiceProcessIdentifierColumns>> where, OrderBy<ServiceProcessIdentifierColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ServiceProcessIdentifierQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ServiceProcessIdentifierQuery Where(WhereDelegate<ServiceProcessIdentifierColumns> where)
        {
            return Where(where, null, null);
        }

        public static ServiceProcessIdentifierQuery Where(WhereDelegate<ServiceProcessIdentifierColumns> where, OrderBy<ServiceProcessIdentifierColumns> orderBy = null, Database db = null)
        {
            return new ServiceProcessIdentifierQuery(where, orderBy, db);
        }

		public ServiceProcessIdentifierCollection Execute()
		{
			return new ServiceProcessIdentifierCollection(this, true);
		}
    }
}