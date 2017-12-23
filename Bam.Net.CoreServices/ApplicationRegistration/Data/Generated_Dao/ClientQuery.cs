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
    public class ClientQuery: Query<ClientColumns, Client>
    { 
		public ClientQuery(){}
		public ClientQuery(WhereDelegate<ClientColumns> where, OrderBy<ClientColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ClientQuery(Func<ClientColumns, QueryFilter<ClientColumns>> where, OrderBy<ClientColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ClientQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ClientQuery Where(WhereDelegate<ClientColumns> where)
        {
            return Where(where, null, null);
        }

        public static ClientQuery Where(WhereDelegate<ClientColumns> where, OrderBy<ClientColumns> orderBy = null, Database db = null)
        {
            return new ClientQuery(where, orderBy, db);
        }

		public ClientCollection Execute()
		{
			return new ClientCollection(this, true);
		}
    }
}