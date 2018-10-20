/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class SecureSessionQuery: Query<SecureSessionColumns, SecureSession>
    { 
		public SecureSessionQuery(){}
		public SecureSessionQuery(WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public SecureSessionQuery(Func<SecureSessionColumns, QueryFilter<SecureSessionColumns>> where, OrderBy<SecureSessionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public SecureSessionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static SecureSessionQuery Where(WhereDelegate<SecureSessionColumns> where)
        {
            return Where(where, null, null);
        }

        public static SecureSessionQuery Where(WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy = null, Database db = null)
        {
            return new SecureSessionQuery(where, orderBy, db);
        }

		public SecureSessionCollection Execute()
		{
			return new SecureSessionCollection(this, true);
		}
    }
}