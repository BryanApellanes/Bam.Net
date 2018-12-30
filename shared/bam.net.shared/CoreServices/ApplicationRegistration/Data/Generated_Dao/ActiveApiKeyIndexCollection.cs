/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ActiveApiKeyIndexCollection: DaoCollection<ActiveApiKeyIndexColumns, ActiveApiKeyIndex>
    { 
		public ActiveApiKeyIndexCollection(){}
		public ActiveApiKeyIndexCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ActiveApiKeyIndexCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ActiveApiKeyIndexCollection(Query<ActiveApiKeyIndexColumns, ActiveApiKeyIndex> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ActiveApiKeyIndexCollection(Database db, Query<ActiveApiKeyIndexColumns, ActiveApiKeyIndex> q, bool load) : base(db, q, load) { }
		public ActiveApiKeyIndexCollection(Query<ActiveApiKeyIndexColumns, ActiveApiKeyIndex> q, bool load) : base(q, load) { }
    }
}