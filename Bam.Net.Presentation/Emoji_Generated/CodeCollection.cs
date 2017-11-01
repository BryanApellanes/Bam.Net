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
    public class CodeCollection: DaoCollection<CodeColumns, Code>
    { 
		public CodeCollection(){}
		public CodeCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CodeCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CodeCollection(Query<CodeColumns, Code> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CodeCollection(Database db, Query<CodeColumns, Code> q, bool load) : base(db, q, load) { }
		public CodeCollection(Query<CodeColumns, Code> q, bool load) : base(q, load) { }
    }
}