/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
    public class MachineServicesCollection: DaoCollection<MachineServicesColumns, MachineServices>
    { 
		public MachineServicesCollection(){}
		public MachineServicesCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public MachineServicesCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MachineServicesCollection(Query<MachineServicesColumns, MachineServices> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MachineServicesCollection(Database db, Query<MachineServicesColumns, MachineServices> q, bool load) : base(db, q, load) { }
		public MachineServicesCollection(Query<MachineServicesColumns, MachineServices> q, bool load) : base(q, load) { }
    }
}