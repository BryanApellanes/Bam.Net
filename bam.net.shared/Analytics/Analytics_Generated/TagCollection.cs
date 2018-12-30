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
    public class TagCollection: DaoCollection<TagColumns, Tag>
    { 
		public TagCollection(){}
		public TagCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TagCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TagCollection(Query<TagColumns, Tag> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TagCollection(Database db, Query<TagColumns, Tag> q, bool load) : base(db, q, load) { }
		public TagCollection(Query<TagColumns, Tag> q, bool load) : base(q, load) { }
    }
}