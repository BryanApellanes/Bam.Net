/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.Distributed.Data.Dao
{
    public class DataRelationshipCollection: DaoCollection<DataRelationshipColumns, DataRelationship>
    { 
		public DataRelationshipCollection(){}
		public DataRelationshipCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DataRelationshipCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DataRelationshipCollection(Query<DataRelationshipColumns, DataRelationship> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DataRelationshipCollection(Database db, Query<DataRelationshipColumns, DataRelationship> q, bool load) : base(db, q, load) { }
		public DataRelationshipCollection(Query<DataRelationshipColumns, DataRelationship> q, bool load) : base(q, load) { }
    }
}