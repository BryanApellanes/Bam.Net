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
    public class TranslationCollection: DaoCollection<TranslationColumns, Translation>
    { 
		public TranslationCollection(){}
		public TranslationCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public TranslationCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TranslationCollection(Query<TranslationColumns, Translation> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TranslationCollection(Database db, Query<TranslationColumns, Translation> q, bool load) : base(db, q, load) { }
		public TranslationCollection(Query<TranslationColumns, Translation> q, bool load) : base(q, load) { }
    }
}