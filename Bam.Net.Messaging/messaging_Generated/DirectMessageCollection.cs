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
    public class DirectMessageCollection: DaoCollection<DirectMessageColumns, DirectMessage>
    { 
		public DirectMessageCollection(){}
		public DirectMessageCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DirectMessageCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DirectMessageCollection(Query<DirectMessageColumns, DirectMessage> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DirectMessageCollection(Database db, Query<DirectMessageColumns, DirectMessage> q, bool load) : base(db, q, load) { }
		public DirectMessageCollection(Query<DirectMessageColumns, DirectMessage> q, bool load) : base(q, load) { }
    }
}