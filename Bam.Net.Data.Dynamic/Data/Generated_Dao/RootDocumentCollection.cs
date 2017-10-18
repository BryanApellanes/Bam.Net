/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class RootDocumentCollection: DaoCollection<RootDocumentColumns, RootDocument>
    { 
		public RootDocumentCollection(){}
		public RootDocumentCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RootDocumentCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RootDocumentCollection(Query<RootDocumentColumns, RootDocument> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RootDocumentCollection(Database db, Query<RootDocumentColumns, RootDocument> q, bool load) : base(db, q, load) { }
		public RootDocumentCollection(Query<RootDocumentColumns, RootDocument> q, bool load) : base(q, load) { }
    }
}