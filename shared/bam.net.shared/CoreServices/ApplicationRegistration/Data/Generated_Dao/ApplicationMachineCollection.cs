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
    public class ApplicationMachineCollection: DaoCollection<ApplicationMachineColumns, ApplicationMachine>
    { 
		public ApplicationMachineCollection(){}
		public ApplicationMachineCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ApplicationMachineCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ApplicationMachineCollection(Query<ApplicationMachineColumns, ApplicationMachine> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ApplicationMachineCollection(Database db, Query<ApplicationMachineColumns, ApplicationMachine> q, bool load) : base(db, q, load) { }
		public ApplicationMachineCollection(Query<ApplicationMachineColumns, ApplicationMachine> q, bool load) : base(q, load) { }
    }
}