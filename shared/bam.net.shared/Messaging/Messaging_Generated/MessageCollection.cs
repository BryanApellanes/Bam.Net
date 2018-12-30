/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Messaging.Data
{
    public class MessageCollection: DaoCollection<MessageColumns, Message>
    { 
		public MessageCollection(){}
		public MessageCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public MessageCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MessageCollection(Query<MessageColumns, Message> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MessageCollection(Database db, Query<MessageColumns, Message> q, bool load) : base(db, q, load) { }
		public MessageCollection(Query<MessageColumns, Message> q, bool load) : base(q, load) { }
    }
}