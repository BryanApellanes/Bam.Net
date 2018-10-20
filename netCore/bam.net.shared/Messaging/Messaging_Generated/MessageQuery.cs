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
    public class MessageQuery: Query<MessageColumns, Message>
    { 
		public MessageQuery(){}
		public MessageQuery(WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public MessageQuery(Func<MessageColumns, QueryFilter<MessageColumns>> where, OrderBy<MessageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public MessageQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static MessageQuery Where(WhereDelegate<MessageColumns> where)
        {
            return Where(where, null, null);
        }

        public static MessageQuery Where(WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy = null, Database db = null)
        {
            return new MessageQuery(where, orderBy, db);
        }

		public MessageCollection Execute()
		{
			return new MessageCollection(this, true);
		}
    }
}