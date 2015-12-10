/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Instructions
{
    public class SectionCollection: DaoCollection<SectionColumns, Section>
    { 
		public SectionCollection(){}
		public SectionCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SectionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SectionCollection(Query<SectionColumns, Section> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SectionCollection(Database db, Query<SectionColumns, Section> q, bool load) : base(db, q, load) { }
		public SectionCollection(Query<SectionColumns, Section> q, bool load) : base(q, load) { }
    }
}