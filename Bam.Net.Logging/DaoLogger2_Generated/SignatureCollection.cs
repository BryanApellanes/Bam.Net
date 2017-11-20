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
    public class SignatureCollection: DaoCollection<SignatureColumns, Signature>
    { 
		public SignatureCollection(){}
		public SignatureCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SignatureCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SignatureCollection(Query<SignatureColumns, Signature> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SignatureCollection(Database db, Query<SignatureColumns, Signature> q, bool load) : base(db, q, load) { }
		public SignatureCollection(Query<SignatureColumns, Signature> q, bool load) : base(q, load) { }
    }
}