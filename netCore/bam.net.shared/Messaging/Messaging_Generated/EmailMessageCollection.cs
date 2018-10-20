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
    public class EmailMessageCollection: DaoCollection<EmailMessageColumns, EmailMessage>
    { 
		public EmailMessageCollection(){}
		public EmailMessageCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public EmailMessageCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EmailMessageCollection(Query<EmailMessageColumns, EmailMessage> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EmailMessageCollection(Database db, Query<EmailMessageColumns, EmailMessage> q, bool load) : base(db, q, load) { }
		public EmailMessageCollection(Query<EmailMessageColumns, EmailMessage> q, bool load) : base(q, load) { }
    }
}