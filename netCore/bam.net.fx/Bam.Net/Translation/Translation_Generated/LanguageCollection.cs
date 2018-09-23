/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Translation
{
    public class LanguageCollection: DaoCollection<LanguageColumns, Language>
    { 
		public LanguageCollection(){}
		public LanguageCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LanguageCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LanguageCollection(Query<LanguageColumns, Language> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LanguageCollection(Database db, Query<LanguageColumns, Language> q, bool load) : base(db, q, load) { }
		public LanguageCollection(Query<LanguageColumns, Language> q, bool load) : base(q, load) { }
    }
}