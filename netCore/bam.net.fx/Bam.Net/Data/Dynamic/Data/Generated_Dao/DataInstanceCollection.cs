/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class DataInstanceCollection: DaoCollection<DataInstanceColumns, DataInstance>
    { 
		public DataInstanceCollection(){}
		public DataInstanceCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DataInstanceCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DataInstanceCollection(Query<DataInstanceColumns, DataInstance> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DataInstanceCollection(Database db, Query<DataInstanceColumns, DataInstance> q, bool load) : base(db, q, load) { }
		public DataInstanceCollection(Query<DataInstanceColumns, DataInstance> q, bool load) : base(q, load) { }
    }
}