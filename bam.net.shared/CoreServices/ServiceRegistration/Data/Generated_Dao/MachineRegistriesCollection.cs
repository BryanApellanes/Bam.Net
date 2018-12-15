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
    public class MachineRegistriesCollection: DaoCollection<MachineRegistriesColumns, MachineRegistries>
    { 
		public MachineRegistriesCollection(){}
		public MachineRegistriesCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public MachineRegistriesCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public MachineRegistriesCollection(Query<MachineRegistriesColumns, MachineRegistries> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public MachineRegistriesCollection(Database db, Query<MachineRegistriesColumns, MachineRegistries> q, bool load) : base(db, q, load) { }
		public MachineRegistriesCollection(Query<MachineRegistriesColumns, MachineRegistries> q, bool load) : base(q, load) { }
    }
}