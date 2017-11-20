/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ApiKeyCollection: DaoCollection<ApiKeyColumns, ApiKey>
    { 
		public ApiKeyCollection(){}
		public ApiKeyCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ApiKeyCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ApiKeyCollection(Query<ApiKeyColumns, ApiKey> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ApiKeyCollection(Database db, Query<ApiKeyColumns, ApiKey> q, bool load) : base(db, q, load) { }
		public ApiKeyCollection(Query<ApiKeyColumns, ApiKey> q, bool load) : base(q, load) { }
    }
}