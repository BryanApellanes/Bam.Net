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
    public class EmailMessageQuery: Query<EmailMessageColumns, EmailMessage>
    { 
		public EmailMessageQuery(){}
		public EmailMessageQuery(WhereDelegate<EmailMessageColumns> where, OrderBy<EmailMessageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public EmailMessageQuery(Func<EmailMessageColumns, QueryFilter<EmailMessageColumns>> where, OrderBy<EmailMessageColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public EmailMessageQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static EmailMessageQuery Where(WhereDelegate<EmailMessageColumns> where)
        {
            return Where(where, null, null);
        }

        public static EmailMessageQuery Where(WhereDelegate<EmailMessageColumns> where, OrderBy<EmailMessageColumns> orderBy = null, Database db = null)
        {
            return new EmailMessageQuery(where, orderBy, db);
        }

		public EmailMessageCollection Execute()
		{
			return new EmailMessageCollection(this, true);
		}
    }
}