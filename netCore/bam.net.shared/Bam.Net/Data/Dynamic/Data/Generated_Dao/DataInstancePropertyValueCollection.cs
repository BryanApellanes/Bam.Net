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
    public class DataInstancePropertyValueCollection: DaoCollection<DataInstancePropertyValueColumns, DataInstancePropertyValue>
    { 
		public DataInstancePropertyValueCollection(){}
		public DataInstancePropertyValueCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DataInstancePropertyValueCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DataInstancePropertyValueCollection(Query<DataInstancePropertyValueColumns, DataInstancePropertyValue> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DataInstancePropertyValueCollection(Database db, Query<DataInstancePropertyValueColumns, DataInstancePropertyValue> q, bool load) : base(db, q, load) { }
		public DataInstancePropertyValueCollection(Query<DataInstancePropertyValueColumns, DataInstancePropertyValue> q, bool load) : base(q, load) { }
    }
}