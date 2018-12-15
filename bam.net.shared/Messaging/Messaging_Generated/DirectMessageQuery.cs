/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Messaging.Data
{
    public class DirectMessageQuery: Query<DirectMessageColumns, DirectMessage>
    { 
		public DirectMessageQuery(){}
		public DirectMessageQuery(WhereDelegate<DirectMessageColumns> where, OrderBy<DirectMessageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DirectMessageQuery(Func<DirectMessageColumns, QueryFilter<DirectMessageColumns>> where, OrderBy<DirectMessageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DirectMessageQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DirectMessageQuery Where(WhereDelegate<DirectMessageColumns> where)
        {
            return Where(where, null, null);
        }

        public static DirectMessageQuery Where(WhereDelegate<DirectMessageColumns> where, OrderBy<DirectMessageColumns> orderBy = null, Database db = null)
        {
            return new DirectMessageQuery(where, orderBy, db);
        }

		public DirectMessageCollection Execute()
		{
			return new DirectMessageCollection(this, true);
		}
    }
}