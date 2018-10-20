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
    public class PathCollection: DaoCollection<PathColumns, Path>
    { 
		public PathCollection(){}
		public PathCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PathCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PathCollection(Query<PathColumns, Path> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PathCollection(Database db, Query<PathColumns, Path> q, bool load) : base(db, q, load) { }
		public PathCollection(Query<PathColumns, Path> q, bool load) : base(q, load) { }
    }
}