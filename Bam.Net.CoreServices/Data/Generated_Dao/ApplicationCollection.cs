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
    public class ApplicationCollection: DaoCollection<ApplicationColumns, Application>
    { 
		public ApplicationCollection(){}
		public ApplicationCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ApplicationCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ApplicationCollection(Query<ApplicationColumns, Application> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ApplicationCollection(Database db, Query<ApplicationColumns, Application> q, bool load) : base(db, q, load) { }
		public ApplicationCollection(Query<ApplicationColumns, Application> q, bool load) : base(q, load) { }
    }
}