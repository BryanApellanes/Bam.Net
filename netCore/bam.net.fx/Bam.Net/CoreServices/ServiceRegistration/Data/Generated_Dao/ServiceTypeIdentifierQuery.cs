/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class ServiceTypeIdentifierQuery: Query<ServiceTypeIdentifierColumns, ServiceTypeIdentifier>
    { 
		public ServiceTypeIdentifierQuery(){}
		public ServiceTypeIdentifierQuery(WhereDelegate<ServiceTypeIdentifierColumns> where, OrderBy<ServiceTypeIdentifierColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ServiceTypeIdentifierQuery(Func<ServiceTypeIdentifierColumns, QueryFilter<ServiceTypeIdentifierColumns>> where, OrderBy<ServiceTypeIdentifierColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ServiceTypeIdentifierQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ServiceTypeIdentifierQuery Where(WhereDelegate<ServiceTypeIdentifierColumns> where)
        {
            return Where(where, null, null);
        }

        public static ServiceTypeIdentifierQuery Where(WhereDelegate<ServiceTypeIdentifierColumns> where, OrderBy<ServiceTypeIdentifierColumns> orderBy = null, Database db = null)
        {
            return new ServiceTypeIdentifierQuery(where, orderBy, db);
        }

		public ServiceTypeIdentifierCollection Execute()
		{
			return new ServiceTypeIdentifierCollection(this, true);
		}
    }
}