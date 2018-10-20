/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class SourceNameCollection: DaoCollection<SourceNameColumns, SourceName>
    { 
		public SourceNameCollection(){}
		public SourceNameCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SourceNameCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SourceNameCollection(Query<SourceNameColumns, SourceName> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SourceNameCollection(Database db, Query<SourceNameColumns, SourceName> q, bool load) : base(db, q, load) { }
		public SourceNameCollection(Query<SourceNameColumns, SourceName> q, bool load) : base(q, load) { }
    }
}