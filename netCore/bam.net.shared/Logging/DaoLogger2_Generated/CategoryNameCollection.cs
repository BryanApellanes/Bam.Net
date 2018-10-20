/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class CategoryNameCollection: DaoCollection<CategoryNameColumns, CategoryName>
    { 
		public CategoryNameCollection(){}
		public CategoryNameCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CategoryNameCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CategoryNameCollection(Query<CategoryNameColumns, CategoryName> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CategoryNameCollection(Database db, Query<CategoryNameColumns, CategoryName> q, bool load) : base(db, q, load) { }
		public CategoryNameCollection(Query<CategoryNameColumns, CategoryName> q, bool load) : base(q, load) { }
    }
}