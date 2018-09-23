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
    public class MachineCollection: DaoCollection<MachineColumns, Machine>
    { 
		public MachineCollection(){}
		public MachineCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public MachineCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MachineCollection(Query<MachineColumns, Machine> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MachineCollection(Database db, Query<MachineColumns, Machine> q, bool load) : base(db, q, load) { }
		public MachineCollection(Query<MachineColumns, Machine> q, bool load) : base(q, load) { }
    }
}