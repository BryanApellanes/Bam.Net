/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Translation
{
    public class OtherNameCollection: DaoCollection<OtherNameColumns, OtherName>
    { 
		public OtherNameCollection(){}
		public OtherNameCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public OtherNameCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public OtherNameCollection(Query<OtherNameColumns, OtherName> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public OtherNameCollection(Database db, Query<OtherNameColumns, OtherName> q, bool load) : base(db, q, load) { }
		public OtherNameCollection(Query<OtherNameColumns, OtherName> q, bool load) : base(q, load) { }
    }
}