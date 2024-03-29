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
    public class FixedFieldCollection: DaoCollection<FixedFieldColumns, FixedField>
    { 
		public FixedFieldCollection(){}
		public FixedFieldCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public FixedFieldCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public FixedFieldCollection(Query<FixedFieldColumns, FixedField> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public FixedFieldCollection(Database db, Query<FixedFieldColumns, FixedField> q, bool load) : base(db, q, load) { }
		public FixedFieldCollection(Query<FixedFieldColumns, FixedField> q, bool load) : base(q, load) { }
    }
}