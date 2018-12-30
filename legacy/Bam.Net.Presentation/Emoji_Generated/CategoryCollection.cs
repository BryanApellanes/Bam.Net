/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Presentation.Unicode
{
    public class CategoryCollection: DaoCollection<CategoryColumns, Category>
    { 
		public CategoryCollection(){}
		public CategoryCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CategoryCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CategoryCollection(Query<CategoryColumns, Category> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CategoryCollection(Database db, Query<CategoryColumns, Category> q, bool load) : base(db, q, load) { }
		public CategoryCollection(Query<CategoryColumns, Category> q, bool load) : base(q, load) { }
    }
}