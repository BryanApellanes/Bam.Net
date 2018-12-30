/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class HeaderDataCollection: DaoCollection<HeaderDataColumns, HeaderData>
    { 
		public HeaderDataCollection(){}
		public HeaderDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public HeaderDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HeaderDataCollection(Query<HeaderDataColumns, HeaderData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HeaderDataCollection(Database db, Query<HeaderDataColumns, HeaderData> q, bool load) : base(db, q, load) { }
		public HeaderDataCollection(Query<HeaderDataColumns, HeaderData> q, bool load) : base(q, load) { }
    }
}