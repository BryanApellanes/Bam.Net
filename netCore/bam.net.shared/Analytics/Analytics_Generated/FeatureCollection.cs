/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class FeatureCollection: DaoCollection<FeatureColumns, Feature>
    { 
		public FeatureCollection(){}
		public FeatureCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public FeatureCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public FeatureCollection(Query<FeatureColumns, Feature> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public FeatureCollection(Database db, Query<FeatureColumns, Feature> q, bool load) : base(db, q, load) { }
		public FeatureCollection(Query<FeatureColumns, Feature> q, bool load) : base(q, load) { }
    }
}