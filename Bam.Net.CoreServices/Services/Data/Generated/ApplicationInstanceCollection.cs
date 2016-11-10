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
    public class ApplicationInstanceCollection: DaoCollection<ApplicationInstanceColumns, ApplicationInstance>
    { 
		public ApplicationInstanceCollection(){}
		public ApplicationInstanceCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ApplicationInstanceCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ApplicationInstanceCollection(Query<ApplicationInstanceColumns, ApplicationInstance> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ApplicationInstanceCollection(Database db, Query<ApplicationInstanceColumns, ApplicationInstance> q, bool load) : base(db, q, load) { }
		public ApplicationInstanceCollection(Query<ApplicationInstanceColumns, ApplicationInstance> q, bool load) : base(q, load) { }
    }
}