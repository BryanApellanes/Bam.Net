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
    public class CookieDataCollection: DaoCollection<CookieDataColumns, CookieData>
    { 
		public CookieDataCollection(){}
		public CookieDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CookieDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CookieDataCollection(Query<CookieDataColumns, CookieData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CookieDataCollection(Database db, Query<CookieDataColumns, CookieData> q, bool load) : base(db, q, load) { }
		public CookieDataCollection(Query<CookieDataColumns, CookieData> q, bool load) : base(q, load) { }
    }
}