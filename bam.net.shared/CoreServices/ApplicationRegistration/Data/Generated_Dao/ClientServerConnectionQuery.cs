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
    public class ClientServerConnectionQuery: Query<ClientServerConnectionColumns, ClientServerConnection>
    { 
		public ClientServerConnectionQuery(){}
		public ClientServerConnectionQuery(WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public ClientServerConnectionQuery(Func<ClientServerConnectionColumns, QueryFilter<ClientServerConnectionColumns>> where, OrderBy<ClientServerConnectionColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public ClientServerConnectionQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static ClientServerConnectionQuery Where(WhereDelegate<ClientServerConnectionColumns> where)
        {
            return Where(where, null, null);
        }

        public static ClientServerConnectionQuery Where(WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy = null, Database db = null)
        {
            return new ClientServerConnectionQuery(where, orderBy, db);
        }

		public ClientServerConnectionCollection Execute()
		{
			return new ClientServerConnectionCollection(this, true);
		}
    }
}