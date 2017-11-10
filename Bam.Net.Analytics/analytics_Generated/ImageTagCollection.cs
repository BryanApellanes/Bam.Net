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
    public class ImageTagCollection: DaoCollection<ImageTagColumns, ImageTag>
    { 
		public ImageTagCollection(){}
		public ImageTagCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ImageTagCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ImageTagCollection(Query<ImageTagColumns, ImageTag> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ImageTagCollection(Database db, Query<ImageTagColumns, ImageTag> q, bool load) : base(db, q, load) { }
		public ImageTagCollection(Query<ImageTagColumns, ImageTag> q, bool load) : base(q, load) { }
    }
}