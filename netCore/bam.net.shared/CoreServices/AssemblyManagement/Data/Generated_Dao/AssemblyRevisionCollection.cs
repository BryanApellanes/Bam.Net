/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyRevisionCollection: DaoCollection<AssemblyRevisionColumns, AssemblyRevision>
    { 
		public AssemblyRevisionCollection(){}
		public AssemblyRevisionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public AssemblyRevisionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AssemblyRevisionCollection(Query<AssemblyRevisionColumns, AssemblyRevision> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AssemblyRevisionCollection(Database db, Query<AssemblyRevisionColumns, AssemblyRevision> q, bool load) : base(db, q, load) { }
		public AssemblyRevisionCollection(Query<AssemblyRevisionColumns, AssemblyRevision> q, bool load) : base(q, load) { }
    }
}