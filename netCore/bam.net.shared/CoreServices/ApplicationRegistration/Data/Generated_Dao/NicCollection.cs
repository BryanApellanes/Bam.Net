/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class NicCollection: DaoCollection<NicColumns, Nic>
    { 
		public NicCollection(){}
		public NicCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public NicCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public NicCollection(Query<NicColumns, Nic> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public NicCollection(Database db, Query<NicColumns, Nic> q, bool load) : base(db, q, load) { }
		public NicCollection(Query<NicColumns, Nic> q, bool load) : base(q, load) { }
    }
}