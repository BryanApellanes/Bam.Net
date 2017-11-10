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
    public class ImageCollection: DaoCollection<ImageColumns, Image>
    { 
		public ImageCollection(){}
		public ImageCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public ImageCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ImageCollection(Query<ImageColumns, Image> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ImageCollection(Database db, Query<ImageColumns, Image> q, bool load) : base(db, q, load) { }
		public ImageCollection(Query<ImageColumns, Image> q, bool load) : base(q, load) { }
    }
}