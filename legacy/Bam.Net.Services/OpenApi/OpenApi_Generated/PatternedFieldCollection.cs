/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.OpenApi
{
    public class PatternedFieldCollection: DaoCollection<PatternedFieldColumns, PatternedField>
    { 
		public PatternedFieldCollection(){}
		public PatternedFieldCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PatternedFieldCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PatternedFieldCollection(Query<PatternedFieldColumns, PatternedField> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PatternedFieldCollection(Database db, Query<PatternedFieldColumns, PatternedField> q, bool load) : base(db, q, load) { }
		public PatternedFieldCollection(Query<PatternedFieldColumns, PatternedField> q, bool load) : base(q, load) { }
    }
}