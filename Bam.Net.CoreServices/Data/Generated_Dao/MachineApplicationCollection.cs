/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class MachineApplicationCollection: DaoCollection<MachineApplicationColumns, MachineApplication>
    { 
		public MachineApplicationCollection(){}
		public MachineApplicationCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public MachineApplicationCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MachineApplicationCollection(Query<MachineApplicationColumns, MachineApplication> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MachineApplicationCollection(Database db, Query<MachineApplicationColumns, MachineApplication> q, bool load) : base(db, q, load) { }
		public MachineApplicationCollection(Query<MachineApplicationColumns, MachineApplication> q, bool load) : base(q, load) { }
    }
}