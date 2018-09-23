/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class UrlTagCollection: DaoCollection<UrlTagColumns, UrlTag>
    { 
		public UrlTagCollection(){}
		public UrlTagCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public UrlTagCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UrlTagCollection(Query<UrlTagColumns, UrlTag> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UrlTagCollection(Database db, Query<UrlTagColumns, UrlTag> q, bool load) : base(db, q, load) { }
		public UrlTagCollection(Query<UrlTagColumns, UrlTag> q, bool load) : base(q, load) { }
    }
}