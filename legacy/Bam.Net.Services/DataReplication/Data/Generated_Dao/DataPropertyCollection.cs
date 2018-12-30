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
    public class DataPropertyCollection: DaoCollection<DataPropertyColumns, DataProperty>
    { 
		public DataPropertyCollection(){}
		public DataPropertyCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DataPropertyCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DataPropertyCollection(Query<DataPropertyColumns, DataProperty> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DataPropertyCollection(Database db, Query<DataPropertyColumns, DataProperty> q, bool load) : base(db, q, load) { }
		public DataPropertyCollection(Query<DataPropertyColumns, DataProperty> q, bool load) : base(q, load) { }
    }
}