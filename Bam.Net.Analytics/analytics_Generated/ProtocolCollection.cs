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
    public class ProtocolCollection: DaoCollection<ProtocolColumns, Protocol>
    { 
		public ProtocolCollection(){}
		public ProtocolCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ProtocolCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ProtocolCollection(Query<ProtocolColumns, Protocol> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ProtocolCollection(Database db, Query<ProtocolColumns, Protocol> q, bool load) : base(db, q, load) { }
		public ProtocolCollection(Query<ProtocolColumns, Protocol> q, bool load) : base(q, load) { }
    }
}