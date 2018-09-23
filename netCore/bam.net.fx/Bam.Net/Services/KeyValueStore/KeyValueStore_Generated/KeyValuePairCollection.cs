/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.Data
{
    public class KeyValuePairCollection: DaoCollection<KeyValuePairColumns, KeyValuePair>
    { 
		public KeyValuePairCollection(){}
		public KeyValuePairCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public KeyValuePairCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public KeyValuePairCollection(Query<KeyValuePairColumns, KeyValuePair> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public KeyValuePairCollection(Database db, Query<KeyValuePairColumns, KeyValuePair> q, bool load) : base(db, q, load) { }
		public KeyValuePairCollection(Query<KeyValuePairColumns, KeyValuePair> q, bool load) : base(q, load) { }
    }
}