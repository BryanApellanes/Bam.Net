/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class DataPointCollection: DaoCollection<DataPointColumns, DataPoint>
    { 
		public DataPointCollection(){}
		public DataPointCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DataPointCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DataPointCollection(Query<DataPointColumns, DataPoint> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DataPointCollection(Database db, Query<DataPointColumns, DataPoint> q, bool load) : base(db, q, load) { }
		public DataPointCollection(Query<DataPointColumns, DataPoint> q, bool load) : base(q, load) { }
    }
}