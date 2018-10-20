/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics.EnglishDictionary
{
    public class DefinitionCollection: DaoCollection<DefinitionColumns, Definition>
    { 
		public DefinitionCollection(){}
		public DefinitionCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DefinitionCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DefinitionCollection(Query<DefinitionColumns, Definition> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DefinitionCollection(Database db, Query<DefinitionColumns, Definition> q, bool load) : base(db, q, load) { }
		public DefinitionCollection(Query<DefinitionColumns, Definition> q, bool load) : base(q, load) { }
    }
}